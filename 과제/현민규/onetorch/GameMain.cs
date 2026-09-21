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

  
    private static readonly Rect SpriteFrame = new(0, 0, 64, 64);
    private static readonly Color4 TextColor = new(0.94f, 0.91f, 0.82f, 1);

    protected override void Initialize()
    {
        ClearColor = new Color4(0.02f, 0.02f, 0.03f, 1);
        _openingTexture = new G2Texture("resource/image/title.png");
        _bgTexture = new G2Texture("resource/background/map.png");
        _playerTexture = new G2Texture("resource/character/PLAYER/ONE-TORCH-Character.png");
        _enemyTexture = new G2Texture("resource/character/ENEMY/MONSTER.png");
        _torchBarTexture = new G2Texture("resource/ui/torch bar.png");
        _labelFont = new G2Font("Malgun Gothic", 12);
        _timeFont = new G2Font("Arial", 24);
        _panelBrush = RenderTarget.CreateSolidColorBrush(new Color4(0.02f, 0.025f, 0.035f, 0.88f));
    }

    protected override void Update()
    {
        var mouse = Input.MousePosition;
        if (_showOpening && Input.IsButtonDown(MouseButtons.Left)
            && mouse.X >= 0 && mouse.X < ScreenSize.Width
            && mouse.Y >= 0 && mouse.Y < ScreenSize.Height)
        {
            _showOpening = false;
        }
    }

    protected override void Render()
    {
        if (_showOpening)
        {
            _openingTexture.Draw(new Rect(0, 0, 960, 540), new Rect(0, 0, 960, 540));
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
       
        _torchBarTexture.Draw(new Rect(22, 18, 280, 29), new Rect(94, 246, 1984, 206));
        _labelFont.DrawText("생존 시간", new Rect(330, 7, 130, 20), TextColor);
        _timeFont.DrawText("00:00", new Rect(330, 28, 130, 32), TextColor);
        _labelFont.DrawText("최고 기록", new Rect(614, 7, 112, 20), TextColor);
        _timeFont.DrawText("00:00", new Rect(614, 28, 112, 32), TextColor);

        RenderTarget.Transform = screenTransform;
    }

    private static void DrawCharacter(G2Texture texture, float centerX, float centerY)
    {
        texture.Draw(new Rect(centerX - 48, centerY - 48, 96, 96), SpriteFrame,
            1.0f, BitmapInterpolationMode.NearestNeighbor);
    }

    public override void Dispose()
    {
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
