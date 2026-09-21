
// 1번은 시작 2번은 나가기 //


internal static class AppMain
{
    [STAThread]
    static void Main()
    {
        Console.WriteLine("장애물 달리기 시작 화면");
        ApplicationConfiguration.Initialize();
        using (GameMain game = new GameMain())
        {
            game.Run();
        }
    }
}