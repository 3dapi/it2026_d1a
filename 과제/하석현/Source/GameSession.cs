using System;
using System.Collections.Generic;

namespace Scruple
{
    public enum Screen { Title, Playing, Dialogue, Paused, Journal, Settings, Confirm, Caught, Nightmare, Ending }

    public static class Story
    {
        public static readonly string[] Titles={"대답하지 않은 밤","네 이름으로 고른 미래","괜찮다는 거짓말","마지막 문장"};
        public static readonly string[][] Memories={
            new[]{"그녀  ·  오늘 잠깐 통화할 수 있을까?\n\n나  ·  이것만 끝내고. 금방 연락할게.\n\n전화기 옆에 놓인 두 잔의 차. 한 잔만 식어 갔다.","나  ·  대답을 미룬 건 한 번이었다고 생각했어.\n\n그녀  ·  기다리는 나에게는, 매번 오늘이었어.\n\n바빴던 하루와 외로웠던 하루는 같은 길이였을까."},
            new[]{"그녀  ·  먼 도시에서 공부해 보고 싶어.\n\n나  ·  여기서도 할 수 있잖아. 너를 위해서 하는 말이야.\n\n접힌 지원서에는, 내가 아닌 그녀의 이름이 적혀 있었다.","나  ·  함께 있고 싶다는 말을 그렇게 했구나.\n\n그녀  ·  내 곁에 있어 주길 바랐어. 내 자리를 정해 주길 바란 건 아니야.\n\n사랑한다는 말 뒤에, 두려움을 숨겼다."},
            new[]{"그녀  ·  우리, 어제 일 이야기할까?\n\n나  ·  이제 괜찮아. 또 싸우고 싶지 않아.\n\n우리는 서로를 다치게 하지 않으려고 입을 닫았다.","그녀  ·  나도 괜찮다고 했어. 사실은 아니었는데.\n\n나  ·  침묵이 우리를 지켜 주는 줄 알았어.\n\n하지 않은 말들이, 두 사람 사이에 벽이 되었다."},
            new[]{"그녀  ·  나는 이제, 여기까지인 것 같아.\n\n나  ·  조금만 기다려. 다 예전처럼 해 놓을게.\n\n그날도 나는, 그녀의 말이 끝나기 전에 대답했다.","그녀  ·  우리 시간이 전부 아팠던 건 아니야.\n\n나  ·  돌아와 달라는 말만 준비하느라, 네 마지막 인사도 듣지 못했네.\n\n접힌 편지의 끝에는 원망 대신, 고맙다는 말이 있었다."}
        };
        public static readonly string[] Ending={
            "나  ·  널 찾으면, 돌아갈 수 있을 줄 알았어.\n\n그녀  ·  그럼 지금은?",
            "나  ·  이제는… 네가 왜 떠났는지 알 것 같아.\n\n그녀  ·  늦었지만, 내 말을 들어 줘서 고마워.",
            "마지막으로 서로를 안았다.\n손끝에 닿은 머리카락이 꽃잎처럼 가벼워졌다.\n\n나  ·  함께해 줘서 고마워.\n그녀  ·  너의 내일은, 너의 것이길.",
            "눈을 뜨자 아침이었다.\n\n그녀는 돌아오지 않았다.\n그래도, 창문을 열 수 있을 것 같았다."
        };
    }

