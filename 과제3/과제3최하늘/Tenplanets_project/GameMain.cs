class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize
        => GameGlobal.ScreenSize;

    public override string GameName
        => GameGlobal.GameName;


    private readonly SceneMain sceneMain
        = new SceneMain();


    protected override void Initialize()
    {
        sceneMain.Initialize();
    }


    protected override void Update()
    {
        sceneMain.Update();
    }


    protected override void Render()
    {
        sceneMain.Render();
    }


    public override void Dispose()
    {
        sceneMain.Dispose();

        base.Dispose();
    }
}