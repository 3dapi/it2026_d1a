using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;
    private G2Texture? background;
    private G2Texture? title;
    private G2Texture? start;

    protected override void Initialize()
    {
        background = new G2Texture("resource/background/bg.png");
        title = new G2Texture("resource/ui/title.png");
        start = new G2Texture("resource/ui/start.png");
    }

    protected override void Update()
    {
        // 내용
    }

    protected override void Render()
    {
        // 배경
        background?.Draw();

        // 제목
        title?.Draw(400, 50);

        // 시작하기
        start?.Draw(800, 700);
    }

    public override void Dispose()
    {
        background?.Dispose();
        title?.Dispose();
        start?.Dispose();

        base.Dispose();
    }
}