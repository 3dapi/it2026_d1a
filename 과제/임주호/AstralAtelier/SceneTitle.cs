using Vortice.Mathematics;

internal enum TitleAction
{
    None,
    StartGame,
    ExitGame
}

internal sealed class SceneTitle : IDisposable
{
    private const float ButtonX = 640.0f;
    private const float ButtonWidth = 300.0f;
    private const float ButtonHeight = 100.0f;
    private const float NewGameButtonY = 20.0f;
    private const float ExitButtonY = 130.0f;

    private G2Texture? _background;
    private G2Texture? _newGameButton;
    private G2Texture? _exitButton;

    public void Initialize()
    {
        _background = new G2Texture(
            "resource/background/academy.png");

        _newGameButton = new G2Texture(
            "resource/ui/system/newgame.png");

        _exitButton = new G2Texture(
            "resource/ui/system/exit.png");
    }

    public TitleAction Update()
    {
        G2InputContext input = G2AppBase.Instance?.Input
            ?? throw new InvalidOperationException(
                "G2AppBase instance is not initialized.");

        if (!input.IsButtonDown(MouseButtons.Left))
        {
            return TitleAction.None;
        }

        float mouseX = input.MousePosition.X;
        float mouseY = input.MousePosition.Y;

        if (IsInsideButton(mouseX, mouseY, NewGameButtonY))
        {
            return TitleAction.StartGame;
        }

        if (IsInsideButton(mouseX, mouseY, ExitButtonY))
        {
            return TitleAction.ExitGame;
        }

        return TitleAction.None;
    }

    public void Render()
    {
        _background?.Draw(
            new Rect(0, 0, 960, 640),
            new Rect(0, 0, 1536, 1024));

        _newGameButton?.Draw(
            new Rect(ButtonX, NewGameButtonY, ButtonWidth, ButtonHeight),
            new Rect(0, 0, 2172, 724));

        _exitButton?.Draw(
            new Rect(ButtonX, ExitButtonY, ButtonWidth, ButtonHeight),
            new Rect(0, 0, 2172, 724));
    }

    private static bool IsInsideButton(
        float mouseX,
        float mouseY,
        float buttonY)
    {
        return mouseX >= ButtonX &&
               mouseX <= ButtonX + ButtonWidth &&
               mouseY >= buttonY &&
               mouseY <= buttonY + ButtonHeight;
    }

    public void Dispose()
    {
        _exitButton?.Dispose();
        _newGameButton?.Dispose();
        _background?.Dispose();
    }
}
