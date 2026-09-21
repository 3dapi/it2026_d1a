// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
	private enum SceneType
	{
		Start,
		Play,
		GameOver
	}

	private const float StartButtonX = 490.0f;
	private const float StartButtonY = 310.0f;
	private const float ExitButtonY = 400.0f;
	private const float GameOverButtonY = 360.0f;
	private const float ButtonWidth = 300.0f;
	private const float ButtonHeight = 70.0f;
	private const float CharacterGroundY = 470.0f;
	private const float PlayerWidth = 68.75f;
	private const float PlayerHeight = 96.875f;
	private const float SpearEnemyWidth = 105.0f;
	private const float ArcherEnemyWidth = 84.375f;
	private const float EnemyHeight = 93.75f;
	private const float ArrowWidth = 45.0f;
	private const float ArrowHeight = 15.0f;
	private const float EnemySpeed = 55.0f;
	private const float ArrowSpeed = 700.0f;

	private SceneType _scene = SceneType.Start;
	private double _playStartedAt;
	private double _survivalTime;
	private int _killCount;
	private int _maxArrowCount = 1;
	private int _playerHealth = 3;
	private float _playerX = 585.0f;
	private float _leftEnemyX = 65.0f;
	private float _rightEnemyX = 1075.0f;
	private float _arrowX;
	private float _arrowY;
	private float _arrowDirection;
	private bool _arrowFlying;
	private bool _arrowAvailable = true;

	private G2Texture? _background;
	private G2Texture? _ground;
	private G2Texture? _player;
	private G2Texture? _spearEnemy;
	private G2Texture? _archerEnemy;
	private G2Texture? _heart;
	private G2Texture? _skull;
	private G2Texture? _arrow;
	private G2Texture? _crosshair;
	private G2AudioSound? _buttonSound;

	private G2Font? _titleFont;
	private G2Font? _buttonFont;
	private G2Font? _hudFont;
	private G2Font? _guideFont;

	private ID2D1SolidColorBrush? _overlayBrush;
	private ID2D1SolidColorBrush? _buttonBrush;
	private ID2D1SolidColorBrush? _buttonHoverBrush;

	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	protected override void Initialize()
	{
		_background = new G2Texture("resource/image/background/bg_forest.png");
		_ground = new G2Texture("resource/image/background/ground_forest.png");
		_player = new G2Texture("resource/image/player/player_hunter.png");
		_spearEnemy = new G2Texture("resource/image/enemy/enemy_spear.png");
		_archerEnemy = new G2Texture("resource/image/enemy/enemy_archer.png");
		_heart = new G2Texture("resource/image/ui/heart.png");
		_skull = new G2Texture("resource/image/ui/skull.png");
		_arrow = new G2Texture("resource/image/arrow/arrow_normal.png");
		_crosshair = new G2Texture("resource/image/ui/crosshair.png");
		_buttonSound = new G2AudioSound("resource/sound/button_pop.wav");

		_titleFont = new G2Font(
			"Malgun Gothic",
			64.0f,
			textAlignment: TextAlignment.Center,
			paragraphAlignment: ParagraphAlignment.Center);
		_buttonFont = new G2Font(
			"Malgun Gothic",
			30.0f,
			textAlignment: TextAlignment.Center,
			paragraphAlignment: ParagraphAlignment.Center);
		_hudFont = new G2Font(
			"Malgun Gothic",
			26.0f,
			textAlignment: TextAlignment.Center,
			paragraphAlignment: ParagraphAlignment.Center);
		_guideFont = new G2Font(
			"Malgun Gothic",
			20.0f,
			textAlignment: TextAlignment.Center,
			paragraphAlignment: ParagraphAlignment.Center);

		_overlayBrush = RenderTarget.CreateSolidColorBrush(new Color4(0.0f, 0.0f, 0.0f, 0.42f));
		_buttonBrush = RenderTarget.CreateSolidColorBrush(new Color4(0.08f, 0.18f, 0.12f, 0.92f));
		_buttonHoverBrush = RenderTarget.CreateSolidColorBrush(new Color4(0.48f, 0.32f, 0.08f, 0.96f));
		ClearColor = new Color4(0.06f, 0.10f, 0.07f, 1.0f);
	}

	protected override void Update()
	{
		switch (_scene)
		{
			case SceneType.Start:
				UpdateStartScene();
				break;
			case SceneType.Play:
				UpdatePlayScene();
				break;
			case SceneType.GameOver:
				UpdateGameOverScene();
				break;
		}
	}

	protected override void Render()
	{
		DrawBackground();

		switch (_scene)
		{
			case SceneType.Start:
				RenderStartScene();
				break;
			case SceneType.Play:
				RenderPlayScene();
				break;
			case SceneType.GameOver:
				RenderGameOverScene();
				break;
		}
	}

	public override void Dispose()
	{
		_buttonSound?.Dispose();
		_crosshair?.Dispose();
		_arrow?.Dispose();
		_skull?.Dispose();
		_heart?.Dispose();
		_archerEnemy?.Dispose();
		_spearEnemy?.Dispose();
		_player?.Dispose();
		_ground?.Dispose();
		_background?.Dispose();

		_guideFont?.Dispose();
		_hudFont?.Dispose();
		_buttonFont?.Dispose();
		_titleFont?.Dispose();

		_buttonHoverBrush?.Dispose();
		_buttonBrush?.Dispose();
		_overlayBrush?.Dispose();
		base.Dispose();
	}

	private void UpdateStartScene()
	{
		if (Input.IsKeyDown(Keys.Enter) || IsButtonClicked(StartButtonY))
		{
			StartGame();
		}
		else if (Input.IsKeyDown(Keys.Escape) || IsButtonClicked(ExitButtonY))
		{
			Close();
		}
	}

	private void UpdatePlayScene()
	{
		if (Input.IsKeyDown(Keys.Escape))
		{
			EndGame();
			return;
		}

		if (IsKeyHeld(Keys.A))
		{
			_playerX -= 300.0f * (float)DeltaTime;
		}
		if (IsKeyHeld(Keys.D))
		{
			_playerX += 300.0f * (float)DeltaTime;
		}

		_playerX = Math.Clamp(_playerX, 16.0f, 1280.0f - PlayerWidth - 16.0f);

		if (Input.IsButtonDown(MouseButtons.Left) && _arrowAvailable)
		{
			FireArrow();
		}

		UpdateArrow();
		UpdateEnemies();
	}

	private void UpdateGameOverScene()
	{
		if (Input.IsKeyDown(Keys.Enter) || Input.IsKeyDown(Keys.Escape) ||
			IsButtonClicked(GameOverButtonY))
		{
			_buttonSound!.Play();
			_scene = SceneType.Start;
		}
	}

	private void StartGame()
	{
		_buttonSound!.Play();
		_scene = SceneType.Play;
		_playStartedAt = TotalTime;
		_survivalTime = 0.0;
		_killCount = 0;
		_maxArrowCount = 1;
		_playerHealth = 3;
		_playerX = 585.0f;
		_leftEnemyX = 65.0f;
		_rightEnemyX = 1075.0f;
		_arrowFlying = false;
		_arrowAvailable = true;
	}

	private void EndGame()
	{
		_buttonSound!.Play();
		_survivalTime = Math.Max(0.0, TotalTime - _playStartedAt);
		_scene = SceneType.GameOver;
	}

	private void FireArrow()
	{
		float playerCenterX = _playerX + PlayerWidth / 2.0f;
		var mouse = Input.MousePosition;

		_arrowDirection = mouse.X < playerCenterX ? -1.0f : 1.0f;
		_arrowX = playerCenterX;
		_arrowY = CharacterGroundY - PlayerHeight / 2.0f - ArrowHeight / 2.0f;
		_arrowFlying = true;
		_arrowAvailable = false;
		_buttonSound!.Play();
	}

	private void UpdateArrow()
	{
		if (!_arrowFlying)
		{
			if (!_arrowAvailable &&
				_arrowX + ArrowWidth >= _playerX && _arrowX <= _playerX + PlayerWidth)
			{
				_arrowAvailable = true;
			}

			return;
		}

		_arrowX += ArrowSpeed * _arrowDirection * (float)DeltaTime;

		if (_arrowDirection < 0.0f &&
			_arrowX <= _leftEnemyX + SpearEnemyWidth && _arrowX + ArrowWidth >= _leftEnemyX)
		{
			_arrowFlying = false;
			_killCount++;
			_leftEnemyX = -SpearEnemyWidth;
		}
		else if (_arrowDirection > 0.0f &&
			_arrowX + ArrowWidth >= _rightEnemyX && _arrowX <= _rightEnemyX + ArcherEnemyWidth)
		{
			_arrowFlying = false;
			_killCount++;
			_rightEnemyX = 1280.0f;
		}

		if (_arrowX < 0.0f)
		{
			_arrowX = 0.0f;
			_arrowFlying = false;
		}
		else if (_arrowX > 1280.0f - ArrowWidth)
		{
			_arrowX = 1280.0f - ArrowWidth;
			_arrowFlying = false;
		}
	}

	private void UpdateEnemies()
	{
		_leftEnemyX += EnemySpeed * (float)DeltaTime;
		_rightEnemyX -= EnemySpeed * (float)DeltaTime;

		if (_leftEnemyX + SpearEnemyWidth >= _playerX && _leftEnemyX <= _playerX + PlayerWidth)
		{
			_playerHealth--;
			_leftEnemyX = -SpearEnemyWidth;
		}
		else if (_rightEnemyX + ArcherEnemyWidth >= _playerX && _rightEnemyX <= _playerX + PlayerWidth)
		{
			_playerHealth--;
			_rightEnemyX = 1280.0f;
		}

		if (_playerHealth <= 0)
		{
			EndGame();
		}
	}

	private void DrawBackground()
	{
		_background!.Draw(
			new Rect(0.0f, 0.0f, 1280.0f, 720.0f),
			new Rect(0.0f, 0.0f, 1672.0f, 941.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		_ground!.Draw(
			new Rect(0.0f, 0.0f, 1280.0f, 720.0f),
			new Rect(0.0f, 0.0f, 1774.0f, 887.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
	}

	private void RenderStartScene()
	{
		_player!.Draw(
			new Rect(350.0f, CharacterGroundY - PlayerHeight, PlayerWidth, PlayerHeight),
			new Rect(176.0f, 183.0f, 764.0f, 1080.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);

		RenderTarget.FillRectangle(new Rect(0.0f, 0.0f, 1280.0f, 720.0f), _overlayBrush!);
		_titleFont!.DrawText(
			"최종병기: 활",
			new Rect(240.0f, 80.0f, 800.0f, 100.0f),
			new Color4(1.0f, 0.88f, 0.42f, 1.0f));

		DrawButton("게임 시작", StartButtonY);
		DrawButton("게임 종료", ExitButtonY);
		_guideFont!.DrawText(
			"마우스로 버튼을 선택하세요",
			new Rect(390.0f, 500.0f, 500.0f, 40.0f),
			new Color4(0.92f, 0.92f, 0.88f, 1.0f));
	}

	private void RenderPlayScene()
	{
		_spearEnemy!.Draw(
			new Rect(_leftEnemyX, CharacterGroundY - EnemyHeight, SpearEnemyWidth, EnemyHeight),
			new Rect(157.0f, 276.0f, 837.0f, 748.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		_archerEnemy!.Draw(
			new Rect(_rightEnemyX, CharacterGroundY - EnemyHeight, ArcherEnemyWidth, EnemyHeight),
			new Rect(91.0f, 140.0f, 911.0f, 1011.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		_player!.Draw(
			new Rect(
				_playerX,
				CharacterGroundY - PlayerHeight,
				PlayerWidth,
				PlayerHeight),
			new Rect(176.0f, 183.0f, 764.0f, 1080.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);

		if (!_arrowAvailable)
		{
			_arrow!.Draw(
				new Rect(_arrowX, _arrowY, ArrowWidth, ArrowHeight),
				new Rect(0.0f, 0.0f, 2172.0f, 724.0f),
				interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		}

		for (int i = 0; i < _playerHealth; i++)
		{
			_heart!.Draw(
				new Rect(32.0f + i * 44.0f, 24.0f, 36.0f, 34.0f),
				new Rect(0.0f, 0.0f, 1305.0f, 1205.0f),
				interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		}

		TimeSpan playTime = TimeSpan.FromSeconds(Math.Max(0.0, TotalTime - _playStartedAt));
		string timeText = $"{(int)playTime.TotalMinutes:00}:{playTime.Seconds:00}";
		_hudFont!.DrawText(
			timeText,
			new Rect(540.0f, 18.0f, 200.0f, 48.0f),
			new Color4(1.0f, 1.0f, 1.0f, 1.0f));

		_skull!.Draw(
			new Rect(1010.0f, 24.0f, 36.0f, 36.0f),
			new Rect(0.0f, 0.0f, 1254.0f, 1254.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		_hudFont.DrawText(_killCount.ToString(), new Rect(1048.0f, 18.0f, 55.0f, 48.0f), new Color4(1.0f));

		_arrow!.Draw(
			new Rect(1120.0f, 28.0f, 78.0f, 26.0f),
			new Rect(0.0f, 0.0f, 2172.0f, 724.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
		_hudFont.DrawText(_arrowAvailable ? "1" : "0", new Rect(1200.0f, 18.0f, 48.0f, 48.0f), new Color4(1.0f));

		_guideFont!.DrawText(
			"A / D 이동    마우스 왼쪽 발사    화살에 닿아 회수",
			new Rect(390.0f, 660.0f, 500.0f, 36.0f),
			new Color4(1.0f, 1.0f, 1.0f, 0.9f));

		var mouse = Input.MousePosition;
		_crosshair!.Draw(
			new Rect(mouse.X - 12.0f, mouse.Y - 12.0f, 24.0f, 24.0f),
			new Rect(0.0f, 0.0f, 1254.0f, 1254.0f),
			interpolationMode: BitmapInterpolationMode.NearestNeighbor);
	}

	private void RenderGameOverScene()
	{
		RenderTarget.FillRectangle(new Rect(0.0f, 0.0f, 1280.0f, 720.0f), _overlayBrush!);
		_titleFont!.DrawText(
			"게임 오버",
			new Rect(240.0f, 90.0f, 800.0f, 100.0f),
			new Color4(0.95f, 0.30f, 0.24f, 1.0f));

		TimeSpan survivalTime = TimeSpan.FromSeconds(_survivalTime);
		string resultText =
			$"생존 시간  {(int)survivalTime.TotalMinutes:00}:{survivalTime.Seconds:00}\n" +
			$"처치 수  {_killCount}\n" +
			$"최대 활 개수  {_maxArrowCount}";
		_hudFont!.DrawText(
			resultText,
			new Rect(340.0f, 200.0f, 600.0f, 120.0f),
			new Color4(1.0f, 1.0f, 1.0f, 1.0f));

		DrawButton("메인 화면", GameOverButtonY);

		_guideFont!.DrawText(
			"Enter 또는 ESC 메인 화면",
			new Rect(390.0f, 450.0f, 500.0f, 40.0f),
			new Color4(0.92f, 0.92f, 0.88f, 1.0f));
	}

	private void DrawButton(string text, float y)
	{
		var mouse = Input.MousePosition;
		bool hovered = IsInside(mouse.X, mouse.Y, StartButtonX, y, ButtonWidth, ButtonHeight);

		RenderTarget.FillRectangle(
			new Rect(StartButtonX, y, ButtonWidth, ButtonHeight),
			hovered ? _buttonHoverBrush! : _buttonBrush!);
		_buttonFont!.DrawText(
			text,
			new Rect(StartButtonX, y, ButtonWidth, ButtonHeight),
			new Color4(1.0f, 1.0f, 1.0f, 1.0f));
	}

	private bool IsButtonClicked(float y)
	{
		var mouse = Input.MousePosition;
		return Input.IsButtonDown(MouseButtons.Left) &&
			IsInside(mouse.X, mouse.Y, StartButtonX, y, ButtonWidth, ButtonHeight);
	}

	private bool IsKeyHeld(Keys key)
	{
		G2InputContext.InputState state = Input.KeyState(key);
		return state == G2InputContext.InputState.Down || state == G2InputContext.InputState.Press;
	}

	private static bool IsInside(float pointX, float pointY, float x, float y, float width, float height)
	{
		return pointX >= x && pointX <= x + width && pointY >= y && pointY <= y + height;
	}
}
