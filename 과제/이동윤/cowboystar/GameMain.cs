using System.Windows.Forms;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private G2Texture? background;
    private G2Texture? title;
    private G2Texture? start;




    // 사운드
    private G2AudioMp3? bgm;
    private G2AudioSound? gunSound;


    protected override void Initialize()
    {
        // 이미지 
        background = new G2Texture("resource/background/bg.png");
        title = new G2Texture("resource/ui/title.png");
        start = new G2Texture("resource/ui/start.png");

        // 사운드 
        bgm = new G2AudioMp3("resource/sound/bgm.mp3");
        gunSound = new G2AudioSound("resource/sound/gun.wav");

        // 배경음악 반복재생
        bgm.Play(true);
    }


    protected override void Update()
    {
        // 마우스 왼쪽 버튼 재생
        if (Input.IsButtonDown(MouseButtons.Left))
        {
            gunSound?.Play();
        }
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
        // 사운드 제거
        bgm?.Dispose();
        gunSound?.Dispose();

        // 이미지 제거
        background?.Dispose();
        title?.Dispose();
        start?.Dispose();

        base.Dispose();
    }
}