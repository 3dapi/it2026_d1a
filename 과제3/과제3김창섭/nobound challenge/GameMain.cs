// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;
    private G2Texture? background;
    private G2Texture? title;
    private G2Texture? gamestart;



    protected override void Initialize()
	{

        
    //---------------------------------------
    // 게임 관련 객체를 생성합니다.
    //---------------------------------------
    var texuidir = "resource/tex_ui/";
        background = new G2Texture("resource/배경.png");
       gamestart = new G2Texture("resource/게임 시작.png");
       title = new G2Texture("resource/타이틀화면.png");
    }

	protected override void Update()
	{
		
		//---------------------------------------
		// 게임 관련 객체를 갱신합니다.
		//---------------------------------------
	}

	protected override void Render()
	{
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------
        background?.Draw();
        title?.Draw();
        gamestart?.Draw(0, 100);


    }

    public override void Dispose()
	{
		base.Dispose();
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------
        gamestart?.Draw(0,100);
        title?.Dispose();
        background?.Dispose();



    }
}
