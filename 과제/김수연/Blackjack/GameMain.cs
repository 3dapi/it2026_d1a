// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

    private G2Texture? _bgTable = null;
    private G2Texture? _bgStart = null;
    private G2Texture? _bgStart_Clicked = null;
    private G2Texture? _bgQuit = null;
    private G2Texture? _bgQuit_Clicked = null;
    private G2Texture? _bgStand = null;
    private G2Texture? _bgStand_clicked = null;
    private G2Texture? _bgReStart = null;
    private G2Texture? _bgReStart_Clicked = null;
    private G2Texture? _bgHit = null;
    private G2Texture? _bgHit_Clicked = null;
    private G2Texture? _bgScore = null;
    private G2Texture? _bgWin = null;
    private G2Texture? _bgLose = null;

    //번호

    private G2Texture? _bg_1 = null;
    private G2Texture? _bg_2 = null;
    private G2Texture? _bg_3 = null;
    private G2Texture? _bg_4 = null;
    private G2Texture? _bg_5 = null;
    private G2Texture? _bg_6 = null;
    private G2Texture? _bg_7 = null;
    private G2Texture? _bg_8 = null;
    private G2Texture? _bg_9 = null;
    private G2Texture? _bg_0 = null;


    //카드 정렬 Spade,Heart,Clover,Diamond 순

    private G2Texture? _bgCardBack = null;

    //Spade
    private G2Texture? _bgSpade_2 =null;
    private G2Texture? _bgSpade_3 = null;
    private G2Texture? _bgSpade_4 = null;
    private G2Texture? _bgSpade_5 = null;
    private G2Texture? _bgSpade_6 = null;
    private G2Texture? _bgSpade_7 = null;
    private G2Texture? _bgSpade_8 = null;
    private G2Texture? _bgSpade_9 = null;
    private G2Texture? _bgSpade_10 = null;

    //Heart
    private G2Texture? _bgHeart_2 = null;
    private G2Texture? _bgHeart_3 = null;
    private G2Texture? _bgHeart_4 = null;
    private G2Texture? _bgHeart_5 = null;
    private G2Texture? _bgHeart_6 = null;
    private G2Texture? _bgHeart_7 = null;
    private G2Texture? _bgHeart_8 = null;
    private G2Texture? _bgHeart_9 = null;
    private G2Texture? _bgHeart_10 = null;


    //Clover
    private G2Texture? _bgClover_2 = null;
    private G2Texture? _bgClover_3 = null;
    private G2Texture? _bgClover_4 = null;
    private G2Texture? _bgClover_5 = null;
    private G2Texture? _bgClover_6 = null;
    private G2Texture? _bgClover_7 = null;
    private G2Texture? _bgClover_8 = null;
    private G2Texture? _bgClover_9 = null;
    private G2Texture? _bgClover_10 = null;

    //Diamond
    private G2Texture? _bgDiamond_2 = null;
    private G2Texture? _bgDiamond_3 = null;
    private G2Texture? _bgDiamond_4 = null;
    private G2Texture? _bgDiamond_5 = null;
    private G2Texture? _bgDiamond_6 = null;
    private G2Texture? _bgDiamond_7 = null;
    private G2Texture? _bgDiamond_8 = null;
    private G2Texture? _bgDiamond_9 = null;
    private G2Texture? _bgDiamond_10 = null;



    protected override void Initialize()
	{
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------

		//카드 외 Table,UI 순
        _bgTable = new("resource/Table_UI.png");
        _bgStart = new("resource/GameStart_UI.png");
        _bgStart_Clicked = new("resource/GameStart_Clicked_UI.png");
        _bgQuit =  new("resource/GameQuit_UI.png");
        _bgQuit_Clicked = new("resource/GameQuit_Clicked_UI.png");
        _bgStand = new("resource/Stand_UI.png");
        _bgStand_clicked = new("resource/Stand_Clicked_UI.png");
		_bgReStart = new("resource/ReStart_UI.png");
        _bgReStart_Clicked = new("resource/ReStart_Clicked_UI.png");
        _bgHit = new("resource/Hit_UI.png");
        _bgHit_Clicked = new("resource/Hit_Clicked_UI.png");
        _bgScore = new("resource/Score_Text_White.png");
        _bgWin = new("resource/Win_UI.png");
        _bgLose = new("resource/Lose_UI.png");

        //숫자
        _bg_1 = new("resource/1_Text.png");
        _bg_2 = new("resource/2_Text.png");
        _bg_3 = new("resource/3_Text.png");
        _bg_4 = new("resource/4_Text.png");
        _bg_5 = new("resource/5_Text.png");
        _bg_6 = new("resource/6_Text.png");
        _bg_7 = new("resource/7_Text.png");
        _bg_8 = new("resource/8_Text.png");
        _bg_9 = new("resource/9_Text.png");
        _bg_0 = new("resource/0_Text.png");



        //카드 정렬 Spade,Heart,Clover,Diamond 순
        _bgCardBack = new("resource/Card_Back_UI.png");
        //Spade
        _bgSpade_2 =  new("resource/Spade_2.png");
		_bgSpade_3 =  new("resource/Spade_3.png");
        _bgSpade_4 =  new("resource/Spade_4.png");
        _bgSpade_5 =  new("resource/Spade_5.png");
        _bgSpade_6 =  new("resource/Spade_6.png");
        _bgSpade_7 =  new("resource/Spade_7.png");
        _bgSpade_8 =  new("resource/Spade_8.png");
        _bgSpade_9 =  new("resource/Spade_9.png");
        _bgSpade_10 = new("resource/Spade_10.png");

        //Heart
        _bgHeart_2 =  new("resource/Heart_2.png");
        _bgHeart_3 =  new("resource/Heart_3.png");
        _bgHeart_4 =  new("resource/Heart_4.png");
        _bgHeart_5 =  new("resource/Heart_5.png");
        _bgHeart_6 =  new("resource/Heart_6.png");
        _bgHeart_7 =  new("resource/Heart_7.png");
        _bgHeart_8 =  new("resource/Heart_8.png");
        _bgHeart_9 =  new("resource/Heart_9.png");
        _bgHeart_10 = new("resource/Heart_10.png");

        //Clover
        _bgClover_2 =  new("resource/Clover_2.png");
        _bgClover_3 =  new("resource/Clover_3.png");
        _bgClover_4 =  new("resource/Clover_4.png");
        _bgClover_5 =  new("resource/Clover_5.png");
        _bgClover_6 =  new("resource/Clover_6.png");
        _bgClover_7 =  new("resource/Clover_7.png");
        _bgClover_8 =  new("resource/Clover_8.png");
        _bgClover_9 =  new("resource/Clover_9.png");
        _bgClover_10 = new("resource/Clover_10.png");


        //Diamond
        _bgDiamond_2 =  new("resource/Diamond_2.png");
        _bgDiamond_3 =  new("resource/Diamond_3.png");
        _bgDiamond_4 =  new("resource/Diamond_4.png");
        _bgDiamond_5 =  new("resource/Diamond_5.png");
        _bgDiamond_6 =  new("resource/Diamond_6.png");
        _bgDiamond_7 =  new("resource/Diamond_7.png");
        _bgDiamond_8 =  new("resource/Diamond_8.png");
        _bgDiamond_9 =  new("resource/Diamond_9.png");
        _bgDiamond_10 = new("resource/Diamond_10.png");

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

        //Table, UI
        _bgTable?.Draw(1, 1);
        _bgHit?.Draw(1, 1);
        _bgStart?.Draw(1, 1);
        _bgStart_Clicked?.Draw(1, 1);
        _bgQuit?.Draw(1, 1);
        _bgQuit_Clicked?.Draw(1, 1);
        _bgStand?.Draw(1, 1);
        _bgStand_clicked?.Draw(1, 1);
        _bgReStart?.Draw(1, 1);
        _bgReStart_Clicked?.Draw(1,1);
        _bgHit?.Draw(1, 1);
        _bgHit_Clicked ?.Draw(1, 1);
        _bgScore?.Draw(1, 1); 
        _bgWin ?.Draw(1, 1);
        _bgLose?.Draw(1, 1);

        //숫자

        _bg_1?.Draw(1, 1);
        _bg_2?.Draw(1, 1);
        _bg_3?.Draw(1, 1);
        _bg_4?.Draw(1, 1);
        _bg_5?.Draw(1, 1);
        _bg_6?.Draw(1, 1);
        _bg_7?.Draw(1, 1);
        _bg_8?.Draw(1, 1);
        _bg_9?.Draw(1, 1);
        _bg_0?.Draw(1, 1);

        //카드

        _bgCardBack ?.Draw(1, 1);
        //Spade
        _bgSpade_2?.Draw(1, 1);
        _bgSpade_3?.Draw(1, 1);
        _bgSpade_4?.Draw(1, 1);
        _bgSpade_5?.Draw(1, 1);
        _bgSpade_6?.Draw(1, 1);
        _bgSpade_7?.Draw(1, 1);
        _bgSpade_8?.Draw(1, 1);
        _bgSpade_9?.Draw(1, 1);
        _bgSpade_10?.Draw(1, 1);

        //Heart
        _bgHeart_2?.Draw(1, 1);
        _bgHeart_3?.Draw(1, 1);
        _bgHeart_4?.Draw(1, 1);
        _bgHeart_5?.Draw(1, 1);
        _bgHeart_6?.Draw(1, 1);
        _bgHeart_7?.Draw(1, 1);
        _bgHeart_8?.Draw(1, 1);
        _bgHeart_9?.Draw(1, 1);
        _bgHeart_10?.Draw(1, 1);

        //Clover
        _bgClover_2?.Draw(1, 1);
        _bgClover_3?.Draw(1, 1);
        _bgClover_4?.Draw(1, 1);
        _bgClover_5?.Draw(1, 1);
        _bgClover_6?.Draw(1, 1);
        _bgClover_7 ?.Draw(1, 1);
        _bgClover_8?.Draw(1, 1);
        _bgClover_9?.Draw(1, 1);
        _bgClover_10?.Draw(1, 1);


        //Diamond
        _bgDiamond_2?.Draw(1, 1);
        _bgDiamond_3?.Draw(1, 1);
        _bgDiamond_4?.Draw(1, 1);
        _bgDiamond_5?.Draw(1, 1);
        _bgDiamond_6?.Draw(1, 1);
        _bgDiamond_7?.Draw(1, 1);
        _bgDiamond_8?.Draw(1, 1);
        _bgDiamond_9?.Draw(1, 1);
        _bgDiamond_10?.Draw(1, 1);


    }

    public override void Dispose()
	{
		base.Dispose();
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------

        //Table, UI
        _bgTable?.Dispose();
    }
}
