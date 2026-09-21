using System;
using System.Collections.Generic;
using System.Drawing;

namespace Scruple
{
    // 좌표 단위는 픽셀이 아니라 타일이다. 렌더러만 화면 픽셀로 변환한다.
    public struct Vec
    {
        public double X, Y;
        public Vec(double x, double y) { X = x; Y = y; }
        public static Vec Center(int x, int y) { return new Vec(x + .5, y + .5); }
        public double Distance(Vec b) { double x=X-b.X, y=Y-b.Y; return Math.Sqrt(x*x+y*y); }
        public Point Tile { get { return new Point((int)Math.Floor(X), (int)Math.Floor(Y)); } }
    }

    public static class Tuning
    {
        public const double PlayerSpeed = 2.85, PlayerRadius = .21;
        public const double PatrolSpeed = 1.15, ChaseSpeed = 2.50, MonsterRadius = .22;
        public const double NoticeTime = .70, LostSightTime = 1.15, SearchTime = 3.2;
        public const double InteractionRange = 1.12, CatchDistance = .40;
    }

    public class Portal
    {
        public Vec At, Arrival;
        public int Target;
        public string Label;
        public bool ShortcutOnly;
        public Portal(int x,int y,int target,Vec arrival,string label,bool shortcut)
        { At=Vec.Center(x,y); Target=target; Arrival=arrival; Label=label; ShortcutOnly=shortcut; }
    }

    public class Maze
    {
        public const int Width=25, Height=19;
        public readonly int Id;
        public readonly string Name, Subtitle, Hint;
        public readonly bool[,] Floor = new bool[Width,Height];
        public readonly List<Portal> Portals = new List<Portal>();
        public readonly List<Vec[]> Patrols = new List<Vec[]>();
        public Vec Memory, Switch=Vec.Center(12,13), Gate=Vec.Center(12,15);
        public bool GateOpen;
        public Maze(int id,string name,string subtitle,string hint)
        { Id=id; Name=name; Subtitle=subtitle; Hint=hint; }
        public void Line(int x,int y,int ex,int ey)
        {
            if(x!=ex && y!=ey) throw new ArgumentException("통로는 가로 또는 세로여야 합니다.");
            int dx=Math.Sign(ex-x),dy=Math.Sign(ey-y);
            for(;;) { Floor[x,y]=true; if(x==ex && y==ey)break; x+=dx; y+=dy; }
        }
        public bool Walkable(int x,int y)
        { return x>=0 && y>=0 && x<Width && y<Height && Floor[x,y] && (Id==4 || GateOpen || x!=12 || y!=15); }
        // 두 몸체 모두 같은 지형과 같은 반경 판정을 사용한다.
        public bool Fits(Vec p,double radius)
        {
            return Walkable((int)Math.Floor(p.X-radius),(int)Math.Floor(p.Y-radius))
                && Walkable((int)Math.Floor(p.X+radius),(int)Math.Floor(p.Y-radius))
                && Walkable((int)Math.Floor(p.X-radius),(int)Math.Floor(p.Y+radius))
                && Walkable((int)Math.Floor(p.X+radius),(int)Math.Floor(p.Y+radius));
        }
        public Vec Move(Vec from,double dx,double dy,double radius)
        {
            // 긴 프레임에도 얇은 벽을 건너뛰지 않도록 작은 이동으로 나눈다.
            int steps=Math.Max(1,(int)Math.Ceiling(Math.Max(Math.Abs(dx),Math.Abs(dy))/.10));
            for(int i=0;i<steps;i++)
            {
                Vec next=new Vec(from.X+dx/steps,from.Y); if(Fits(next,radius))from=next;
                next=new Vec(from.X,from.Y+dy/steps); if(Fits(next,radius))from=next;
            }
            return from;
        }
        public bool CanSee(Vec a,Vec b,double range)
        {
            double distance=a.Distance(b); if(distance>range)return false;
            int count=Math.Max(1,(int)Math.Ceiling(distance/.06));
            for(int i=0;i<=count;i++)
            {
                double f=(double)i/count;
                if(!Walkable((int)Math.Floor(a.X+(b.X-a.X)*f),(int)Math.Floor(a.Y+(b.Y-a.Y)*f)))return false;
            }
            return true;
        }
        // 작은 고정 맵에는 우선순위 큐가 없는 BFS도 충분하다.
        // 결과는 출발 칸을 제외한 타일 중심 목록이다.
        public List<Vec> Path(Vec from,Vec to)
        {
            Point s=from.Tile,t=to.Tile;
            List<Vec> result=new List<Vec>();
            if(!Walkable(s.X,s.Y)||!Walkable(t.X,t.Y))return result;
            Queue<Point> q=new Queue<Point>(); Dictionary<Point,Point> parent=new Dictionary<Point,Point>();
            q.Enqueue(s);parent[s]=s;
            int[] dx={1,0,-1,0},dy={0,1,0,-1};
            while(q.Count>0)
            {
                Point p=q.Dequeue(); if(p==t)break;
                for(int i=0;i<4;i++) { Point n=new Point(p.X+dx[i],p.Y+dy[i]);
                    if(Walkable(n.X,n.Y)&&!parent.ContainsKey(n)){parent[n]=p;q.Enqueue(n);} }
            }
            if(!parent.ContainsKey(t))return result;
            for(Point p=t;p!=s;p=parent[p])result.Add(Vec.Center(p.X,p.Y));
            result.Reverse();return result;
        }
    }

