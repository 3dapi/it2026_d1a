using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    G2Texture background = null!;
    G2Texture runner = null!;
    G2Texture jumper = null!;
    G2Font title = null!;
    G2Font buttonFont = null!;
    ID2D1SolidColorBrush black = null!;
    ID2D1SolidColorBrush white = null!;
    RectangleF startButton = new RectangleF(420, 350, 440, 82);
    RectangleF exitButton = new RectangleF(420, 454, 440, 82);
    int selectedButton = 0; 

    protected override void Initialize()
    {
        ClearColor = new Color4(1, 1, 1, 1);
        background = new G2Texture("resource/menu/background.png");
        runner = new G2Texture("resource/menu/run 3.png");
        jumper = new G2Texture("resource/menu/jump.png");
        title = new G2Font("맑은 고딕", 52, FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal, TextAlignment.Center, ParagraphAlignment.Center);
        buttonFont = new G2Font("맑은 고딕", 36, FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal, TextAlignment.Center, ParagraphAlignment.Center);
        black = RenderTarget.CreateSolidColorBrush(new Color4(0, 0, 0, 1));
        white = RenderTarget.CreateSolidColorBrush(new Color4(1, 1, 1, 1));
    }

    protected override void Update()
    {
        if (Input.IsKeyDown(Keys.D1) || Input.IsKeyDown(Keys.NumPad1))
        {
            selectedButton = 1;
        }
        else if (Input.IsKeyDown(Keys.D2) || Input.IsKeyDown(Keys.NumPad2))
        {
            if (selectedButton == 2)
            {
                Close(); 
            }
            else
            {
                selectedButton = 2;
            }
        }
    }
    protected override void Render()
    {
        background.Draw(new Rect(0, 355, 1280, 360), new Rect(0, 0, 1669, 942));
        RenderTarget.FillRectangle(new Rect(48, 240, 282, 300), white);
        runner.Draw(new Rect(60, 260, 256, 256), new Rect(0, 0, 256, 256), 1, BitmapInterpolationMode.NearestNeighbor);
        RenderTarget.FillRectangle(new Rect(942, 48, 270, 270), white);
        jumper.Draw(new Rect(948, 52, 256, 256), new Rect(0, 0, 256, 256), 1, BitmapInterpolationMode.NearestNeighbor);
        RenderTarget.FillRectangle(new Rect(374, 102, 540, 512), black);
        RenderTarget.FillRectangle(new Rect(366, 94, 540, 512), white);
        RenderTarget.DrawRectangle(new Rect(366, 94, 540, 512), black, 4);
        title.DrawText("장애물 달리기!", new Rect(388, 155, 496, 108), new Color4(0, 0, 0, 1));
        DrawButton(startButton, "게임 시작", selectedButton == 1);
        DrawButton(exitButton, "나가기", selectedButton == 2);
    }

    void DrawButton(RectangleF box, string text, bool selected)
    {
        Rect area = new Rect(box.X, box.Y, box.Width, box.Height);
        RenderTarget.FillRectangle(area, selected ? black : white);
        RenderTarget.DrawRectangle(area, black, 3);
        Color4 textColor = selected ? new Color4(1, 1, 1, 1) : new Color4(0, 0, 0, 1);
        buttonFont.DrawText(text, area, textColor);
    }

    public override void Dispose()
    {
        background?.Dispose();
        runner?.Dispose();
        jumper?.Dispose();
        title?.Dispose();
        buttonFont?.Dispose();
        black?.Dispose();
        white?.Dispose();
        base.Dispose();
    }
}