    // 진행 규칙은 Windows Forms에 의존하지 않아서 콘솔 테스트에서도 실행할 수 있다.
    public class GameSession
    {
        public Maze[] Maps=World.Create();
        public Maze Map { get { return Maps[Area]; } }
        public int Area;
        public Vec Player=Vec.Center(2,16);
        public readonly List<Monster> Monsters=new List<Monster>();
        public bool[] Memories=new bool[4];
        public Screen Mode=Screen.Title, ReturnScreen=Screen.Title;
        public string[] DialoguePages=new string[0];
        public string DialogueTitle="";
        public int DialoguePage, EndingPage;
        public double ModeTime, Elapsed, SafeTime, WalkPhase;
        public string Toast="";
        public double ToastTime;
        public event Action<string> Sound;
        bool[] savedMemories, savedGates;
        int savedArea;
        Vec savedPosition;
        public int Count { get { int c=0;foreach(bool b in Memories)if(b)c++;return c; } }
        public bool Threat { get { return Monsters.Exists(m=>m.State==MonsterState.Chase || m.State==MonsterState.Notice); } }
        public void SetMode(Screen s) { Mode=s;ModeTime=0; }
        void Play(string name) { if(Sound!=null)Sound(name); }
        public void NewGame()
        {
            Maps=World.Create();Memories=new bool[4];Elapsed=0;EndingPage=0;
            Enter(0,Vec.Center(2,16));
            ShowDialogue("잊혀진 미로 속에서, 다시",new[]{
                "헤어진 뒤로 같은 꿈을 꾸었다.\n\n무너진 회랑 끝에서, 누군가 나를 기다리고 있었다.\n이름을 부르려 했지만, 기억나는 것은 하지 못한 말들뿐이었다.",
                "네 개의 기억을 찾아, 그녀에게 가자.\n\nWASD  이동     E  조사·문·대화\nJ  모은 기억     Tab  연결 지도     Esc  일시정지\n\n괴물의 붉은 신호를 보면 모퉁이를 돌아 시야를 끊자.\n죽어도 구역 입구에서 다시 시작할 수 있다."});
        }
        public void Enter(int area,Vec position)
        {
            Area=area;Player=position;SpawnMonsters();SafeTime=2.5;SetMode(Screen.Playing);
            savedArea=area;savedPosition=position;savedMemories=(bool[])Memories.Clone();
            savedGates=new bool[4];for(int i=0;i<4;i++)savedGates[i]=Maps[i].GateOpen;
            Notify(Map.Name+"에 도착했습니다.");
        }
        void SpawnMonsters() { Monsters.Clear();foreach(Vec[] p in Map.Patrols)Monsters.Add(new Monster(p,Area)); }
        public void Retry()
        {
            Memories=(bool[])savedMemories.Clone();for(int i=0;i<4;i++)Maps[i].GateOpen=savedGates[i];
            Enter(savedArea,savedPosition);Play("door");
        }
        public void Notify(string text) { Toast=text;ToastTime=4; }
        public void ShowDialogue(string title,string[] pages)
        { DialogueTitle=title;DialoguePages=pages;DialoguePage=0;SetMode(Screen.Dialogue); }
        public void Advance()
        {
            if(ModeTime<.25)return;
            if(Mode==Screen.Dialogue)
            { DialoguePage++;ModeTime=0;if(DialoguePage>=DialoguePages.Length)SetMode(Screen.Playing);Play("click"); }
            else if(Mode==Screen.Ending && ModeTime>=2)
            { if(EndingPage<3){EndingPage++;ModeTime=0;Play("click");} }
        }
        public void Pause()
        { if(Mode==Screen.Playing){SetMode(Screen.Paused);}else if(Mode==Screen.Paused)SetMode(Screen.Playing); }
        public void LoseFocus()
        { if(Mode==Screen.Playing)SetMode(Screen.Paused); }
        public void Update(double dt,double horizontal,double vertical)
        {
            dt=Math.Max(0,Math.Min(dt,.1));ModeTime+=dt;
            if(Mode!=Screen.Playing)return;
            Elapsed+=dt;SafeTime=Math.Max(0,SafeTime-dt);ToastTime=Math.Max(0,ToastTime-dt);
            double len=Math.Sqrt(horizontal*horizontal+vertical*vertical);
            if(len>0)
            {
                Vec before=Player;
                Player=Map.Move(Player,horizontal/len*Tuning.PlayerSpeed*dt,vertical/len*Tuning.PlayerSpeed*dt,Tuning.PlayerRadius);
                if(Player.Distance(before)>.001)WalkPhase+=dt*10;
            }
            foreach(Monster m in Monsters)
            {
                if(m.Update(Map,Player,dt,SafeTime>0))Play("alert");
                if(SafeTime<=0 && Player.Distance(m.Position)<Tuning.CatchDistance)
                { SetMode(Screen.Caught);Play("fail");break; }
            }
        }
        public bool Near(Vec p) { return Player.Distance(p)<Tuning.InteractionRange && Map.CanSee(Player,p,2); }
        public string InteractionHint()
        {
            if(Area<4 && !Memories[Area] && Near(Map.Memory))return "E  ·  기억 조각 읽기";
            if(Area<4 && !Map.GateOpen && Near(Map.Switch))return "E  ·  빗장을 열어 입구와 연결하기";
            if(Area==4 && Near(Vec.Center(12,5)))return "E  ·  그녀에게 마지막 말을 건네기";
            foreach(Portal p in Map.Portals)if(Near(p.At))
                return p.ShortcutOnly&&!Map.GateOpen?"중앙 문이 잠겨 있습니다 · 금빛 빗장을 찾으세요":"E  ·  "+p.Label+"으로 이동";
            return "";
        }
        public void Interact()
        {
            if(Mode!=Screen.Playing)return;
            if(Area<4 && !Memories[Area] && Near(Map.Memory))
            { Memories[Area]=true;Play("memory");ShowDialogue("기억 "+(Area+1)+"  /  "+Story.Titles[Area],Story.Memories[Area]);return; }
            if(Area<4 && !Map.GateOpen && Near(Map.Switch))
            { Map.GateOpen=true;Play("door");Notify("빗장이 열렸다. 아래쪽 입구와 중앙의 새벽길이 이어졌다.");return; }
            if(Area==4 && Near(Vec.Center(12,5)))
            { if(Count==4)BeginEnding();else SetMode(Screen.Confirm);return; }
            foreach(Portal p in Map.Portals)if(Near(p.At))
            {
                if(p.ShortcutOnly && !Map.GateOpen){Notify("금빛 빗장을 열면 중앙의 문도 깨어납니다.");return;}
                Play("door");Enter(p.Target,p.Arrival);return;
            }
        }
        public void BeginEnding() { EndingPage=0;SetMode(Screen.Ending); }
        public void AcceptIncomplete() { if(Mode==Screen.Confirm){SetMode(Screen.Nightmare);Play("fail");} }
    }
}
