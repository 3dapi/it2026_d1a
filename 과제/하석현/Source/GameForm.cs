using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Scruple
{
    public class UiButton
    {
        public RectangleF Rect;
        public string Text;
        public Action Action;
        public UiButton(RectangleF rect,string text,Action action){Rect=rect;Text=text;Action=action;}
    }

    public sealed class GameForm : Form
    {
        public readonly GameSession Game=new GameSession();
        public readonly Audio Audio;
        readonly Dictionary<string,Image> art=new Dictionary<string,Image>();
        readonly HashSet<Keys> held=new HashSet<Keys>();
        readonly HashSet<Keys> pressed=new HashSet<Keys>();
        readonly List<UiButton> buttons=new List<UiButton>();
        readonly Dictionary<string,Font> fonts=new Dictionary<string,Font>();
        readonly Timer timer=new Timer();
        readonly Stopwatch clock=Stopwatch.StartNew();
        double last,ambient;
        float scale=1,offsetX,offsetY;
        PointF mouse=new PointF(-1,-1);
        int selected, journalPage;
        bool showWorld;
        string buttonContext="";
        const int Tile=30, BoardX=447, BoardY=132;
        readonly Color Ink=Color.FromArgb(234,224,239),Muted=Color.FromArgb(160,151,177),Rose=Color.FromArgb(232,139,177),Gold=Color.FromArgb(231,198,143);

        public GameForm()
        {
            Text="스크러플 · 잊혀진 미로 속에서, 다시";
            ClientSize=new Size(1280,800);MinimumSize=new Size(976,640);StartPosition=FormStartPosition.CenterScreen;
            DoubleBuffered=true;KeyPreview=true;BackColor=Color.FromArgb(12,12,23);
            string root=AppDomain.CurrentDomain.BaseDirectory;
            foreach(string n in new[]{"title","ending","world","portrait","player","her","monster"})
                art[n]=Image.FromFile(Path.Combine(root,"Assets","Art",n+".png"));
            Audio=new Audio(Path.Combine(root,"Assets","Audio"));Game.Sound+=Audio.Effect;
            KeyDown+=OnKey;KeyUp+=(s,e)=>{held.Remove(e.KeyCode);pressed.Remove(e.KeyCode);};
            Deactivate+=(s,e)=>{held.Clear();pressed.Clear();Game.LoseFocus();};
            MouseMove+=(s,e)=>{mouse=new PointF((e.X-offsetX)/scale,(e.Y-offsetY)/scale);Invalidate();};
            MouseDown+=(s,e)=>
            {
                if(e.Button!=MouseButtons.Left)return;
                PointF p=new PointF((e.X-offsetX)/scale,(e.Y-offsetY)/scale);
                foreach(UiButton b in buttons.ToArray())if(b.Rect.Contains(p)){held.Clear();Audio.Effect("click");b.Action();Invalidate();break;}
            };
            timer.Interval=16;timer.Tick+=(s,e)=>TickFrame();timer.Start();
        }
        void TickFrame()
        {
            double now=clock.Elapsed.TotalSeconds,dt=Math.Min(.05,now-last);last=now;ambient+=dt;
            double dx=(held.Contains(Keys.D)?1:0)-(held.Contains(Keys.A)?1:0);
            double dy=(held.Contains(Keys.S)?1:0)-(held.Contains(Keys.W)?1:0);
            Game.Update(dt,dx,dy);Audio.Update(dt,Game.Mode,Game.Threat);Invalidate();
        }
        void Change(Screen mode) { held.Clear();Game.SetMode(mode);selected=0; }
        void Settings() { Game.ReturnScreen=Game.Mode;Change(Screen.Settings); }
        void OnKey(object sender,KeyEventArgs e)
        {
            e.SuppressKeyPress=true;if(!pressed.Add(e.KeyCode))return;held.Add(e.KeyCode);
            if(e.KeyCode==Keys.M){Audio.Muted=!Audio.Muted;return;}
            if(e.KeyCode==Keys.Escape)
            {
                held.Clear();
                if(Game.Mode==Screen.Playing || Game.Mode==Screen.Paused)Game.Pause();
                else if(Game.Mode==Screen.Journal || Game.Mode==Screen.Confirm)Change(Screen.Playing);
                else if(Game.Mode==Screen.Settings)Change(Game.ReturnScreen);
                return;
            }
            if(Game.Mode==Screen.Playing)
            {
                if(e.KeyCode==Keys.E){Game.Interact();held.Clear();}
                else if(e.KeyCode==Keys.J){journalPage=0;Change(Screen.Journal);}
                else if(e.KeyCode==Keys.Tab)showWorld=!showWorld;
                return;
            }
            if(Game.Mode==Screen.Dialogue && (e.KeyCode==Keys.E || e.KeyCode==Keys.Enter || e.KeyCode==Keys.Space))
            {Game.Advance();return;}
            if(Game.Mode==Screen.Ending && Game.EndingPage<3 && (e.KeyCode==Keys.E || e.KeyCode==Keys.Enter || e.KeyCode==Keys.Space))
            {Game.Advance();return;}
            if(Game.Mode==Screen.Journal && (e.KeyCode==Keys.Left || e.KeyCode==Keys.Right))
            {journalPage=(journalPage+(e.KeyCode==Keys.Right?1:3))%4;return;}
            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down || e.KeyCode==Keys.Tab)
            {if(buttons.Count>0)selected=(selected+(e.KeyCode==Keys.Up?buttons.Count-1:1))%buttons.Count;return;}
            if(e.KeyCode==Keys.Enter && buttons.Count>0)
            {buttons[Math.Min(selected,buttons.Count-1)].Action();held.Clear();Audio.Effect("click");}
        }
        Font FontAt(float size,bool serif=false,bool bold=false)
        {
            string key=size+"/"+serif+"/"+bold;
            if(!fonts.ContainsKey(key))fonts[key]=new Font(serif?"바탕":"맑은 고딕",size,bold?FontStyle.Bold:FontStyle.Regular,GraphicsUnit.Pixel);
            return fonts[key];
        }
        void TextAt(Graphics g,string text,float x,float y,float size,Color color,float width=1000,float height=80,bool serif=false,bool bold=false)
        {
            using(SolidBrush b=new SolidBrush(color))
            using(StringFormat sf=new StringFormat()){sf.Trimming=StringTrimming.EllipsisCharacter;g.DrawString(text,FontAt(size,serif,bold),b,new RectangleF(x,y,width,height),sf);}
        }
        void CenterText(Graphics g,string text,float y,float size,Color color,bool serif=false)
        {using(SolidBrush b=new SolidBrush(color))using(StringFormat sf=new StringFormat()){sf.Alignment=StringAlignment.Center;g.DrawString(text,FontAt(size,serif),b,new RectangleF(0,y,1280,90),sf);}}
        void Fill(Graphics g,Color color,float x,float y,float w,float h)
        {using(SolidBrush b=new SolidBrush(color))g.FillRectangle(b,x,y,w,h);}
        void Line(Graphics g,Color color,float x,float y,float ex,float ey,float width=1)
        {using(Pen p=new Pen(color,width))g.DrawLine(p,x,y,ex,ey);}
        void Panel(Graphics g,float x,float y,float w,float h,int alpha=230)
        {Fill(g,Color.FromArgb(alpha,19,19,33),x,y,w,h);using(Pen p=new Pen(Color.FromArgb(80,155,123,161)))g.DrawRectangle(p,x,y,w,h);}
        void Cover(Graphics g,Image img,RectangleF box,float opacity=1)
        {
            float s=Math.Max(box.Width/img.Width,box.Height/img.Height);
            RectangleF src=new RectangleF((img.Width-box.Width/s)/2,(img.Height-box.Height/s)/2,box.Width/s,box.Height/s);
            if(opacity>=.999f)g.DrawImage(img,box,src,GraphicsUnit.Pixel);
            else using(ImageAttributes ia=new ImageAttributes())
            {ColorMatrix matrix=new ColorMatrix();matrix.Matrix33=opacity;ia.SetColorMatrix(matrix);g.DrawImage(img,Rectangle.Round(box),src.X,src.Y,src.Width,src.Height,GraphicsUnit.Pixel,ia);}
        }
        void Sprite(Graphics g,string name,float cx,float foot,float width,float height,float alpha=1)
        {
            Image img=art[name];float s=Math.Min(width/img.Width,height/img.Height);
            Rectangle r=Rectangle.Round(new RectangleF(cx-img.Width*s/2,foot-img.Height*s,img.Width*s,img.Height*s));
            using(ImageAttributes ia=new ImageAttributes()){ColorMatrix cm=new ColorMatrix();cm.Matrix33=alpha;ia.SetColorMatrix(cm);g.DrawImage(img,r,0,0,img.Width,img.Height,GraphicsUnit.Pixel,ia);}
        }
        void Glow(Graphics g,float x,float y,float radius,Color color)
        {
            using(GraphicsPath p=new GraphicsPath())
            {p.AddEllipse(x-radius,y-radius,radius*2,radius*2);using(PathGradientBrush b=new PathGradientBrush(p))
                {b.CenterColor=color;b.SurroundColors=new[]{Color.FromArgb(0,color)};g.FillPath(b,p);}}
        }
        void Button(Graphics g,string text,float x,float y,float w,Action action)
        {
            RectangleF r=new RectangleF(x,y,w,44);bool active=buttons.Count==selected || r.Contains(mouse);
            Fill(g,active?Color.FromArgb(100,116,61,90):Color.FromArgb(190,28,26,43),x,y,w,44);
            using(Pen p=new Pen(active?Rose:Color.FromArgb(72,63,85)))g.DrawRectangle(p,x,y,w,44);
            TextAt(g,text,x+18,y+10,17,active?Ink:Muted,w-30,30);
            buttons.Add(new UiButton(r,text,action));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);Graphics g=e.Graphics;g.Clear(Color.FromArgb(8,8,15));
            scale=Math.Min(ClientSize.Width/1280f,ClientSize.Height/800f);
            offsetX=(ClientSize.Width-1280*scale)/2;offsetY=(ClientSize.Height-800*scale)/2;
            g.TranslateTransform(offsetX,offsetY);g.ScaleTransform(scale,scale);
            g.SmoothingMode=SmoothingMode.AntiAlias;g.InterpolationMode=InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint=System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            string context=Game.Mode+"/"+(Game.Mode==Screen.Ending?Game.EndingPage:0);
            if(context!=buttonContext){selected=0;buttonContext=context;}
            buttons.Clear();
            if(Game.Mode==Screen.Title || (Game.Mode==Screen.Settings && Game.ReturnScreen==Screen.Title))DrawTitle(g);
            else if(Game.Mode==Screen.Ending)DrawEnding(g);
            else DrawPlay(g);
            if(Game.Mode==Screen.Dialogue)DrawDialogue(g);
            if(Game.Mode==Screen.Paused)DrawPause(g);
            if(Game.Mode==Screen.Settings)DrawSettings(g);
            if(Game.Mode==Screen.Journal)DrawJournal(g);
            if(Game.Mode==Screen.Confirm)DrawConfirm(g);
            if(Game.Mode==Screen.Caught || Game.Mode==Screen.Nightmare)DrawFailure(g);
            if(Game.Mode!=Screen.Title && Game.Mode!=Screen.Ending)
                TextAt(g,Audio.Muted?"M  소리 꺼짐":"M  소리 켜짐",1130,760,12,Muted,140,22);
        }
        void DrawTitle(Graphics g)
        {
            Cover(g,art["title"],new RectangleF(0,0,1280,800));
            using(LinearGradientBrush b=new LinearGradientBrush(new Rectangle(0,0,1280,800),Color.FromArgb(15,5,5,12),Color.FromArgb(110,6,6,16),90))g.FillRectangle(b,0,0,1280,800);
            TextAt(g,"S C R U P L E   /   A SMALL GAME ABOUT GOODBYE",38,27,12,Ink);
            // 원본 일러스트에 그려진 메뉴 영역 전체를 실제 메뉴 패널로 덮는다.
            Panel(g,419,339,442,246,253);
            TextAt(g,"꿈이 끝나는 곳까지",461,358,17,Muted,360,28,true);
            if(Game.Mode==Screen.Title)
            {
                Button(g,"꿈속으로 들어가기    →",459,404,362,()=>Game.NewGame());
                Button(g,"설정 · 조작 방법",459,462,362,Settings);
                Button(g,"종료",459,520,362,Close);
            }
            Petals(g,32,.5f);
            CenterText(g,"잃어버린 네 문장. 그리고, 마지막 인사.",717,17,Ink,true);
            CenterText(g,"26031033 하석현  ·  객체지향프로그래밍  ·  WEEK 04",759,12,Muted);
        }
        PointF At(Vec p) { return new PointF(BoardX+(float)p.X*Tile,BoardY+(float)p.Y*Tile); }
        void DrawPlay(Graphics g)
        {
            Cover(g,art["world"],new RectangleF(0,0,1280,800),.28f);
            Fill(g,Color.FromArgb(155,9,11,23),0,0,1280,800);
            TextAt(g,"S C R U P L E",38,26,15,Rose,300,30);
            TextAt(g,"잊혀진 미로 속에서, 다시",38,58,14,Muted);
            Line(g,Color.FromArgb(67,65,87),38,96,1242,96);
            TextAt(g,Game.Map.Subtitle,440,37,12,Gold,750,25);
            TextAt(g,Game.Map.Name,440,62,25,Ink,500,44,true);
            TextAt(g,(Game.Threat?"●  후회가 당신을 보았습니다":"○  서두르지 말고, 길을 읽으세요"),909,66,13,Game.Threat?Rose:Muted,340,35);
            Panel(g,37,122,348,583,220);
            Cover(g,art["portrait"],new RectangleF(38,123,346,214),.75f);
            using(LinearGradientBrush b=new LinearGradientBrush(new Rectangle(38,238,346,100),Color.Transparent,Color.FromArgb(255,19,19,33),90))g.FillRectangle(b,38,238,346,100);
            TextAt(g,"그녀를 찾아서",60,315,26,Ink,300,42,true);
            TextAt(g,"이별의 기억",61,369,14,Muted,240,28);
            TextAt(g,Game.Count+" / 4",277,358,29,Rose,95,50);
            for(int i=0;i<4;i++)
            {
                float y=411+i*40;
                TextAt(g,Game.Memories[i]?"◆":"◇",63,y,17,Game.Memories[i]?Rose:Muted,30,30);
                TextAt(g,Game.Memories[i]?Story.Titles[i]:"아직 닿지 않은 기억",93,y+1,15,Game.Memories[i]?Ink:Muted,270,28);
            }
            Line(g,Color.FromArgb(62,57,77),60,583,361,583);
            TextAt(g,Game.Map.Hint,61,602,14,Muted,295,79);
            Panel(g,417,115,811,603,175);
            DrawMaze(g);
            if(showWorld)DrawWorld(g);
            string hint=Game.InteractionHint();
            if(Game.Mode==Screen.Playing && hint!="")
            {Panel(g,443,673,758,40,247);TextAt(g,hint,462,683,15,Gold,730,26);}
            if(Game.Mode==Screen.Playing && Game.ToastTime>0)
            {Panel(g,435,724,774,29,230);TextAt(g,Game.Toast,448,729,13,Ink,756,23);}
            TextAt(g,"WASD 이동     E 조사·문     J 기억     Tab 연결 지도     Esc 일시정지",38,759,13,Muted,950,28);
        }
        void DrawMaze(Graphics g)
        {
            Maze m=Game.Map;
            // 통로 밖의 낮은 폐허와 기둥: 지형보다 어둡게 그려 이동 경계를 흐리지 않는다.
            for(int y=3;y<16;y++)for(int x=3;x<22;x++)
            {
                if(m.Floor[x,y] || (x*11+y*7)%9!=0)continue;
                if(!m.Floor[x-1,y]&&!m.Floor[x+1,y]&&!m.Floor[x,y-1]&&!m.Floor[x,y+1])continue;
                float px=BoardX+x*Tile,py=BoardY+y*Tile;
                Fill(g,Color.FromArgb(29,28,44),px+8,py+7,15,25);
                Fill(g,Color.FromArgb(42,37,56),px+6,py+4,19,6);
                Line(g,Color.FromArgb(65,53,73),px+9,py+7,px+9,py+27);
                using(Pen p=new Pen(Color.FromArgb(56,50,75)))g.DrawArc(p,px+9,py+12,11,14,180,180);
                Fill(g,Color.FromArgb(68,31,52),px+19,py+17,4,4);
            }
            // 바닥의 테두리가 충돌 경계다. 장식용 배경은 실제 통로 아래에만 놓인다.
            for(int y=0;y<Maze.Height;y++)for(int x=0;x<Maze.Width;x++)if(m.Floor[x,y])
            {
                float px=BoardX+x*Tile,py=BoardY+y*Tile;
                int tone=(x*17+y*31+m.Id*11)%15;
                Fill(g,Color.FromArgb(17,15,29),px+3,py+5,Tile,Tile);
                Fill(g,Color.FromArgb(55+tone,50+tone,73+tone),px,py,Tile,Tile);
                Line(g,Color.FromArgb(91,86,110),px+1,py+1,px+Tile-1,py+1);
                Line(g,Color.FromArgb(42,38,60),px,py+Tile-1,px+Tile,py+Tile-1);
                Line(g,Color.FromArgb(45,43,63),px+Tile-1,py,px+Tile-1,py+Tile);
                Line(g,Color.FromArgb(38,37,55),px+((y%2)*10+7),py+2,px+((y%2)*10+7),py+14);
                Line(g,Color.FromArgb(38,37,55),px+1,py+15,px+29,py+15);
                Line(g,Color.FromArgb(38,37,55),px+((y%2)*10+3),py+16,px+((y%2)*10+3),py+28);
                if((x*13+y*17)%7==0){Line(g,Color.FromArgb(38,30,47),px+18,py+3,px+13,py+11);Line(g,Color.FromArgb(38,30,47),px+13,py+11,px+15,py+14);}
                Color edge=Color.FromArgb(150,134,153,180);
                if(x==0||!m.Floor[x-1,y])Line(g,edge,px,py,px,py+Tile,2);
                if(x==24||!m.Floor[x+1,y])Line(g,edge,px+Tile,py,px+Tile,py+Tile,2);
                if(y==0||!m.Floor[x,y-1])Line(g,edge,px,py,px+Tile,py,2);
                if(y==18||!m.Floor[x,y+1])Line(g,edge,px,py+Tile,px+Tile,py+Tile,2);
                if((x*7+y*13)%23==0)
                {Glow(g,px+15,py+13,28,Color.FromArgb(97,255,184,125));Fill(g,Color.FromArgb(32,25,39),px+10,py+5,10,16);Fill(g,Gold,px+13,py+7,4,9);Line(g,Gold,px+10,py+4,px+15,py-1);Line(g,Gold,px+15,py-1,px+20,py+4);}
            }
            if(m.Id<4)
            {
                PointF gate=At(m.Gate),lever=At(m.Switch);
                if(!m.GateOpen)
                {Fill(g,Color.FromArgb(35,30,42),gate.X-14,gate.Y-14,28,28);for(int j=-10;j<=10;j+=5)Line(g,Gold,gate.X+j,gate.Y-13,gate.X+j,gate.Y+13,2);}
                else{Line(g,Gold,gate.X-12,gate.Y-10,gate.X-12,gate.Y+12,2);Line(g,Gold,gate.X+12,gate.Y-10,gate.X+12,gate.Y+12,2);}
                Glow(g,lever.X,lever.Y,20,Color.FromArgb(100,Gold));
                TextAt(g,m.GateOpen?"✓":"⚿",lever.X-9,lever.Y-13,21,Gold,35,35);
                if(!Game.Memories[m.Id])
                {
                    PointF p=At(m.Memory);float pulse=(float)Math.Sin(ambient*2)*3;
                    Glow(g,p.X,p.Y,37+pulse,Color.FromArgb(175,235,116,181));
                    using(SolidBrush b=new SolidBrush(Rose))g.FillPolygon(b,new[]{new PointF(p.X,p.Y-12),new PointF(p.X+7,p.Y),new PointF(p.X,p.Y+12),new PointF(p.X-7,p.Y)});
                    Line(g,Ink,p.X,p.Y-8,p.X,p.Y+6);
                }
            }
            foreach(Portal p in m.Portals)
            {
                PointF at=At(p.At);bool locked=p.ShortcutOnly&&!m.GateOpen;
                Color c=locked?Muted:Color.FromArgb(180,171,240);
                if(!locked)Glow(g,at.X,at.Y,27,Color.FromArgb(110,c));
                using(Pen pen=new Pen(c,2))g.DrawEllipse(pen,at.X-11,at.Y-11,22,22);
                TextAt(g,(p.Target+1).ToString(),at.X-5,at.Y-10,14,c,23,23);
            }
            if(m.Id==4)
            {
                PointF p=At(Vec.Center(12,5));Glow(g,p.X,p.Y,85,Color.FromArgb(155,196,139,228));
                Sprite(g,"her",p.X,p.Y+10,52,67);
                TextAt(g,"마지막 인사",p.X-46,p.Y-77,15,Ink,140,30,true);
            }
            foreach(Monster monster in Game.Monsters)
            {
                PointF p=At(monster.Position);bool danger=monster.State==MonsterState.Chase || monster.State==MonsterState.Notice;
                Glow(g,p.X,p.Y,31,Color.FromArgb(danger?150:65,224,65,116));
                using(Pen pen=new Pen(danger?Rose:Muted,1.5f))g.DrawEllipse(pen,p.X-9,p.Y-6,18,12);
                Sprite(g,"monster",p.X,p.Y+10+(float)Math.Sin(ambient*4)*2,47,49);
                if(monster.State!=MonsterState.Patrol)
                {Fill(g,Color.FromArgb(225,20,16,30),p.X-24,p.Y-43,49,18);TextAt(g,monster.StateText,p.X-20,p.Y-43,12,danger?Rose:Gold,65,25);}
            }
            PointF player=At(Game.Player);
            Glow(g,player.X,player.Y,32,Color.FromArgb(120,182,198,245));
            using(Pen pen=new Pen(Color.FromArgb(214,213,235),2))g.DrawEllipse(pen,player.X-8,player.Y-5,16,10);
            Sprite(g,"player",player.X,player.Y+11+(float)Math.Sin(Game.WalkPhase)*1.5f,43,50,Game.SafeTime>0?.7f:1);
            if(Game.SafeTime>0)TextAt(g,"입장 보호",player.X-24,player.Y-47,11,Ink,90,22);
        }
        void DrawWorld(Graphics g)
        {
            Panel(g,530,171,597,402,247);
            TextAt(g,"서로 이어진 꿈",558,190,23,Ink,450,44,true);
            TextAt(g,"숫자는 미로 위의 문 번호입니다 · Tab 닫기",558,235,13,Muted,510,30);
            PointF[] p={new PointF(650,315),new PointF(1000,315),new PointF(1000,488),new PointF(650,488),new PointF(825,403)};
            for(int i=0;i<4;i++)
            {PointF a=p[i],b=p[(i+1)%4];Line(g,Muted,a.X,a.Y,b.X,b.Y,2);if(Game.Maps[i].GateOpen||i==3)Line(g,Gold,a.X,a.Y,p[4].X,p[4].Y,1);}
            for(int i=0;i<5;i++)
            {
                PointF a=p[i];Glow(g,a.X,a.Y,29,Color.FromArgb(100,i==Game.Area?Rose:Muted));
                Fill(g,Color.FromArgb(30,25,44),a.X-62,a.Y-22,124,51);
                TextAt(g,(i+1)+"  "+World.Names[i],a.X-52,a.Y-14,14,i==Game.Area?Rose:Ink,119,40);
            }
            TextAt(g,"금빛 선: 열린 중앙길  /  바깥 고리: 언제든 왕복 가능",554,537,12,Gold,560,26);
        }
        void Shade(Graphics g) { Fill(g,Color.FromArgb(175,4,5,13),0,0,1280,800);buttons.Clear(); }
        void DrawDialogue(Graphics g)
        {
            Shade(g);Panel(g,205,188,870,433,249);
            TextAt(g,Game.DialogueTitle,249,219,23,Rose,785,49,true);
            Line(g,Color.FromArgb(80,125,96,127),250,275,1028,275);
            TextAt(g,Game.DialoguePages[Math.Min(Game.DialoguePage,Game.DialoguePages.Length-1)],250,301,20,Ink,778,236);
            TextAt(g,(Game.DialoguePage+1)+" / "+Game.DialoguePages.Length+"     이 순간, 미로는 멈춰 있습니다.",250,571,13,Muted,520,28);
            Button(g,"E  계속",835,553,193,()=>Game.Advance());
        }
        void DrawPause(Graphics g)
        {
            Shade(g);Panel(g,417,201,446,375,249);
            TextAt(g,"잠시, 숨을 고르다",454,231,28,Ink,380,52,true);
            TextAt(g,"미로와 후회괴물이 멈췄습니다.",457,287,15,Muted,370,32);
            Button(g,"계속하기",456,335,368,()=>Change(Screen.Playing));
            Button(g,"설정 · 조작 방법",456,394,368,Settings);
            Button(g,"시작 화면으로 · 진행 초기화",456,453,368,()=>Change(Screen.Title));
            TextAt(g,"Esc  계속하기",457,528,13,Muted,300,26);
        }
        void DrawSettings(Graphics g)
        {
            Shade(g);Panel(g,335,145,610,523,251);
            TextAt(g,"설정과 조작",375,173,29,Ink,530,50,true);
            TextAt(g,"WASD  이동     E  조사·문·대화\nJ  기억 다시 읽기     Tab  연결 지도\nEsc  일시정지     M  음소거\n\n메뉴: ↑↓ 선택 / Enter 확인 또는 마우스 클릭\n발견 신호 → 모퉁이로 피하기 → 수색이 끝나길 기다리기",375,236,17,Muted,530,180);
            TextAt(g,"전체 음량  "+Audio.Volume+"%",375,423,19,Ink,450,30);
            Button(g,"− 줄이기",375,470,162,()=>Audio.Volume=Math.Max(0,Audio.Volume-10));
            Button(g,"+ 키우기",552,470,162,()=>Audio.Volume=Math.Min(100,Audio.Volume+10));
            Button(g,Audio.Muted?"소리 켜기":"음소거",729,470,175,()=>Audio.Muted=!Audio.Muted);
            Button(g,"돌아가기",375,541,529,()=>Change(Game.ReturnScreen));
            TextAt(g,"음악: TAD / Wolfgang_ · 효과음: Kenney · CC0",375,612,12,Muted,540,30);
        }
        void DrawJournal(Graphics g)
        {
            Shade(g);Panel(g,186,142,908,538,249);
            TextAt(g,"기억의 기록",225,170,27,Ink,800,47,true);
            for(int i=0;i<4;i++){int n=i;Button(g,(i+1)+"  "+(Game.Memories[i]?Story.Titles[i]:"미발견"),224+i*213,229,202,()=>journalPage=n);}
            TextAt(g,Story.Titles[journalPage],228,301,22,Rose,830,41,true);
            string text=Game.Memories[journalPage]?String.Join("\n\n",Story.Memories[journalPage]):"아직 이 기억에 닿지 못했습니다.\n\n"+World.Names[journalPage]+"에서 붉은 기억 조각을 찾아보세요.";
            TextAt(g,text,229,354,16,Ink,819,240);
            Button(g,"미로로 돌아가기",824,612,231,()=>Change(Screen.Playing));
            TextAt(g,"← → 기억 선택  /  Esc 닫기",230,626,13,Muted,500,26);
        }
        void DrawConfirm(Graphics g)
        {
            Shade(g);Panel(g,331,229,618,331,250);
            TextAt(g,"아직, 듣지 못한 말",370,260,28,Ink,550,49,true);
            TextAt(g,"모은 기억은 "+Game.Count+" / 4개입니다.\n이대로 그녀를 만나면 꿈은 악몽으로 끝납니다.\n돌아가서 빠진 기억을 찾을 수 있습니다.",371,327,19,Muted,537,106);
            Button(g,"돌아가서 기억 찾기",370,461,267,()=>Change(Screen.Playing));
            Button(g,"그래도 지금 만나기",653,461,257,()=>Game.AcceptIncomplete());
        }
        void DrawFailure(Graphics g)
        {
            Shade(g);Glow(g,640,302,270,Color.FromArgb(95,164,30,79));
            CenterText(g,Game.Mode==Screen.Caught?"후회에 붙잡혔다":"닿지 못한 인사",238,40,Rose,true);
            CenterText(g,Game.Mode==Screen.Caught?"모퉁이를 돌아 시야를 끊고, 수색이 끝나기를 기다려 보자.":"그녀가 아니라, 끝내 듣지 못한 마음이 악몽이 되었다.",312,18,Ink);
            CenterText(g,"구역 진입 시점으로 돌아갑니다. 이전 구역의 진행은 유지됩니다.",361,14,Muted);
            Button(g,"구역 입구에서 다시 시도",463,441,354,()=>Game.Retry());
            Button(g,"시작 화면으로",463,501,354,()=>Change(Screen.Title));
        }
        void Petals(Graphics g,int count,float opacity)
        {
            for(int i=0;i<count;i++)
            {
                float x=(float)((i*137.2+ambient*(12+i%11))%1360)-40;
                float y=(float)((i*91.7-ambient*(9+i%13)+16000)%900)-50;
                float size=3+i%5;
                using(SolidBrush b=new SolidBrush(Color.FromArgb((int)(opacity*(50+i%120)),243,155,199)))
                    g.FillEllipse(b,x+(float)Math.Sin(ambient+i)*14,y,size*1.7f,size);
            }
        }
        void DrawEnding(Graphics g)
        {
            if(Game.EndingPage<3)
            {
                float zoom=(float)Math.Min(Game.ModeTime*.002,.025);
                Cover(g,art["ending"],new RectangleF(-1280*zoom/2,-800*zoom/2,1280*(1+zoom),800*(1+zoom)));
                if(Game.EndingPage==0)
                {
                    Fill(g,Color.FromArgb(207,13,12,25),0,0,1280,800);
                    Sprite(g,"player",553,503,225,270);Sprite(g,"her",716,506,204,285);
                    Glow(g,647,408,180,Color.FromArgb(70,217,156,219));
                }
                if(Game.EndingPage==2)
                {float fade=(float)Math.Min(.6,Game.ModeTime/18);Fill(g,Color.FromArgb((int)(fade*255),245,217,227),0,0,1280,800);}
                Petals(g,100,1);
                using(LinearGradientBrush b=new LinearGradientBrush(new Rectangle(0,453,1280,347),Color.Transparent,Color.FromArgb(250,12,11,23),90))g.FillRectangle(b,0,453,1280,347);
                TextAt(g,Story.Ending[Game.EndingPage],111,560,23,Ink,1055,163,true);
                TextAt(g,"마지막 인사  /  "+(Game.EndingPage+1)+"",43,31,13,Ink,800,25);
                if(Game.ModeTime>=2)Button(g,"E  계속",1008,730,198,()=>Game.Advance());
            }
            else
            {
                using(LinearGradientBrush b=new LinearGradientBrush(new Rectangle(0,0,1280,800),Color.FromArgb(236,210,193),Color.FromArgb(108,107,146),90))g.FillRectangle(b,0,0,1280,800);
                Glow(g,960,213,270,Color.FromArgb(225,255,238,202));
                Fill(g,Color.FromArgb(50,65,57,74),830,80,12,560);Fill(g,Color.FromArgb(50,65,57,74),1070,80,12,560);Fill(g,Color.FromArgb(50,65,57,74),830,323,253,10);
                TextAt(g,"이제, 나의 아침",98,158,46,Color.FromArgb(48,40,61),780,80,true);
                TextAt(g,Story.Ending[3],104,279,24,Color.FromArgb(57,47,66),690,240,true);
                TextAt(g,"스크러플  ·  TRUE END\n당신의 내일에도, 작은 빛이 들기를.",108,559,16,Color.FromArgb(62,49,72),770,78);
                Button(g,"시작 화면으로",106,694,294,()=>Change(Screen.Title));Button(g,"꿈을 마치기",421,694,247,Close);
            }
        }
        // QA 전용: 게임과 동일한 OnPaint 경로를 파일로 저장한다. 사용자 세이브를 변경하지 않는다.
        public void SaveFrame(string path)
        {using(Bitmap bitmap=new Bitmap(ClientSize.Width,ClientSize.Height)){using(Graphics g=Graphics.FromImage(bitmap))OnPaint(new PaintEventArgs(g,new Rectangle(Point.Empty,ClientSize)));bitmap.Save(path,ImageFormat.Png);}}
        protected override void Dispose(bool disposing)
        {
            if(disposing){timer.Stop();timer.Dispose();Audio.Dispose();foreach(Image im in art.Values)im.Dispose();foreach(Font f in fonts.Values)f.Dispose();}
            base.Dispose(disposing);
        }
    }
}
