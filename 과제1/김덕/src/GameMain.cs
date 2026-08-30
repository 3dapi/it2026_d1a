using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private readonly SceneMain _sceneMain = new();

    protected override void Initialize()
    {
        _sceneMain.Initialize();
    }

    protected override void Update()
    {
        _sceneMain.Update();
    }

    protected override void Render()
    {
        _sceneMain.Render();
    }

    public override void Dispose()
    {
        _sceneMain.Dispose();
        base.Dispose();
    }
}
