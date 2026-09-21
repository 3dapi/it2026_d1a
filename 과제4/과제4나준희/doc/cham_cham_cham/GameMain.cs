// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    // 게임 시작 여부를 체크하는 상태 변수
    private bool _isPlaying = false;

    private G2Texture? _bgTexture = null;
    private G2Texture? _startTexture = null;
    private G2Texture? _enemyTexture = null;
    private G2Texture? _titleTexture = null;
    private G2Texture? _exitTexture = null;

    /*
    private G2Texture _downhandTexture = null;
    private G2Texture _uphandTexture = null;
    private G2Texture _refthandTexture = null;
    private G2Texture _lefthandTexture = null;
    private G2Texture _upTexture = null;
    private G2Texture _downTexture = null;
    private G2Texture _reftTexture = null;
    private G2Texture _leftTexture = null;
    private G2Texture _heartTexture = null;
    private G2Texture _emptyheartTexture = null;
    private G2Texture _gameoverTexture = null;
    private G2Texture _gameclearTexture = null;
    private G2Texture _defenseturnTexture = null;
    private G2Texture _attackturnTexture = null;
    */

    private G2AudioMp3? _bgm = null;

    /*
    private G2AudioMp3? _gameOverSound = null;
    private G2AudioMp3? _hitSound = null;
    private G2AudioMp3? _restartGameSound = null;
    private G2AudioMp3? _startSound = null;
    private G2AudioMp3? _uiChooseSound = null;
    private G2AudioMp3? _uiSelectSound = null;
    */

    protected override void Initialize()
    {
        var texUiDir = "resource/ui/";

        _bgTexture = new G2Texture(texUiDir + "bg/backgraund.png");
        _titleTexture = new G2Texture(texUiDir + "message/game_title.png");
        _startTexture = new G2Texture(texUiDir + "button/start_button.png");
        _enemyTexture = new G2Texture(texUiDir + "enemy/enemy.png");
        _exitTexture = new G2Texture(texUiDir + "button/exit_button.png");

        //var sndDir = "resource/sound/";
        //_bgm = new G2AudioMp3(sndDir + "bgm.mp3");

        //_bgm.Play(true);
    }

    protected override void Update()
    {
        double elapsed = TotalTime;

        this.ClearColor = new Color4(
            red: (float)(Math.Sin(elapsed) * 0.5 + 0.5),
            green: (float)(Math.Sin(elapsed + Math.PI / 2.0) * 0.5 + 0.5),
            blue: (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5),
            alpha: 1.0f);

        if (Input.IsKeyDown(Keys.Escape))
        {
            DialogResult result = MessageBox.Show("정말 종료하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        if (Input.IsButtonDown(MouseButtons.Left))
        {
            var mousePos = Input.MousePosition;
            if (!_isPlaying)
            {
                float startX = 300f;
                float startY = 600f;
                float startWidth = 200f;
                float startHeight = 80f;

                if (mousePos.X >= startX && mousePos.X <= startX + startWidth &&
                    mousePos.Y >= startY && mousePos.Y <= startY + startHeight)
                {
                    _isPlaying = true; // 게임 상태를 시작됨으로 변경
                }

                float exitX = 300f;
                float exitY = 675f;
                float exitWidth = 200f;
                float exitHeight = 80f;

                if (mousePos.X >= exitX && mousePos.X <= exitX + exitWidth &&
                    mousePos.Y >= exitY && mousePos.Y <= exitY + exitHeight)
                {
                    this.Close();
                }
            }
        }
    }

    protected override void Render()
    {
        _bgTexture?.Draw();
        _enemyTexture?.Draw(420, 250);

        if (!_isPlaying)
        {
            _titleTexture?.Draw(420, -280);
            _startTexture?.Draw(300, 600);
            _exitTexture?.Draw(300, 675);
        }
    }

    public override void Dispose()
    {
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------
        _bgTexture?.Dispose();
        _startTexture?.Dispose();
        _enemyTexture?.Dispose();
        _titleTexture?.Dispose();
        _exitTexture?.Dispose();

        //_bgm?.Dispose();

        base.Dispose();

    }
}