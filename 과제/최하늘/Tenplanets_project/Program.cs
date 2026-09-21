using System;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        // Windows 화면 배율 때문에
        // glc2d 화면이 이중 확대되는 현상 방지
        Application.SetHighDpiMode(
            HighDpiMode.DpiUnaware
        );

        Application.EnableVisualStyles();

        Application.SetCompatibleTextRenderingDefault(
            false
        );


        using GameMain game = new GameMain();

        game.Run();
    }
}