// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;
using System.Numerics;

class GameMain : G2AppBase
{
	private enum ScreenState { Title, Play }
	private ScreenState _screenState = ScreenState.Title;

	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	private G2Texture? _background;
	private G2Texture? _titleBackground;
	private G2Texture? _stone;
	private G2Texture? _hardStone;
	private G2Texture? _pickaxe;
	private G2Texture? _strongPickaxe;
	private G2Texture? _oxygen;
	private readonly Pickaxe _pickaxeState = new(GameGlobal.ScreenSize.Width / 2f);
	private G2Font? _font;
	private G2Font? _titleFont;
	private G2Font? _startFont;
	private GameAudio? _audio;
	private bool _lowOxygenPreview;

	protected override void Initialize()
	{
		_titleBackground = new G2Texture("resource/Robby.png");
		_background = new G2Texture("resource/InplayBG.png");
		_stone = new G2Texture("resource/Stone_Tile.png");
		_hardStone = new G2Texture("resource/HardStone_Tile.png");
		_pickaxe = new G2Texture("resource/Pickaxe.png");
		_strongPickaxe = new G2Texture("resource/Pickaxe_Strong.png");
		_oxygen = new G2Texture("resource/oxygen.png");
		_font = new G2Font("Malgun Gothic", 22);
		_titleFont = new G2Font("Malgun Gothic", 72,
			textAlignment: Vortice.DirectWrite.TextAlignment.Center);
		_startFont = new G2Font("Malgun Gothic", 28,
			textAlignment: Vortice.DirectWrite.TextAlignment.Center);
		_audio = new GameAudio();
	}

	protected override void Update()
	{
		bool hasFocus = System.Windows.Forms.Form.ActiveForm != null;
		if (_screenState == ScreenState.Title)
		{
			if (hasFocus && Input.IsKeyDown(Keys.M)) _audio?.ToggleMute();
			_audio?.Update(DeltaTime, 1f, isPlaying: false, hasFocus: hasFocus);
			if (hasFocus && (Input.IsButtonDown(MouseButtons.Left) || Input.IsKeyDown(Keys.Space)))
			{
				_screenState = ScreenState.Play;
				_audio?.Update(0, 1f, isPlaying: true, hasFocus: true);
			}
			// 시작 클릭을 스윙으로 전달하지 않습니다. Input.Reset은 누른 버튼을
			// 다음 프레임에 새 클릭으로 만들 수 있으므로 호출하지 않습니다.
			return;
		}
		// 산소 시스템 연결 전에는 F4로 20% / 100% 상태를 미리 확인합니다.
		if (hasFocus && Input.IsKeyDown(Keys.F4)) _lowOxygenPreview = !_lowOxygenPreview;
		if (hasFocus && Input.IsKeyDown(Keys.M)) _audio?.ToggleMute();
		_audio?.Update(DeltaTime, _lowOxygenPreview ? 0.2f : 1f, isPlaying: true, hasFocus: hasFocus);
		if (!hasFocus) return;
		float scale = Math.Min(ScreenSize.Width / 800f, ScreenSize.Height / 600f);
		// 회전 중에도 이미지가 화면 가장자리 밖으로 나가지 않게 여백을 둡니다.
		_pickaxeState.Update((float)DeltaTime, Input.MousePosition.X, ScreenSize.Width, 72 * scale);
		if (Input.IsButtonDown(MouseButtons.Right) && _pickaxeState.ToggleMode())
			_audio?.Play(GameSound.Mode);
		if (Input.IsButtonDown(MouseButtons.Left) && _pickaxeState.TrySwing())
			_audio?.Play(_pickaxeState.IsStrongSwing ? GameSound.StrongSwing : GameSound.Swing);
		if (Input.IsKeyDown(Keys.F1)) _audio?.Play(GameSound.RockBreak);
		if (Input.IsKeyDown(Keys.F2)) _audio?.Play(GameSound.Bounce);
		if (Input.IsKeyDown(Keys.F3))
		{
			_lowOxygenPreview = false;
			_audio?.Play(GameSound.Oxygen);
		}
		if (Input.IsKeyDown(Keys.F5)) _audio?.Play(GameSound.Cheer);
	}

