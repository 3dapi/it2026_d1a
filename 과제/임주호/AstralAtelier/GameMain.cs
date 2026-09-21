using Vortice.Mathematics;

internal sealed class GameMain : G2AppBase
{
    private enum GameScene
    {
        Title,
        Battle
    }

    private readonly SceneTitle _titleScene = new();
    private readonly SceneBattle _battleScene = new();

    private GameScene _currentScene = GameScene.Title;

    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    protected override void Initialize()
    {
        ClearColor = new Color4(
            red: 0.05f,
            green: 0.05f,
            blue: 0.1f,
            alpha: 1.0f);

        _titleScene.Initialize();
        _battleScene.Initialize();
    }

    protected override void Update()
    {
        if (_currentScene == GameScene.Title)
        {
            TitleAction action = _titleScene.Update();

            if (action == TitleAction.StartGame)
            {
                _currentScene = GameScene.Battle;
            }
            else if (action == TitleAction.ExitGame)
            {
                Close();
            }
        }
        else if (_currentScene == GameScene.Battle)
        {
            bool returnToTitle = _battleScene.Update();

            if (returnToTitle)
            {
                _currentScene = GameScene.Title;
            }
        }
    }

    protected override void Render()
    {
        if (_currentScene == GameScene.Title)
        {
            _titleScene.Render();
        }
        else if (_currentScene == GameScene.Battle)
        {
            _battleScene.Render();
        }
    }

    public override void Dispose()
    {
        _battleScene.Dispose();
        _titleScene.Dispose();
        base.Dispose();
    }
}
