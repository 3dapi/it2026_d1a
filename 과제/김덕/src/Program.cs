namespace _26310022_kimduk_GameProject;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        using GameMain app = new();
        app.Run();
    }
}
