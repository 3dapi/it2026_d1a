// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using Vortice.Mathematics;

enum GameState
{
    Title,
    Play
}

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private GameState _state = GameState.Title;

    private G2Texture? _bgTexture = null;
    private G2Texture? _titleTexture = null;
    private G2Texture? _btnStartTexture = null;
    private G2Texture? _btnExitTexture = null;

    private Rectangle _btnStartRect = new Rectangle(100, 220, 250, 160);
    private Rectangle _btnExitRect = new Rectangle(450, 220, 250, 160);

    private bool _isMousePrevDown = false;

    protected override void Initialize()
    {
        _bgTexture = new G2Texture("resource/grid_map/map.png");
        _titleTexture = new G2Texture("resource/ui/title.png");
        _btnStartTexture = new G2Texture("resource/ui/start.png");
        _btnExitTexture = new G2Texture("resource/ui/exit.png");
    }

    protected override void Update()
    {
        PointF mousePos = this.Input.MousePosition;
        Point pt = Point.Round(mousePos);
        bool isMouseDown = this.Input.IsButtonDown(MouseButtons.Left);

        if (isMouseDown && !_isMousePrevDown)
        {
            if (_state == GameState.Title)
            {
                if (_btnStartRect.Contains(pt))
                {
                    _state = GameState.Play;
                }
                else if (_btnExitRect.Contains(pt))
                {
                    Environment.Exit(0);
                }
            }
        }

        _isMousePrevDown = isMouseDown;

        if (_state == GameState.Title)
        {
            double elapsed = TotalTime;
            this.ClearColor = new Color4(
                red: (float)(Math.Sin(elapsed) * 0.5 + 0.5),
                green: (float)(Math.Sin(elapsed + Math.PI / 2.0) * 0.5 + 0.5),
                blue: (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5),
                alpha: 1.0f);
        }
        else
        {
            this.ClearColor = new Color4(0.15f, 0.15f, 0.18f, 1.0f);
        }
    }

    protected override void Render()
    {
        if (_state == GameState.Title)
        {
            _titleTexture?.Draw(200, 70);
            _btnStartTexture?.Draw(100, 220);
            _btnExitTexture?.Draw(450, 220);
        }
        else if (_state == GameState.Play)
        {
            _bgTexture?.Draw(160, 60);
        }
    }

    public override void Dispose()
    {
        _bgTexture?.Dispose();
        _titleTexture?.Dispose();
        _btnStartTexture?.Dispose();
        _btnExitTexture?.Dispose();

        _bgTexture = null;
        _titleTexture = null;
        _btnStartTexture = null;
        _btnExitTexture = null;

        base.Dispose();
    }
}