	protected override void Render()
	{
		if (_screenState == ScreenState.Title)
		{
			RenderTitle();
			return;
		}
		float width = ScreenSize.Width;
		float height = ScreenSize.Height;
		float imageScale = Math.Min(width / 800.0f, height / 600.0f);
		_background?.Draw(new Rect(0, 0, width, height), new Rect(0, 0, 536, 319));

		float margin = 12 * imageScale;
		float gap = 4 * imageScale;
		float rockSize = (width - margin * 2 - gap * 9) / 10;

		// 상단에 돌을 10열 2행으로 배치합니다. Tile Sheet는 4열 4행입니다.
		for (int row = 0; row < 2; row++)
		{
			for (int column = 0; column < 10; column++)
			{
				Rect destination = new(
					margin + column * (rockSize + gap),
					margin + row * (rockSize + gap),
					rockSize, rockSize);
				int tileIndex = (row * 10 + column) % 16;
				bool isHardStone = row == 0 && column % 3 == 1;
				int tileSize = isHardStone ? 128 : 256;
				Rect source = new(
					(tileIndex % 4) * tileSize,
					(tileIndex / 4) * tileSize,
					tileSize, tileSize);
				(isHardStone ? _hardStone : _stone)?.Draw(destination, source);
			}
		}

		// 곡괭이만 회전시킨 뒤 원래 Transform을 복구하여 UI에 영향을 주지 않습니다.
		float pickaxeSize = 100 * imageScale;
		float oxygenSize = 52 * imageScale;
		Vector2 pivot = new(_pickaxeState.X, height * 5 / 6 - 20 * imageScale * _pickaxeState.StrongLift);
		Matrix3x2 previousTransform = RenderTarget.Transform;
		try
		{
			RenderTarget.Transform = Matrix3x2.CreateRotation(_pickaxeState.Angle, pivot) * previousTransform;
			bool strongPose = _pickaxeState.UsesStrongPose;
			(strongPose ? _strongPickaxe : _pickaxe)?.Draw(
				new Rect(pivot.X - pickaxeSize / 2, pivot.Y - pickaxeSize / 2, pickaxeSize, pickaxeSize),
				strongPose ? new Rect(0, 0, 1254, 1254) : new Rect(0, 0, 640, 640));
		}
		finally
		{
			RenderTarget.Transform = previousTransform;
		}
		_oxygen?.Draw(
			new Rect(16 * imageScale, height - margin - oxygenSize, oxygenSize, oxygenSize),
			new Rect(0, 0, 360, 360));
		bool strong = _pickaxeState.IsStrongMode || _pickaxeState.IsStrongSwing;
		string mode = strong ? "강타 · I자 들어올리기" : "일반 · 기본 자세";
		string audio = _audio?.IsAvailable != true ? "출력 장치 없음" : _audio.IsMuted ? "OFF" : "ON";
		_font?.DrawText($"사운드 확인: F1 돌 · F2 반사 · F3 산소 회복 · F4 저산소 {(_lowOxygenPreview ? "ON (5초 간격)" : "OFF")} · F5 박수",
			new Rect(24, height - 132, width - 48, 34), new Color4(1f, 1f, 1f, 1f));
		_font?.DrawText($"조작 데모  |  {mode}  |  사운드 {audio}",
			new Rect(100 * imageScale, height - 90, width - 120 * imageScale, 34),
			strong ? new Color4(1f, 0.8f, 0.3f, 1f) : new Color4(1f, 1f, 1f, 1f));
		_font?.DrawText("마우스 이동 · 좌클릭 스윙 · 우클릭 강타 전환 · M 음소거",
			new Rect(100 * imageScale, height - 52, width - 120 * imageScale, 34),
			new Color4(1f, 1f, 1f, 1f));
	}

	private void RenderTitle()
	{
		float width = ScreenSize.Width;
		float height = ScreenSize.Height;
		_titleBackground?.Draw(new Rect(0, 0, width, height), new Rect(0, 0, 736, 414));
		// 밝은 배경에서도 읽을 수 있도록 글자 뒤에 짧은 그림자를 둡니다.
		_titleFont?.DrawText("ESCAPE MINE", new Rect(3, height * 0.22f + 3, width, 100),
			new Color4(0.08f, 0.04f, 0.03f, 1f));
		_titleFont?.DrawText("ESCAPE MINE", new Rect(0, height * 0.22f, width, 100),
			new Color4(1f, 0.88f, 0.62f, 1f));
		const string startText = "좌클릭 또는 SPACE를 눌러 시작";
		_startFont?.DrawText(startText, new Rect(2, height * 0.72f + 2, width, 50),
			new Color4(0.08f, 0.04f, 0.03f, 1f));
		_startFont?.DrawText(startText, new Rect(0, height * 0.72f, width, 50),
			new Color4(1f, 1f, 1f, 1f));
	}

	public override void Dispose()
	{
		_audio?.Dispose();
		_startFont?.Dispose();
		_titleFont?.Dispose();
		_titleBackground?.Dispose();
		_font?.Dispose();
		_oxygen?.Dispose();
		_pickaxe?.Dispose();
		_strongPickaxe?.Dispose();
		_hardStone?.Dispose();
		_stone?.Dispose();
		_background?.Dispose();
		base.Dispose();
	}
}