    public static class World
    {
        public static readonly string[] Names={"달빛 회랑","잠긴 서고","침묵의 정원","마지막 종탑","새벽의 문턱"};
        public static Maze[] Create()
        {
            Maze[] m={
                new Maze(0,Names[0],"01  /  THE UNANSWERED","붉은 등불을 관찰하세요. 모퉁이를 돌면 시야가 끊어집니다."),
                new Maze(1,Names[1],"02  /  THE UNCHOSEN","한 길이 막혀도 돌아가는 길은 있습니다. 순찰의 빈틈을 기다리세요."),
                new Maze(2,Names[2],"03  /  THE UNSPOKEN","수색 중인 괴물에게 다시 보이지 않게, 두 번 꺾어 달아나세요."),
                new Maze(3,Names[3],"04  /  THE LAST WORDS","기억을 놓쳤다면 돌아가도 괜찮습니다. 이 길들은 이어져 있습니다."),
                new Maze(4,Names[4],"05  /  LETTING GO","이곳에서는 후회가 당신을 쫓지 않습니다.")};
            for(int i=0;i<4;i++)
            {
                Maze a=m[i];
                a.Line(2,2,22,2);a.Line(22,2,22,16);a.Line(22,16,2,16);a.Line(2,16,2,2);
                a.Line(12,13,12,16);a.Line(12,13,18,13);a.Line(18,13,18,16);
                a.Portals.Add(new Portal(2,2,(i+3)%4,Vec.Center(21,16),Names[(i+3)%4],false));
                a.Portals.Add(new Portal(22,16,(i+1)%4,Vec.Center(3,2),Names[(i+1)%4],false));
                a.Portals.Add(new Portal(18,13,4,Vec.Center(4+i*5,14),"새벽의 문턱",i!=3));
            }
            // 고정 설계: 순환 통로와 중앙의 막다른 기억 방을 구역마다 다르게 연결한다.
            m[0].Line(2,6,10,6);m[0].Line(10,2,10,12);m[0].Line(6,12,18,12);
            m[0].Line(18,6,18,16);m[0].Line(14,6,22,6);m[0].Line(6,6,6,16);
            m[0].Line(10,9,14,9);m[0].Line(14,6,14,13);m[0].Memory=Vec.Center(14,9);
            m[0].Patrols.Add(new[]{Vec.Center(18,6),Vec.Center(22,6),Vec.Center(22,16),Vec.Center(18,16)});
            m[1].Line(6,2,6,12);m[1].Line(2,12,10,12);m[1].Line(10,6,10,16);
            m[1].Line(6,6,18,6);m[1].Line(18,2,18,13);m[1].Line(14,10,22,10);
            m[1].Line(14,6,14,10);m[1].Line(10,9,12,9);m[1].Memory=Vec.Center(12,9);
            m[1].Patrols.Add(new[]{Vec.Center(6,6),Vec.Center(18,6),Vec.Center(18,2),Vec.Center(6,2)});
            m[2].Line(2,6,18,6);m[2].Line(6,6,6,16);m[2].Line(10,2,10,10);
            m[2].Line(6,10,22,10);m[2].Line(18,6,18,16);m[2].Line(14,10,14,13);
            m[2].Line(2,13,10,13);m[2].Line(10,10,10,13);m[2].Memory=Vec.Center(10,6);
            m[2].Patrols.Add(new[]{Vec.Center(6,6),Vec.Center(18,6),Vec.Center(18,10),Vec.Center(6,10)});
            m[2].Patrols.Add(new[]{Vec.Center(22,10),Vec.Center(22,16),Vec.Center(18,16),Vec.Center(18,10)});
            m[3].Line(6,2,6,16);m[3].Line(6,6,22,6);m[3].Line(10,6,10,12);
            m[3].Line(2,12,18,12);m[3].Line(18,2,18,16);m[3].Line(14,6,14,9);
            m[3].Line(14,9,18,9);m[3].Line(10,9,12,9);m[3].Memory=Vec.Center(12,9);
            m[3].Patrols.Add(new[]{Vec.Center(6,6),Vec.Center(18,6),Vec.Center(18,12),Vec.Center(6,12)});
            m[3].Patrols.Add(new[]{Vec.Center(22,2),Vec.Center(22,16),Vec.Center(18,16),Vec.Center(18,2)});
            Maze end=m[4];end.GateOpen=true;
            end.Line(4,14,19,14);end.Line(12,4,12,14);end.Line(7,8,17,8);
            end.Line(7,8,7,14);end.Line(17,8,17,14);end.Line(10,4,14,4);
            for(int x=10;x<=14;x++)for(int y=4;y<=6;y++)end.Floor[x,y]=true;
            for(int i=0;i<4;i++)end.Portals.Add(new Portal(4+i*5,14,i,Vec.Center(18,14),Names[i],false));
            return m;
        }
    }
}
