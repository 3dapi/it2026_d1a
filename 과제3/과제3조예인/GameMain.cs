using Vortice.Mathematics;

class GameMain : G2AppBase
{
    private G2Texture? _bgTexture = null;
    private G2Texture? _titleTexture = null;
    private G2Texture? _uiTexture = null;

    private GameState _state = GameState.Start;

    public override System.Drawing.Size ScreenSize => new(640, 360);

    public override string GameName => "왁뿌수기";

    protected override void Initialize()
    {
        _bgTexture = new G2Texture("resource/start_background.png");
        _titleTexture = new G2Texture("resource/title.png");
        _uiTexture = new G2Texture("resource/start_ui.png");
    }

    protected override void Update()
    {
        // 게임 업데이트
    }

    protected override void Render()
    {
        if (_state == GameState.Start)
        {
            RenderStart();
        }
        else if (_state == GameState.Playing)
        {
            RenderGame();
        }
        else if (_state == GameState.End)
        {
            RenderEnd();
        }
    }

    private void RenderStart()
    {
        // 1920 x 1080 이미지를 640 x 360 화면에 맞춰 출력
        _bgTexture?.Draw(
            new Rect(0, 0, 640, 360),
            new Rect(0, 0, 1920, 1080)
        );

        _titleTexture?.Draw(
            new Rect(0, 0, 640, 360),
            new Rect(0, 0, 1920, 1080)
        );

        _uiTexture?.Draw(
            new Rect(0, 0, 640, 360),
            new Rect(0, 0, 1920, 1080)
        );
    }

    private void RenderGame()
    {
        // 실제 게임 화면
    }

    private void RenderEnd()
    {
        // 게임 종료 화면
    }
}

enum GameState
{
    Start,
    Playing,
    End
}