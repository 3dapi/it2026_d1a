using System;
using System.IO;
using System.Windows.Forms;

namespace Scruple
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if(args.Length>0 && args[0]=="--self-test") { Environment.ExitCode=SelfTests.Run();return; }
                using(GameForm game=new GameForm())Application.Run(game);
            }
            catch(Exception ex)
            {MessageBox.Show("게임을 시작하지 못했습니다.\nAssets 폴더를 실행 파일과 같은 폴더에 두세요.\n\n"+ex.Message,"스크러플",MessageBoxButtons.OK,MessageBoxIcon.Error);}
        }
    }
}
