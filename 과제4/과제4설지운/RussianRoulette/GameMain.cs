// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

using System.Windows.Forms;
class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private enum GameScene
    {
        Start,
        Play,
        Result
    }

    private GameScene _currentScene = GameScene.Start;

    //배경
    private G2Texture _bgDarkTexture = null;
    private G2Texture _bgTexture = null;


    //메인 메뉴 관련
    //타이틀
    private G2Texture _titleTexture = null!;
    // 시작 버튼
    private G2Texture _startButtonNormalTexture = null!;
    private G2Texture _startButtonPressedTexture = null!;
    private bool _isMouseOnStartButton = false;


    // 종료 버튼
    private G2Texture _exitButtonNormalTexture = null!;
    private G2Texture _exitButtonPressedTexture = null!;
    private bool _isMouseOnExitButton = false;

    //인게임 관련 
    private G2Texture _enemyTexture = null!;
    private G2Texture _gunTexture = null;

    //텍스트 
    private G2Font _text = null;
    private string _mousePositionText = string.Empty;

    //사운드
    private G2AudioSound _bgm = null;
    private G2AudioSound _buttonClickSound = null!;
    private G2AudioSound _cylinderSpinSound = null;

    protected override void Initialize()
    {
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------
        var texBackgroundDir = "resource\\background\\";
        var texCharacterDir = "resource\\character\\";
        var texUIDir = "resource\\ui\\";
        var texWeaponDir = "resource\\weapon\\";

        var audioBgmDir = "resource\\audio\\bgm\\";
        var audioSfxDir = "resource\\audio\\sfx\\";
        

        //배경 설정
        _bgTexture = new G2Texture(texBackgroundDir + "room_background.png");
        _bgDarkTexture = new G2Texture(texBackgroundDir + "room_background_dark.png");

        //타이틀 
        _titleTexture = new G2Texture(texUIDir + "title.png");

        //시작 버튼 
        _startButtonNormalTexture = new G2Texture(texUIDir + "button_start_normal.png");
        _startButtonPressedTexture = new G2Texture(texUIDir + "button_start_pressed.png");

        //종료 버튼
        _exitButtonNormalTexture = new G2Texture(texUIDir + "button_exit_normal.png");
        _exitButtonPressedTexture = new G2Texture(texUIDir + "button_exit_pressed.png");

        //인게임 관련
        _enemyTexture = new G2Texture(texCharacterDir + "img_enemy_normal.png");
        _gunTexture = new G2Texture(texWeaponDir + "revolver_normal.png");

        _text = new G2Font("Arial", 18);

        //사운드 설정
        _bgm = new G2AudioSound(audioBgmDir + "longnoteone.wav");
        _buttonClickSound = new G2AudioSound(audioSfxDir + "button_click.wav");
        _cylinderSpinSound = new G2AudioSound(audioSfxDir + "cylinder_spin.wav");

        _bgm.Play(true);



    }

    protected override void Update()
    {
        //버튼 영역 판단을 위한 마우스 위치 확인용
        var mousePos = Input.MousePosition;
        _mousePositionText = $"실시간 마우스 좌표: (X: {(int)mousePos.X}, Y: {(int)mousePos.Y})";

        //시작, 종료 버튼 
        if (_currentScene == GameScene.Start)
        {
            //시작 버튼 영역에서 위에 마우스 있는 지 여부 확인
            _isMouseOnStartButton = mousePos.X >= 240 && mousePos.X <= 407 &&
                                   mousePos.Y >= 415 && mousePos.Y <= 484;

            //종료 버튼 영역에서 위에 마우스 있는 지 여부 확인
            _isMouseOnExitButton = mousePos.X >= 540 && mousePos.X <= 707 &&
                                   mousePos.Y >= 415 && mousePos.Y <= 484;

            //시작 버튼 입력
            if (_isMouseOnStartButton && Input.IsButtonDown(MouseButtons.Left))
            {
                _buttonClickSound.Play();
                _currentScene = GameScene.Play;
                _cylinderSpinSound.Play();
            }
            //종료 버튼 입력
            if (_isMouseOnExitButton && Input.IsButtonDown(MouseButtons.Left))
            {
                _buttonClickSound.Play();
                Close();
            }
        }



    }

    protected override void Render()
    {
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------

        //시작 화면 구성용
        if (_currentScene == GameScene.Start)
        {
            RenderStartScene();
        }
        else if (_currentScene == GameScene.Play)
        {
            RenderPlayScene();
        }

        //마우스 위치 출력
        _text.DrawText(_mousePositionText, new Rect(20, 20, 600, 100), new Color4(1.0f, 1.0f, 0.0f, 1.0f)
    );


    }

    public override void Dispose()
    {

        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------

        // 배경
        _bgTexture.Dispose();
        _bgDarkTexture.Dispose();

        //타이틀
        _titleTexture.Dispose();

        // 시작 버튼
        _startButtonNormalTexture.Dispose();
        _startButtonPressedTexture.Dispose();

        // 종료 버튼
        _exitButtonNormalTexture.Dispose();
        _exitButtonPressedTexture.Dispose();

        //인게임
        _enemyTexture.Dispose();
        _gunTexture.Dispose();

        //임시 텍스트
        _text.Dispose();

        //사운드
        _bgm.Dispose();
        _buttonClickSound.Dispose();
        _cylinderSpinSound.Dispose();

        base.Dispose();

    }



    private void RenderStartScene()
    {
        _bgDarkTexture.Draw();
        _titleTexture.Draw(280, 110);


        // 시작 버튼
        if (_isMouseOnStartButton)
        {
            _startButtonPressedTexture.Draw(230, 355);
        }
        else
        {
            _startButtonNormalTexture.Draw(230, 355);
        }


        // 종료 버튼
        if (_isMouseOnExitButton)
        {
            _exitButtonPressedTexture.Draw(530, 355);
        }
        else
        {
            _exitButtonNormalTexture.Draw(530, 355);
        }
    }

    private void RenderPlayScene()
    {
        _bgTexture.Draw();
        _enemyTexture.Draw(280, 110);
        _gunTexture.Draw(610, 350);
    }
}
