using System;
using System.Collections.Generic;

namespace Scruple
{
    public enum MonsterState { Patrol, Notice, Chase, Search, Return }
    public class Monster
    {
        public Vec Position, LastSeen;
        public MonsterState State=MonsterState.Patrol;
        public readonly Vec[] Route;
        public readonly double Sight;
        public double Clock, Unseen;
        int waypoint;
        List<Vec> path=new List<Vec>();
        Vec goal;
        bool hasGoal;
        double repath;
        int searchStep;
        public Monster(Vec[] route,int area) { Route=route;Position=route[0];LastSeen=Position;Sight=4.0+area*.40; }
        public string StateText { get { return new[]{"순찰","발견!","추격","수색","복귀"}[(int)State]; } }
        void Change(MonsterState s) { State=s;Clock=0;repath=0;hasGoal=false;path.Clear(); }
        public bool Update(Maze map,Vec player,double dt,bool protectedEntry)
        {
            Clock+=dt;repath-=dt;
            bool seen=!protectedEntry && map.CanSee(Position,player,Sight);
            bool alert=false;
            if(seen)
            {
                LastSeen=player;Unseen=0;
                if(State==MonsterState.Patrol || State==MonsterState.Return || State==MonsterState.Search)
                { Change(MonsterState.Notice);alert=true; }
            }
            else Unseen+=dt;
            if(State==MonsterState.Notice)
            { if(Clock>=Tuning.NoticeTime)Change(seen?MonsterState.Chase:MonsterState.Search);return alert; }
            if(State==MonsterState.Chase)
            {
                if(Unseen>=Tuning.LostSightTime){Change(MonsterState.Search);searchStep=0;}
                else Navigate(map,LastSeen,Tuning.ChaseSpeed,dt);
            }
            if(State==MonsterState.Search)
            {
                // 마지막 목격 지점에 도착한 뒤 주변 두 칸을 확인한다. 플레이어 위치는 쓰지 않는다.
                Vec target=LastSeen;
                if(Position.Distance(LastSeen)<.20 || searchStep>0)
                {
                    if(searchStep==0)searchStep=1;
                    int x=LastSeen.Tile.X,y=LastSeen.Tile.Y;
                    int[,] offsets={{1,0},{0,1},{-1,0},{0,-1}};
                    for(int k=0;k<4;k++){int j=(k+searchStep-1)%4;
                        if(map.Walkable(x+offsets[j,0],y+offsets[j,1])){target=Vec.Center(x+offsets[j,0],y+offsets[j,1]);break;}}
                    if(Position.Distance(target)<.15)searchStep++;
                }
                Navigate(map,target,Tuning.PatrolSpeed,dt);
                if(Clock>Tuning.SearchTime)Change(MonsterState.Return);
            }
            if(State==MonsterState.Patrol || State==MonsterState.Return)
            {
                Navigate(map,Route[waypoint],Tuning.PatrolSpeed,dt);
                if(Position.Distance(Route[waypoint])<.1){waypoint=(waypoint+1)%Route.Length;if(State==MonsterState.Return)Change(MonsterState.Patrol);}
            }
            return alert;
        }
        void Navigate(Maze map,Vec target,double speed,double dt)
        {
            // 타일 중심을 경유해 모서리를 잘라서 지나가는 것을 방지한다.
            // 추격 중 재탐색은 현재 경유점에 도달한 뒤 수행해 중심으로 되돌아가는 떨림을 막는다.
            if(path.Count==0 || (repath<=0 && Position.Distance(Vec.Center(Position.Tile.X,Position.Tile.Y))<.05 && (!hasGoal || goal.Tile!=target.Tile)))
            {
                path=map.Path(Position,target);goal=target;hasGoal=true;repath=.25;
                Vec center=Vec.Center(Position.Tile.X,Position.Tile.Y);
                if(Position.Distance(center)>.025)path.Insert(0,center);
                if(path.Count==0 && Position.Tile==target.Tile)path.Add(target);
            }
            double budget=speed*dt;
            while(path.Count>0 && budget>0)
            {
                Vec p=path[0];double d=Position.Distance(p);
                if(d<.015){path.RemoveAt(0);continue;}
                double step=Math.Min(budget,d);Vec old=Position;
                Position=map.Move(Position,(p.X-Position.X)/d*step,(p.Y-Position.Y)/d*step,Tuning.MonsterRadius);
                budget-=step;
                if(Position.Distance(old)<.0001){path.Clear();break;}
                if(Position.Distance(p)<.015)path.RemoveAt(0);
            }
        }
    }
}
