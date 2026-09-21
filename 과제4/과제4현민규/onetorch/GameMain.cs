using Vortice.Direct2D1;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private G2Texture _bgTexture = null!;
    private G2Texture _openingTexture = null!;
    private bool _showOpening = true;
    private G2Texture _playerTexture = null!;
    private G2Texture _enemyTexture = null!;
    private G2Texture _torchBarTexture = null!;
    private G2Font _labelFont = null!;
    private G2Font _timeFont = null!;
    private ID2D1SolidColorBrush _panelBrush = null!;
    private ID2D1SolidColorBrush _effectBrush = null!;
    private G2AudioSound? _clickSound;
    private G2AudioSound? _startSound;
    private G2AudioSound? _backgroundMusic;
    private static readonly Rect StartButton = new(350, 451, 265, 53);
    private bool _startHovered;
    private float _startTransition;
    private float _clickRemaining;
    private System.Numerics.Vector2 _clickPosition;

  
    private static readonly Rect SpriteFrame = new(0, 0, 64, 64);
    private static readonly Color4 TextColor = new(0.94f, 0.91f, 0.82f, 1);

    protected override void Initialize()
    {
        ClearColor = new Color4(0.02f, 0.02f, 0.03f, 1);
        _openingTexture = new G2Texture("resource/image/title.png");
        _bgTexture = new G2Texture("resource/background/map.png");
        _playerTexture = new G2Texture("resource/character/PLAYER/ONE-TORCH-Character.png");
        _enemyTexture = new G2Texture("resource/character/ENEMY/MONSTER.png");
        _torchBarTexture = new G2Texture("resource/ui/torch-bar-transparent.png");
        _labelFont = new G2Font("Malgun Gothic", 12);
        _timeFont = new G2Font("Arial", 24);
        _panelBrush = RenderTarget.CreateSolidColorBrush(new Color4(0.02f, 0.025f, 0.035f, 0.88f));
        _effectBrush = RenderTarget.CreateSolidColorBrush(new Color4(1, 0.65f, 0.2f, 1));
        // 오디오 출력 장치가 없는 PC에서도 화면은 정상적으로 실행합니다.
        if (G2AudioContext.Instance?.Audio != null)
        {
            _clickSound = new G2AudioSound("resource/music/click.wav");
            _startSound = new G2AudioSound("resource/music/start.wav");
            _backgroundMusic = new G2AudioSound("resource/music/dark-ambient.wav");
            _backgroundMusic.Play(true);
        }
    }

    protected override void Update()
    {
        var mouse = Input.MousePosition;
        float dt = (float)DeltaTime;
        _clickRemaining = Math.Max(0, _clickRemaining - dt);
        _startHovered = StartButton.Contains(mouse.X, mouse.Y);
        if (_startTransition > 0)
        {
            _startTransition = Math.Max(0, _startTransition - dt);
            if (_startTransition == 0) _showOpening = false;
            return;
        }
        if (Input.IsButtonDown(MouseButtons.Left)
            && mouse.X >= 0 && mouse.X < ScreenSize.Width
            && mouse.Y >= 0 && mouse.Y < ScreenSize.Height)
        {
            _clickPosition = new System.Numerics.Vector2(mouse.X, mouse.Y);
            _clickRemaining = 0.3f;
            if (_showOpening && _startHovered)
            {
                _startSound?.Play();
                _startTransition = 0.45f;
            }
            else _clickSound?.Play();
        }
    }

    protected override void Render()
    {
        if (_showOpening)
        {
            _openingTexture.Draw(new Rect(0, 0, 960, 540), new Rect(0, 0, 960, 540));
            if (_startHovered || _startTransition > 0)
            {
                _effectBrush.Color = new Color4(1, 0.65f, 0.2f, 0.7f);
                RenderTarget.DrawRectangle(StartButton, _effectBrush, 2);
            }
            RenderClickEffect();
            if (_startTransition > 0)
            {
                _effectBrush.Color = new Color4(0, 0, 0, 1 - _startTransition / 0.45f);
                RenderTarget.FillRectangle(new Rect(0, 0, 960, 540), _effectBrush);
            }
            return;
        }

        
        var screenTransform = RenderTarget.Transform;
        RenderTarget.Transform = System.Numerics.Matrix3x2.CreateTranslation(106, 0) * screenTransform;
        _bgTexture.Draw();

        
        DrawCharacter(_playerTexture, 374, 270);
        DrawCharacter(_enemyTexture, 232, 174);
        DrawCharacter(_enemyTexture, 505, 192);
        DrawCharacter(_enemyTexture, 215, 342);
        DrawCharacter(_enemyTexture, 521, 361);

        RenderTarget.FillRectangle(new Rect(0, 0, 748, 66), _panelBrush);
       
        _torchBarTexture.Draw(new Rect(22, 18, 280, 30.65f), new Rect(80, 235, 2010, 220));
        _labelFont.DrawText("생존 시간", new Rect(330, 7, 130, 20), TextColor);
        _timeFont.DrawText("00:00", new Rect(330, 28, 130, 32), TextColor);
        _labelFont.DrawText("최고 기록", new Rect(614, 7, 112, 20), TextColor);
        _timeFont.DrawText("00:00", new Rect(614, 28, 112, 32), TextColor);

        RenderTarget.Transform = screenTransform;
        RenderClickEffect();
    }

    private void RenderClickEffect()
    {
        if (_clickRemaining <= 0) return;
        float progress = 1 - _clickRemaining / 0.3f;
        float radius = 5 + progress * 22;
        _effectBrush.Color = new Color4(1, 0.65f, 0.2f, 1 - progress);
        RenderTarget.DrawEllipse(new Ellipse(_clickPosition, radius, radius), _effectBrush, 2);
    }

    private static void DrawCharacter(G2Texture texture, float centerX, float centerY)
    {
        texture.Draw(new Rect(centerX - 48, centerY - 48, 96, 96), SpriteFrame,
            1.0f, BitmapInterpolationMode.NearestNeighbor);
    }

    public override void Dispose()
    {
        _backgroundMusic?.Dispose();
        _startSound?.Dispose();
        _clickSound?.Dispose();
        _effectBrush?.Dispose();
        _openingTexture?.Dispose();
        _bgTexture?.Dispose();
        _playerTexture?.Dispose();
        _enemyTexture?.Dispose();
        _torchBarTexture?.Dispose();
        _labelFont?.Dispose();
        _timeFont?.Dispose();
        _panelBrush?.Dispose();
        base.Dispose();
    }
}
