using System.Numerics;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

// 제공된 PNG를 배경 → 문 → 손 → 안내 UI 순서로 출력합니다.
sealed class GameScene : IDisposable
{
    private const float Width = 1170;
    private const float Height = 659;
    private static readonly Rect Screen = new(0, 0, Width, Height);
    private static readonly Color4 White = new(0.94f, 0.96f, 1, 1);
    private static readonly Color4 Gold = new(0.91f, 0.75f, 0.43f, 1);
    private static readonly Color4 Blue = new(0.40f, 0.82f, 1, 1);
    private static readonly string[] Floors =
        ["firstfloor", "secondfloor", "thirdfloor", "fourthfloor", "fifthfloor"];

    private readonly GameSession _game;
    private readonly Dictionary<string, (G2Texture Texture, Rect Source)> _images = new();
    private G2Font? _titleFont;
    private G2Font? _headingFont;
    private G2Font? _bodyFont;
    private G2Font? _smallFont;
    private ID2D1SolidColorBrush? _brush;

    public GameScene(GameSession game) => _game = game;

    public void Initialize()
    {
        string[] names = ["main", .. Floors, "door_closed", "door_half", "door_full",
            "leftidle", "leftselect", "rightidle", "rightselect", "success_ending", "fail_ending"];
        foreach (string name in names)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "resource", name + ".png");
            using var image = System.Drawing.Image.FromFile(path);
            _images.Add(name, (new G2Texture(path), new Rect(0, 0, image.Width, image.Height)));
        }
        _titleFont = Font(58);
        _headingFont = Font(30);
        _bodyFont = Font(21);
        _smallFont = Font(16);
        _brush = Target.CreateSolidColorBrush(White);
    }

    private static ID2D1RenderTarget Target => G2AppBase.Instance!.RenderTarget;
    private static G2Font Font(float size) => new("Malgun Gothic", size,
        FontWeight.Bold, Vortice.DirectWrite.FontStyle.Normal,
        TextAlignment.Center, ParagraphAlignment.Center);

    public void Render()
    {
        switch (_game.Phase)
        {
            case GamePhase.Title: RenderTitle(); break;
            case GamePhase.Playing:
            case GamePhase.Entering: RenderFloor(); break;
            default: RenderEnding(); break;
        }

        // 새 층과 엔딩은 검은 화면에서 자연스럽게 나타납니다.
        if (_game.Phase != GamePhase.Title && _game.SceneTime < 0.35)
            Fill(Screen, new Color4(0, 0, 0, 1 - (float)(_game.SceneTime / 0.35)));
    }

    private void RenderTitle()
    {
        Draw("main", Screen);
        Fill(Screen, new Color4(0.01f, 0.025f, 0.06f, 0.18f));
        Fill(new Rect(48, 84, 470, 500), new Color4(0.015f, 0.03f, 0.07f, 0.85f));
        Fill(new Rect(78, 114, 410, 2), Gold);
        Text(_smallFont, "운명을 고르는 다섯 번의 도전", new Rect(78, 138, 410, 30), Gold);
        Text(_titleFont, "GO TO TOP", new Rect(60, 182, 448, 118), White);
        Text(_headingFont, "탑 오르기", new Rect(78, 304, 410, 48), Gold);
        Text(_bodyFont, "두 개의 문, 하나의 길.\n5층 너머의 보물을 찾아보세요.",
            new Rect(78, 363, 410, 64), White);
        Button("ENTER   ·   게임 시작", new Rect(105, 454, 356, 58));
        Text(_smallFont, "A / D  문 선택     ENTER  선택 확정", new Rect(78, 529, 410, 28), White);
        Text(_smallFont, "GO TO TOP   /   CHOOSE YOUR FATE", new Rect(730, 604, 405, 26), Gold);
    }

    private void RenderFloor()
    {
        bool entering = _game.Phase == GamePhase.Entering;
        float progress = Math.Clamp((float)((_game.EntryTime - 0.25) / 1.05), 0, 1);
        float eased = progress * progress * (3 - 2 * progress);
        var original = Target.Transform;
        try
        {
            if (entering)
            {
                float doorX = _game.SelectedDoor == DoorSide.Left ? 428 : 742;
                // 선택한 문 중심으로 카메라를 옮기며 확대합니다.
                var center = new Vector2(Width / 2, Height / 2);
                var camera = Vector2.Lerp(center, new Vector2(doorX, 306), eased);
                Target.Transform = Matrix3x2.CreateTranslation(-camera)
                    * Matrix3x2.CreateScale(1 + 1.6f * eased)
                    * Matrix3x2.CreateTranslation(center) * original;
            }
            Draw(Floors[_game.Floor - 1], Screen);
            DrawDoor(DoorSide.Left, 428);
            DrawDoor(DoorSide.Right, 742);
        }
        finally { Target.Transform = original; }

        float handOpacity = entering ? 1 - eased : 1;
        float bob = entering ? 0 : (float)Math.Sin(_game.PlayTime * 2.4) * 3;
        Draw(_game.SelectedDoor == DoorSide.Left ? "leftselect" : "leftidle",
            new Rect(0, 359 + bob, 450, 300), handOpacity);
        Draw(_game.SelectedDoor == DoorSide.Right ? "rightselect" : "rightidle",
            new Rect(720, 359 - bob, 450, 300), handOpacity);

        Fill(new Rect(0, 0, Width, 78), new Color4(0.01f, 0.025f, 0.055f, 0.88f));
        Text(_headingFont, "GO TO TOP", new Rect(24, 18, 215, 44), Gold);
        for (int i = 1; i <= GameSession.TopFloor; i++)
        {
            Rect step = new(440 + (i - 1) * 60, 24, 46, 34);
            Fill(step, i == _game.Floor ? new Color4(0.12f, 0.39f, 0.56f, 1)
                : new Color4(0.09f, 0.13f, 0.18f, 1));
            Text(_smallFont, i.ToString(), step, i <= _game.Floor ? Gold : White);
        }
        Text(_headingFont, $"{_game.Floor} / 5 층", new Rect(948, 17, 190, 44), White);

        if (!entering)
        {
            DoorLabel(DoorSide.Left, new Rect(327, 498, 202, 36), "A   왼쪽 문");
            DoorLabel(DoorSide.Right, new Rect(641, 498, 202, 36), "D   오른쪽 문");
            Fill(new Rect(315, 95, 540, 42), new Color4(0.01f, 0.025f, 0.055f, 0.84f));
            Text(_bodyFont, _game.Message, new Rect(315, 95, 540, 42), White);
        }

        Fill(new Rect(0, 600, Width, 59), new Color4(0.01f, 0.025f, 0.055f, 0.91f));
        string hint = entering ? "문 너머로 이동 중…"
            : _game.SelectedDoor == null ? "A / D  문 선택    ·    ENTER  확정    ·    ESC  시작 화면"
            : $"{(_game.SelectedDoor == DoorSide.Left ? "왼쪽" : "오른쪽")} 문 선택됨    ·    ENTER  진입    ·    A / D  변경    ·    ESC  시작 화면";
        Text(_bodyFont, hint, new Rect(155, 610, 860, 34), White);

        if (entering)
        {
            float fade = Math.Clamp((float)((_game.EntryTime - 0.9) / 0.4), 0, 1);
            Fill(Screen, new Color4(0, 0, 0, fade));
        }
    }

    private void DrawDoor(DoorSide side, float centerX)
    {
        bool selected = _game.SelectedDoor == side;
        string name = selected ? (_game.Phase == GamePhase.Entering ? "door_full" : "door_half") : "door_closed";
        Rect source = _images[name].Source;
        float width = 350 * (float)(source.Width / source.Height);
        Draw(name, new Rect(centerX - width / 2, 135, width, 350));
    }

    private void DoorLabel(DoorSide side, Rect bounds, string label)
    {
        bool selected = _game.SelectedDoor == side;
        Fill(bounds, new Color4(0.01f, 0.025f, 0.055f, 0.9f));
        Text(_bodyFont, selected ? label + "  ✓" : label, bounds, selected ? Blue : White);
    }

    private void RenderEnding()
    {
        bool clear = _game.Phase == GamePhase.Clear;
        string name = clear ? "success_ending" : "fail_ending";
        // 엔딩 원본 비율을 유지합니다. 실패 그림에는 양옆 여백이 생깁니다.
        Fill(Screen, new Color4(0.015f, 0.025f, 0.045f, 1));
        Rect source = _images[name].Source;
        float scale = Math.Min(Width / (float)source.Width, Height / (float)source.Height);
        float w = (float)source.Width * scale, h = (float)source.Height * scale;
        Draw(name, new Rect((Width - w) / 2, (Height - h) / 2, w, h));
        Fill(new Rect(0, 0, Width, 120), new Color4(0.01f, 0.025f, 0.055f, 0.9f));
        Text(_headingFont, clear ? "탑의 보물을 획득했습니다!" : "탑 아래로 떨어졌습니다", new Rect(180, 16, 810, 55), Gold);
        Text(_bodyFont, clear ? "5개의 층을 모두 통과했습니다." : "1층에서 잘못된 문을 선택했습니다. 다시 도전해 보세요.",
            new Rect(100, 75, 970, 30), White);
        Fill(new Rect(0, 497, Width, 162), new Color4(0.01f, 0.025f, 0.055f, 0.93f));
        string time = TimeSpan.FromSeconds(_game.PlayTime).ToString(@"mm\:ss");
        Text(_bodyFont, $"문 선택 {_game.Choices}회   ·   플레이 시간 {time}", new Rect(180, 510, 810, 35), White);
        Button("ENTER / R   다시 도전", new Rect(398, 556, 374, 51));
        Text(_smallFont, "ESC   시작 화면으로", new Rect(398, 617, 374, 28), White);
    }

    private void Button(string label, Rect bounds)
    {
        Fill(bounds, new Color4(0.10f, 0.25f, 0.36f, 0.98f));
        _brush!.Color = Gold;
        Target.DrawRectangle(bounds, _brush, 1.5f);
        Text(_bodyFont, label, bounds, Gold);
    }

    private void Draw(string name, Rect destination, float opacity = 1)
    {
        var image = _images[name];
        image.Texture.Draw(destination, image.Source, opacity);
    }

    private void Fill(Rect bounds, Color4 color)
    {
        _brush!.Color = color;
        Target.FillRectangle(bounds, _brush);
    }

    private static void Text(G2Font? font, string text, Rect bounds, Color4 color) =>
        font?.DrawText(text, bounds, color);

    public void Dispose()
    {
        foreach (var image in _images.Values) image.Texture.Dispose();
        _images.Clear();
        _titleFont?.Dispose();
        _headingFont?.Dispose();
        _bodyFont?.Dispose();
        _smallFont?.Dispose();
        _brush?.Dispose();
    }
}
