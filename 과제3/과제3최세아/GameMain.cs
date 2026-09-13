// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;
using System.Drawing;
using System.Windows.Forms;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	private G2Texture _Wallpaper = null;
	private G2Texture _hg = null;
    private G2Texture _Start= null;
    private G2Texture _Snakemein = null;
    private G2Texture _Gameend = null;
    private G2Texture _Pool = null;

    private enum GameState
    {
        Start,
        Playing,
        Clear,
        GameOver
    }

    private GameState _state = GameState.Start;

    protected override void Initialize()
	{
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------

        _Wallpaper = new G2Texture("resource/tex_play/ui_wallpaper.png");
        _hg = new G2Texture("resource/tex_play/ui_bg.png");
        _Start = new G2Texture("resource/tex_play/ui_Start.png");
        _Snakemein = new G2Texture("resource/tex_play/ui_Snake.png");
        _Gameend = new G2Texture("resource/tex_play/ui_gameend.png");
        _Pool = new G2Texture("resource/tex_play/ui_pool.png");
    }
    

	

	protected override void Update()
	{
		double elapsed = TotalTime;

		this.ClearColor = new Color4(
			red: (float)(Math.Sin(elapsed) * 0.5 + 0.5),
			green: (float)(Math.Sin(elapsed + Math.PI / 2.0) * 0.5 + 0.5),
			blue: (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5),
		alpha: 1.0f);

        //---------------------------------------
        // 게임 관련 객체를 갱신합니다.
        //---------------------------------------

        if (_state == GameState.Start)
        {
            // 마우스 왼쪽 버튼을 누른 순간
            if (Input.IsButtonDown(MouseButtons.Left))
            {
                var mouse = Input.MousePosition;
                var startButton = new RectangleF(324, 340, 312, 130);
                var exitButton = new RectangleF(324, 489, 312, 130);

                if (startButton.Contains(mouse))
                {
                    _state = GameState.Playing;
                }
                else if (exitButton.Contains(mouse))
                {
                    Close();
                    return;
                }
            }
        }
        else if (_state == GameState.Playing)
        {
            // 나중에 여기에 뱀 이동과 충돌 처리 추가
        }
    }

	protected override void Render()
	{
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------

        // 모든 화면에 공통으로 보일 배경
        var bgDest = new Vortice.RawRectF(0, 0, 960, 640);
        var bgSrc = new Vortice.RawRectF(0, 0, 1024, 559);
        _Wallpaper.Draw(bgDest, bgSrc);

        switch (_state)
        {
            case GameState.Start: // 게임 시작 화면

                _Snakemein.Draw(
                    new Vortice.RawRectF(142, -30, 818, 339),
                    new Vortice.RawRectF(0, 0, 676, 369));

                _Start.Draw(
                    new Vortice.RawRectF(142, 220, 818, 589),
                    new Vortice.RawRectF(0, 0, 676, 369));

                _Gameend.Draw(
                    new Vortice.RawRectF(142, 370, 818, 739),
                    new Vortice.RawRectF(0, 0, 1696, 928));
                break;


            case GameState.Playing: // 게임 플레이 화면
                _hg.Draw(

                    new Vortice.RawRectF(200, 40, 760, 600),
                    new Vortice.RawRectF(0, 0, 1024, 1024));

                // 왼쪽 바깥 풀
                _Pool.Draw(
                    new Vortice.RawRectF(-10, 495, 250, 625),
                    new Vortice.RawRectF(0, 0, 1774, 887));

                // 오른쪽 바깥 풀
                _Pool.Draw(
                    new Vortice.RawRectF(710, 495, 970, 625),
                    new Vortice.RawRectF(0, 0, 1774, 887));

                // 왼쪽 안쪽 작은 풀
                _Pool.Draw(
                    new Vortice.RawRectF(170, 525, 370, 625),
                    new Vortice.RawRectF(0, 0, 1774, 887));

                // 오른쪽 안쪽 작은 풀
                _Pool.Draw(
                    new Vortice.RawRectF(590, 525, 790, 625),
                    new Vortice.RawRectF(0, 0, 1774, 887));

                // 왼쪽 중심 큰 풀
                _Pool.Draw(
                    new Vortice.RawRectF(40, 475, 360, 635),
                    new Vortice.RawRectF(0, 0, 1774, 887));

                // 오른쪽 중심 큰 풀
                _Pool.Draw(
                    new Vortice.RawRectF(600, 475, 920, 635),
                    new Vortice.RawRectF(0, 0, 1774, 887));
                break;



            case GameState.Clear: // 게임 클리어 화면

                // 나중에 클리어 화면 넣기

                break;

            case GameState.GameOver: // 게임 오버 화면

                // 나중에 게임 오버 화면 넣기

                break;
        }
    }

	public override void Dispose()
	{

        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------

        _Wallpaper.Dispose();
        _hg.Dispose();
        _Start.Dispose();
        _Snakemein.Dispose();
        _Gameend.Dispose();
        _Pool.Dispose();
    }
}  
