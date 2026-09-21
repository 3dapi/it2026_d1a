using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Scruple
{
    // Windows의 MCI를 작은 클래스로 감싼다. 별도 NuGet·사운드 엔진은 필요 없다.
    public sealed class Audio : IDisposable
    {
        [DllImport("winmm.dll",CharSet=CharSet.Unicode)]
        static extern int mciSendString(string command,StringBuilder result,int length,IntPtr handle);
        public readonly List<string> Errors=new List<string>();
        readonly Dictionary<string,string> effects=new Dictionary<string,string>();
        string current="",pending="";
        double gain,heartbeat;
        public bool Muted;
        public int Volume=65;
        bool fading;
        readonly string root;
        public Audio(string directory)
        {
            root=directory;
            foreach(string n in new[]{"click","memory","door","alert","fail","pulse"})
            {
                string alias="sfx_"+n;
                if(Open(Path.Combine(root,n+".wav"),alias))effects[n]=alias;
            }
        }
        bool Open(string path,string alias)
        { return Send("open \""+path+"\" type mpegvideo alias "+alias); }
        bool Send(string command)
        {
            int e=mciSendString(command,null,0,IntPtr.Zero);
            if(e!=0 && Errors.Count<30)Errors.Add(e+": "+command);
            return e==0;
        }
        public void Effect(string n)
        {
            string a;if(Muted || !effects.TryGetValue(n,out a))return;
            Send("setaudio "+a+" volume to "+(Volume*6));
            Send("seek "+a+" to start");Send("play "+a);
        }
        public void Update(double dt,Screen mode,bool threat)
        {
            string desired=(mode==Screen.Ending)?"farewell":(mode==Screen.Caught || mode==Screen.Nightmare)?"":"dream";
            if(desired!=pending){pending=desired;fading=true;}
            double target=Muted?0:Volume/100.0*(threat?.20:.65);
            if(mode==Screen.Paused || mode==Screen.Journal || mode==Screen.Settings)target*=.4;
            if(fading)
            {
                gain=Math.Max(0,gain-dt*.7);
                if(gain<=0)
                {
                    if(current!="")Send("close music");
                    current=pending;fading=false;
                    if(current!="" && Open(Path.Combine(root,current+".mp3"),"music"))
                    { Send("setaudio music volume to 0");Send("play music repeat"); }
                }
            }
            else gain+=Math.Sign(target-gain)*Math.Min(Math.Abs(target-gain),dt*.45);
            if(current!="")Send("setaudio music volume to "+(Muted?0:(int)(gain*1000)));
            heartbeat-=dt;
            if(mode==Screen.Playing && threat && heartbeat<=0){Effect("pulse");heartbeat=.85;}
            if(Muted)foreach(string a in effects.Values)Send("stop "+a);
        }
        public string Status(string alias)
        { StringBuilder b=new StringBuilder(128);int e=mciSendString("status "+alias+" mode",b,b.Capacity,IntPtr.Zero);return e==0?b.ToString():"error "+e; }
        public void Dispose()
        { if(current!="")Send("close music");foreach(string a in effects.Values)Send("close "+a); }
    }
}
