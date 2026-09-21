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
