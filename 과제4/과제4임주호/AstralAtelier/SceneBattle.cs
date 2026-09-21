using Vortice.Mathematics;

internal sealed class SceneBattle : IDisposable
{
    private G2Texture? _background;
    private G2Texture? _player;
    private G2Texture? _enemy;

    public void Initialize()
    {
        _background = new G2Texture(
            "resource/background/dungueon.png");

        _player = new G2Texture(
            "resource/character/charaterbattle.png");

        _enemy = new G2Texture(
            "resource/enemy/obmon.png");
    }

    public bool Update()
    {
        G2InputContext input = G2AppBase.Instance?.Input
            ?? throw new InvalidOperationException(
                "G2AppBase instance is not initialized.");

        return input.IsKeyDown(Keys.Escape);
    }

    public void Render()
    {
        _background?.Draw(
            new Rect(0, 0, 960, 640),
            new Rect(0, 0, 1536, 1024));

        _player?.Draw(
            new Rect(40, 300, 300, 300),
            new Rect(0, 0, 1254, 1254));

        _enemy?.Draw(
            new Rect(570, 230, 330, 312),
            new Rect(0, 0, 1291, 1218));
    }

    public void Dispose()
    {
        _enemy?.Dispose();
        _player?.Dispose();
        _background?.Dispose();
    }
}
