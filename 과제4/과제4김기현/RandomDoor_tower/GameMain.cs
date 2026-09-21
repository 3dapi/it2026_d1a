// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	private G2Texture? _titleImage;
	private G2Texture? _floorImage;
	private G2Texture? _doorImage;
	private G2Texture? _leftHandImage;
	private G2Texture? _rightHandImage;
	private G2Font? _font;
	private G2AudioSound? _titleBgm;
	private G2AudioSound? _enterSound;
	private G2AudioSound? _caveBgm;
	private Form? _window;
	private bool _startRequested = false;
	private bool _gameStarted = false;

	protected override void Initialize()
	{
		//---------------------------------------
		// 게임 관련 객체를 생성합니다.
		//---------------------------------------
		_titleImage = new G2Texture("resource/main.png");
		_floorImage = new G2Texture("resource/firstfloor.png");
		_doorImage = new G2Texture("resource/door_closed.png");
		_leftHandImage = new G2Texture("resource/leftidle.png");
		_rightHandImage = new G2Texture("resource/rightidle.png");
		_font = new G2Font("Malgun Gothic", 32);

		_titleBgm = new G2AudioSound("resource/audio/Simple_Lofi_Piano_Loop.wav");
		_enterSound = new G2AudioSound("resource/audio/Piano_Ui (1).wav");
		_caveBgm = new G2AudioSound("resource/audio/Cave.wav");
		_titleBgm.Play(true); // 시작 화면 배경음 반복 재생

		_window = Control.FromHandle(RenderTarget.Hwnd) as Form;
		if (_window != null) _window.KeyDown += OnKeyDown;
	}

	protected override void Update()
	{
		//---------------------------------------
		// 게임 관련 객체를 갱신합니다.
		//---------------------------------------
		if (_startRequested)
		{
			_gameStarted = true;
			_startRequested = false;
			_titleBgm?.Stop();
			_enterSound?.Play(); // 진입 효과음은 한 번만 재생
			_caveBgm?.Play(true); // 탑 내부 배경음 반복 재생
		}
	}

	private void OnKeyDown(object? sender, KeyEventArgs e)
	{
		if (!_gameStarted && e.KeyCode == Keys.Enter && !e.Alt) _startRequested = true;
	}

	protected override void Render()
	{
		//---------------------------------------
		// 게임 관련 객체를 렌더링 합니다.
		//---------------------------------------
		Color4 textColor = new(1, 1, 1, 1);
		if (!_gameStarted)
		{
			_titleImage?.Draw();
			_font?.DrawText("Go to Top - 탑 오르기", new Rect(410, 90, 600, 60), textColor);
			_font?.DrawText("Enter 키를 눌러 시작", new Rect(430, 560, 500, 60), textColor);
			return;
		}

		// 배경 → 문 두 개 → 양손 → 현재 층 순서
		_floorImage?.Draw();
		_doorImage?.Draw(new Rect(303, 135, 250, 350), new Rect(0, 0, 304, 423));
		_doorImage?.Draw(new Rect(617, 135, 250, 350), new Rect(0, 0, 304, 423));
		_leftHandImage?.Draw(new Rect(0, 359, 450, 300), new Rect(0, 0, 1075, 717));
		_rightHandImage?.Draw(new Rect(720, 359, 450, 300), new Rect(0, 0, 1075, 717));
		_font?.DrawText("1층", new Rect(550, 30, 150, 60), textColor);
	}

	public override void Dispose()
	{
		//---------------------------------------
		// 게임 관련 객체를 해제합니다.
		//---------------------------------------
		if (_window != null) _window.KeyDown -= OnKeyDown;
		_titleImage?.Dispose();
		_floorImage?.Dispose();
		_doorImage?.Dispose();
		_leftHandImage?.Dispose();
		_rightHandImage?.Dispose();
		_font?.Dispose();
		_titleBgm?.Dispose();
		_enterSound?.Dispose();
		_caveBgm?.Dispose();
		base.Dispose();
	}
}
