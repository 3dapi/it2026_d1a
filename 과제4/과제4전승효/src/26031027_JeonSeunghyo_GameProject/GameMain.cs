// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Numerics;
using Vortice.Mathematics;
using Vortice.DirectWrite;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	enum GameScene { Title, Play }

	G2Texture? _titleBg;
	G2Texture? _playBg;
	G2Texture? _btnNewGame;
	G2Texture? _btnContinue;
	G2Texture? _player;
	G2Texture? _monster;
	G2Texture? _hpBar;
	G2Texture? _btnSettings;
	G2Texture? _uiFrame;
	G2Texture? _btnMainMenu;
	G2Texture? _btnGameOptions;
	G2Texture? _btnQuitGame;
	G2Font?    _titleFont;
	G2AudioSound? _titleBgm;
	G2AudioSound? _battleBgm;
	G2AudioSound? _btnClick;
	bool       _prevClick;
	bool       _showSettings;
	GameScene  _scene = GameScene.Title;

	protected override void Initialize()
	{
		_titleBg        = new G2Texture("resource/image/background/title_bg.png");
		_playBg         = new G2Texture("resource/image/background/battle_forest.png");
		_btnNewGame     = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 7 - 1080p.png");
		_btnContinue    = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 12 - 1080p.png");
		_player         = new G2Texture("resource/image/player/colour1/nooutline/120x80_pngsheets/_Idle.png");
		_monster        = new G2Texture("resource/image/enemy/monsters_creatures_fantasy/goblin/Idle.png");
		_hpBar          = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 2 - 1080p.png");
		_btnSettings    = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 3 - 1080p.png");
		_uiFrame        = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 1 - 1080p.png");
		_btnMainMenu    = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 11 - 1080p.png");
		_btnGameOptions = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 13 - 1080p.png");
		_btnQuitGame    = new G2Texture("resource/image/ui_menu/game_menu/1x/Asset 5 - 1080p.png");
		_titleFont      = new G2Font("Arial", 72, FontWeight.Heavy, Vortice.DirectWrite.FontStyle.Normal,
		                             TextAlignment.Center, ParagraphAlignment.Center);
		_titleBgm  = new G2AudioSound("resource/audio/title_bgm.wav");
		_battleBgm = new G2AudioSound("resource/audio/battle_bgm.wav");
		_btnClick  = new G2AudioSound("resource/audio/btn_click.wav");

		this.ClearColor = new Color4(0.05f, 0.05f, 0.15f, 1.0f);

		_titleBgm.Play(true);
	}

	protected override void Update()
	{
		bool down  = Input.IsButtonDown(System.Windows.Forms.MouseButtons.Left);
		bool click = down && !_prevClick;
		_prevClick = down;

		if (_scene == GameScene.Title)
		{
			if (click)
			{
				_btnClick!.Play();
				_titleBgm!.Stop();
				_battleBgm!.Play(true);
				_scene = GameScene.Play;
			}
			return;
		}

		if (!click) return;

		float mx = Input.MousePosition.X;
		float my = Input.MousePosition.Y;

		// 세팅 버튼 클릭 (우상단 855,8 ~ 945,40)
		if (mx >= 855 && mx <= 945 && my >= 8 && my <= 40)
		{
			_btnClick!.Play();
			_showSettings = !_showSettings;
			return;
		}

		if (_showSettings)
		{
			// MAIN MENU 버튼 → 타이틀 화면으로
			if (mx >= 340 && mx <= 620 && my >= 230 && my <= 276)
			{
				_btnClick!.Play();
				_battleBgm!.Stop();
				_titleBgm!.Play(true);
				_scene = GameScene.Title;
				_showSettings = false;
				return;
			}
			// QUIT GAME 버튼 → 게임 종료
			if (mx >= 340 && mx <= 620 && my >= 390 && my <= 444)
			{
				_btnClick!.Play();
				Close();
			}
		}
	}

	protected override void Render()
	{
		if (_scene == GameScene.Title)
		{
			_titleBg!.Draw(new Rect(0, 0, 960, 640), new Rect(0, 0, 192, 108));
			_titleFont!.DrawText("IDLE QUEST", new Rect(0, 120, 960, 160), new Color4(1.0f, 0.88f, 0.2f, 1.0f));
			// NEW GAME 버튼 (원본 199×35 → 2배 확대, 화면 중앙)
			_btnNewGame!.Draw(new Rect(281, 400, 398, 70), new Rect(0, 0, 199, 35));
			// CONTINUE 버튼 (원본 178×36 → 2배 확대)
			_btnContinue!.Draw(new Rect(302, 490, 356, 72), new Rect(0, 0, 178, 36));
			return;
		}

		_playBg!.Draw(new Rect(0, 0, 960, 640), new Rect(0, 0, 1536, 1024));

		// 플레이어 (좌측) - Idle 10프레임, HP바 머리 바로 위
		int pFrame = (int)(TotalTime * 10) % 10;
		_player!.Draw(new Rect(60, 250, 360, 240), new Rect(pFrame * 120, 0, 120, 80));
		_hpBar!.Draw(new Rect(165, 324, 150, 58), new Rect(0, 0, 568, 218));

		// 몬스터 (우측) - 고블린 Idle 4프레임, 좌우반전, HP바 머리 바로 위
		int mFrame = (int)(TotalTime * 6) % 4;
		float cx = 620 + 160f;
		RenderTarget.Transform = Matrix3x2.CreateScale(-1f, 1f, new System.Numerics.Vector2(cx, 0));
		_monster!.Draw(new Rect(620, 260, 320, 320), new Rect(mFrame * 150, 0, 150, 150));
		RenderTarget.Transform = Matrix3x2.Identity;
		_hpBar!.Draw(new Rect(705, 344, 150, 58), new Rect(0, 0, 568, 218));

		// 세팅 버튼 (우상단)
		_btnSettings!.Draw(new Rect(855, 8, 90, 32), new Rect(0, 0, 136, 49));

		// 세팅 패널
		if (_showSettings)
		{
			_uiFrame!.Draw(new Rect(280, 80, 400, 462), new Rect(0, 0, 739, 853));
			_btnMainMenu!.Draw(new Rect(340, 230, 280, 46), new Rect(0, 0, 220, 36));
			_btnGameOptions!.Draw(new Rect(340, 320, 280, 48), new Rect(0, 0, 264, 45));
			_btnQuitGame!.Draw(new Rect(340, 390, 280, 54), new Rect(0, 0, 224, 43));
		}
	}

	public override void Dispose()
	{
		_titleBg?.Dispose();
		_playBg?.Dispose();
		_btnNewGame?.Dispose();
		_btnContinue?.Dispose();
		_player?.Dispose();
		_monster?.Dispose();
		_hpBar?.Dispose();
		_btnSettings?.Dispose();
		_uiFrame?.Dispose();
		_btnMainMenu?.Dispose();
		_btnGameOptions?.Dispose();
		_btnQuitGame?.Dispose();
		_titleFont?.Dispose();
		_titleBgm?.Dispose();
		_battleBgm?.Dispose();
		_btnClick?.Dispose();
		base.Dispose();
	}
}
