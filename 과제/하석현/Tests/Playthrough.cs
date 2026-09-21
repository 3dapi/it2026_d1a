using System;
using System.Collections.Generic;
using System.Drawing;
using Scruple;

class Playthrough
{
    static List<Vec> Plan(GameSession game,Vec target)
    {
        Maze map=game.Map;Point start=game.Player.Tile,end=target.Tile;
        Dictionary<Point,double> distance=new Dictionary<Point,double>();
        Dictionary<Point,Point> parent=new Dictionary<Point,Point>();
        List<Point> open=new List<Point>();open.Add(start);distance[start]=0;
        int[] dx={1,0,-1,0},dy={0,1,0,-1};
        while(open.Count>0)
        {
            int best=0;for(int i=1;i<open.Count;i++)if(distance[open[i]]<distance[open[best]])best=i;
            Point p=open[best];open.RemoveAt(best);if(p==end)break;
            for(int i=0;i<4;i++)
            {
                Point n=new Point(p.X+dx[i],p.Y+dy[i]);if(!map.Walkable(n.X,n.Y))continue;
                Vec at=Vec.Center(n.X,n.Y);double cost=1;
                foreach(Monster monster in game.Monsters)
                {
                    double d=monster.Position.Distance(at);
                    if(d<1.5)cost+=1000;
                    else if(d<3)cost+=50;
                    else if(d<4.5 && map.CanSee(monster.Position,at,5))cost+=12;
                }
                double value=distance[p]+cost;
                if(!distance.ContainsKey(n)||value<distance[n]){distance[n]=value;parent[n]=p;if(!open.Contains(n))open.Add(n);}
            }
        }
        List<Vec> path=new List<Vec>();if(!distance.ContainsKey(end))return path;
        for(Point p=end;p!=start;p=parent[p])path.Add(Vec.Center(p.X,p.Y));path.Reverse();return path;
    }
    static bool Travel(GameSession game,Vec target,int maxFrames)
    {
        Vec next=game.Player;int stuck=0;
        for(int i=0;i<maxFrames;i++)
        {
            if(game.Mode!=Screen.Playing)return false;
            if(game.Player.Distance(target)<.10)return true;
            if(game.Player.Distance(next)<.025)
            {
                var path=Plan(game,target);if(path.Count==0)return false;next=path[0];
            }
            double d=game.Player.Distance(next);
            Vec before=game.Player;game.Update(Math.Min(1.0/60,d/Tuning.PlayerSpeed),next.X-game.Player.X,next.Y-game.Player.Y);
            if(game.Player.Distance(before)<.00001)stuck++;else stuck=0;if(stuck>120)return false;
        }
        return false;
    }
    static void Main()
    {
        GameSession g=new GameSession();g.NewGame();g.SetMode(Screen.Playing);
        bool ok=true;
        for(int area=0;area<4;area++)
        {
            bool cleared=false;
            for(int attempt=0;attempt<6;attempt++)
            {
                bool reached=Travel(g,g.Map.Memory,60*240);
                if(reached)
                {g.Interact();while(g.Mode==Screen.Dialogue){g.Update(.1,0,0);g.Advance();}cleared=true;break;}
                Console.WriteLine("Retry area "+area+" after "+g.Elapsed.ToString("F1")+"s, mode="+g.Mode);g.Retry();
                // Wait a different time before each retry; no monster is removed or frozen.
                for(int j=0;j<attempt*79;j++)g.Update(1.0/60,0,0);
            }
            Console.WriteLine("Area "+area+" memory="+cleared+" time="+g.Elapsed.ToString("F1"));
            if(!cleared){ok=false;break;}
            Portal exit=g.Map.Portals[1];
            if(area==3)exit=g.Map.Portals[2];
            if(!Travel(g,exit.At,60*240)){Console.WriteLine("Failed exit "+area);ok=false;break;}
            g.Interact();Console.WriteLine("Transitioned to "+g.Area);
        }
        if(ok){ok=Travel(g,Vec.Center(12,5),6000);if(ok)g.Interact();}
        Console.WriteLine("Completed="+ok+" mode="+g.Mode+" memories="+g.Count+" simulated seconds="+g.Elapsed.ToString("F1"));
        Environment.ExitCode=ok&&g.Mode==Screen.Ending?0:1;
    }
}
