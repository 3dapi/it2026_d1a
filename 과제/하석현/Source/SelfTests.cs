using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Scruple
{
    public static class SelfTests
    {
        static readonly List<string> report=new List<string>();
        static int failed;
        static void Check(bool pass,string label)
        {report.Add((pass?"PASS  ":"FAIL  ")+label);if(!pass)failed++;}
        static bool Reach(Maze m,Vec a,Vec b) {return a.Tile==b.Tile || m.Path(a,b).Count>0;}
        public static int Run()
        {
            report.Clear();failed=0;
            report.Add("Scruple automated verification / "+DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            Maze[] maps=World.Create();
            foreach(Maze m in maps)
            {
                Vec start=m.Id==4?Vec.Center(4,14):Vec.Center(2,16);
                int vertices=0,edges=0;bool all=true;
                for(int x=0;x<25;x++)for(int y=0;y<19;y++)if(m.Walkable(x,y))
                {vertices++;if(m.Walkable(x+1,y))edges++;if(m.Walkable(x,y+1))edges++;if(!Reach(m,start,Vec.Center(x,y)))all=false;}
                Check(all,m.Name+": all walkable tiles reachable with shortcut closed");
                Check(edges-vertices+1>=1,m.Name+": at least one cycle / bypass exists");
                if(m.Id<4)
                {Check(Reach(m,start,m.Memory),m.Name+": memory reachable");Check(Reach(m,start,m.Switch),m.Name+": switch reachable");
                    int before=m.Path(start,m.Switch).Count;m.GateOpen=true;int after=m.Path(start,m.Switch).Count;
                    Check(after<before,m.Name+": shortcut reduces route ("+before+" -> "+after+")");m.GateOpen=false;}
                foreach(Portal p in m.Portals)
                {Check(Reach(m,start,p.At),m.Name+": portal reachable -> "+p.Label);Check(maps[p.Target].Fits(p.Arrival,Tuning.PlayerRadius),"arrival is valid -> "+p.Label);}
                foreach(Vec[] patrol in m.Patrols)for(int k=0;k<patrol.Length;k++)Check(Reach(m,patrol[k],patrol[(k+1)%patrol.Length]),m.Name+": patrol leg "+k);
                Random rng=new Random(201+m.Id);Vec position=start;bool fits=true;
                for(int k=0;k<4000;k++)
                {position=m.Move(position,(rng.NextDouble()-.5)*4,(rng.NextDouble()-.5)*4,Tuning.PlayerRadius);if(!m.Fits(position,Tuning.PlayerRadius))fits=false;}
                Check(fits,m.Name+": 4000 randomized moves stay inside collision geometry");
            }
            // 4방향 LOS는 벽으로 끊기고, 긴 한 프레임 이동도 벽을 건너뛰지 않는다.
            Maze corridor=new Maze(0,"test","","");corridor.Line(2,2,12,2);corridor.Line(8,2,8,8);
            Check(corridor.CanSee(Vec.Center(2,2),Vec.Center(6,2),5),"LOS sees along corridor");
            Check(!corridor.CanSee(Vec.Center(6,2),Vec.Center(8,5),10),"LOS blocked around corner");
            Vec wall=corridor.Move(Vec.Center(3,2),0,9,.21);Check(wall.Y<3,"large dt does not tunnel through wall");
            Monster monster=new Monster(new[]{Vec.Center(3,2),Vec.Center(10,2)},0);
            Check(monster.Update(corridor,Vec.Center(6,2),.01,false)&&monster.State==MonsterState.Notice,"monster enters notice with signal");
            Vec old=monster.Position;monster.Update(corridor,Vec.Center(6,2),.3,false);
            Check(old.Distance(monster.Position)==0,"notice gives reaction time without movement");
            for(int i=0;i<50;i++)monster.Update(corridor,Vec.Center(6,2),.01,false);
            Check(monster.State==MonsterState.Chase,"notice transitions to chase");
            Vec lastSeen=monster.LastSeen;
            for(int i=0;i<130;i++)monster.Update(corridor,Vec.Center(8,8),.01,false);
            Check(monster.State==MonsterState.Search,"losing sight transitions to search");
            Check(monster.LastSeen.Distance(lastSeen)<.001,"hidden player does not update last seen position");
            bool returned=false,patrolled=false,inside=true;
            for(int i=0;i<1800;i++)
            {monster.Update(corridor,Vec.Center(8,8),.01,false);returned|=monster.State==MonsterState.Return;patrolled|=monster.State==MonsterState.Patrol;inside&=corridor.Fits(monster.Position,.22);}
            Check(returned&&patrolled,"search returns to patrol");Check(inside,"monster navigation remains on floor");
            GameSession game=new GameSession();game.NewGame();game.SetMode(Screen.Playing);
            game.Update(.1,1,0);game.Pause();Vec paused=game.Player;Vec pausedMonster=game.Monsters[0].Position;
            for(int i=0;i<60;i++)game.Update(.016,1,1);
            Check(game.Player.Distance(paused)==0 && game.Monsters[0].Position.Distance(pausedMonster)==0,"pause freezes player and monsters");
            game.Pause();game.LoseFocus();Check(game.Mode==Screen.Paused,"focus loss pauses play");
            GameSession a=new GameSession(),b=new GameSession();a.Enter(4,Vec.Center(12,10));b.Enter(4,Vec.Center(12,10));
            for(int i=0;i<60;i++)a.Update(1.0/60,0,-1);for(int i=0;i<120;i++)b.Update(1.0/120,0,-1);
            Check(a.Player.Distance(b.Player)<.000001,"movement consistent at 60 and 120 Hz");
            game=new GameSession();game.NewGame();game.SetMode(Screen.Playing);game.Player=game.Map.Memory;game.Interact();
            Check(game.Count==1 && game.Mode==Screen.Dialogue,"memory collected and dialogue opened");
            game.Enter(1,Vec.Center(3,2));game.Memories[1]=true;game.Map.GateOpen=true;game.Retry();
            Check(game.Memories[0]&&!game.Memories[1]&&!game.Map.GateOpen,"retry restores exact area-entry snapshot");
            game.Enter(4,Vec.Center(12,5));game.Interact();Check(game.Mode==Screen.Confirm,"incomplete memories require explicit choice");
            game.AcceptIncomplete();Check(game.Mode==Screen.Nightmare,"explicit incomplete ending reachable");
            game.Memories=new[]{true,true,true,true};game.Enter(4,Vec.Center(12,5));game.Interact();Check(game.Mode==Screen.Ending,"four memories unlock true ending");
            for(int i=0;i<3;i++){game.ModeTime=3;game.Advance();}Check(game.EndingPage==3,"true ending reaches dawn");
            game=new GameSession();game.Enter(0,Vec.Center(18,6));game.SafeTime=0;game.Update(.016,0,0);
            Check(game.Mode==Screen.Caught,"contact triggers caught ending");
            game.Retry();Check(game.Mode==Screen.Playing&&game.SafeTime>0,"retry grants temporary entry protection");
            string baseDir=AppDomain.CurrentDomain.BaseDirectory,dir=Path.Combine(baseDir,"Tests");Directory.CreateDirectory(dir);
            using(Audio audio=new Audio(Path.Combine(baseDir,"Assets","Audio")))
            {
                for(int i=0;i<30;i++)audio.Update(.1,Screen.Playing,false);
                Check(audio.Status("music")=="playing","MCI music channel reports playing");
                foreach(string effect in new[]{"click","memory","door","alert","fail","pulse"})
                {audio.Effect(effect);Check(audio.Status("sfx_"+effect)=="playing","MCI effect reports playing: "+effect);}
                audio.Muted=true;audio.Update(.1,Screen.Playing,false);
                Check(audio.Status("sfx_pulse")!="playing","mute stops effects");
                for(int i=0;i<30;i++)audio.Update(.1,Screen.Ending,false);
                Check(audio.Status("music")=="playing","ending track transition opens playback");
                Check(audio.Errors.Count==0,"MCI commands complete without errors");foreach(string error in audio.Errors)report.Add("AUDIO ERROR "+error);
            }
            // 이 프레임들은 지정한 상태의 렌더 검증이며 실제 플레이 완료 증거와 구분한다.
            using(GameForm form=new GameForm())
            {
                form.Game.NewGame();form.Game.ModeTime=1;
                System.Reflection.MethodInfo keyHandler=typeof(GameForm).GetMethod("OnKey",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
                keyHandler.Invoke(form,new object[]{form,new System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.E)});
                form.Game.ModeTime=1;
                keyHandler.Invoke(form,new object[]{form,new System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.E)});
                Check(form.Game.DialoguePage==1 && form.Game.Mode==Screen.Dialogue,"held E repeats do not skip a second dialogue page");
                System.Reflection.MethodInfo releaseHandler=typeof(System.Windows.Forms.Control).GetMethod("OnKeyUp",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
                releaseHandler.Invoke(form,new object[]{new System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.E)});
                keyHandler.Invoke(form,new object[]{form,new System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.E)});
                Check(form.Game.Mode==Screen.Playing,"release and repress E advances dialogue normally");
                form.Game.SetMode(Screen.Title);
                form.CreateControl();form.SaveFrame(Path.Combine(dir,"01-title.png"));
                form.Game.NewGame();form.Game.SetMode(Screen.Playing);form.Game.ToastTime=0;form.SaveFrame(Path.Combine(dir,"02-play.png"));
                form.Game.Player=form.Game.Map.Memory;form.Game.Interact();form.SaveFrame(Path.Combine(dir,"03-memory.png"));
                form.Game.SetMode(Screen.Ending);form.Game.EndingPage=2;form.Game.ModeTime=3;form.SaveFrame(Path.Combine(dir,"04-ending.png"));
                form.Game.EndingPage=3;form.SaveFrame(Path.Combine(dir,"05-dawn.png"));
                form.Game.SetMode(Screen.Journal);form.SaveFrame(Path.Combine(dir,"06-journal.png"));
                form.Game.SetMode(Screen.Settings);form.Game.ReturnScreen=Screen.Playing;form.SaveFrame(Path.Combine(dir,"07-settings.png"));
            }
            report.Add("TOTAL: "+failed+" failures");File.WriteAllLines(Path.Combine(dir,"automated-results.txt"),report.ToArray(),System.Text.Encoding.UTF8);
            return failed==0?0:1;
        }
    }
}
