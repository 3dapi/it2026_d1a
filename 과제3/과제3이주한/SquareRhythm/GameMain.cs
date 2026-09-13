// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

    private G2Font? _fntMessage1 = null;
    private G2Font? _fntMessage2 = null;
    private G2Texture? _bgTexture = null;
    private G2Texture? _uiTitleTexture = null;
    private G2Texture? _uiButtonTexture = null;
    private G2Texture? _uiSelectedTexture = null;
    private G2Texture? _grayNoteTexture = null;
    private G2Texture? _redNoteTexture = null;
    private G2Texture? _blueNoteTexture = null;

    protected override void Initialize()
	{
		//---------------------------------------
		// 게임 관련 객체를 생성합니다.
		//---------------------------------------
		var texUiDir = "resource/image/";

        _fntMessage1 = new("Bahnschrift", 32);
        _fntMessage2 = new("Bahnschrift", 32);
        _bgTexture = new(texUiDir + "ui/background.png");
        _uiTitleTexture = new(texUiDir + "ui/menu/title.png");
        _uiButtonTexture = new(texUiDir + "ui/menu/button.png");
        _uiSelectedTexture = new(texUiDir + "ui/menu/selected.png");
        _grayNoteTexture = new(texUiDir + "note/gray/right.png");
        _redNoteTexture = new(texUiDir + "note/red/right.png");
        _blueNoteTexture = new(texUiDir + "note/blue/right.png");
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
	}

	protected override void Render()
	{
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------
        
        _bgTexture.Draw(0f, 0f);
        _uiTitleTexture.Draw(240, 70);
        _grayNoteTexture.Draw(390, 250);
        _redNoteTexture.Draw(544, 250);
        _blueNoteTexture.Draw(697, 250);
        _uiButtonTexture.Draw(430, 400);
        _uiButtonTexture.Draw(430, 500);
        _uiSelectedTexture.Draw(350, 400);

        _fntMessage1.DrawText("START GAME", new(547, 435, 500, 100), new(1, 1, 1, 1));
        _fntMessage2.DrawText("EXIT", new(609, 535, 500, 100), new(0.7f, 0.7f, 0.7f, 1));
    }

	public override void Dispose()
	{
		base.Dispose();
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------
       
        _bgTexture.Dispose();
        _uiTitleTexture.Dispose();
        _uiButtonTexture.Dispose();
        _uiSelectedTexture.Dispose();
        _grayNoteTexture.Dispose();
        _redNoteTexture.Dispose();
        _blueNoteTexture.Dispose();

        _fntMessage1.Dispose();
        _fntMessage2.Dispose();
    }
}
