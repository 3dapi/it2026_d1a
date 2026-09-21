// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// MEMORY CODE - graphics resource / game start demonstration
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Text;
using System.Windows.Forms;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private enum GameState
    {
        Start,
        Memorize,
        Input,
        Correct,
        Wrong
    }

    private const float TextureX = 288.0f;
    private const float TextureY = 128.0f;
    private const double MemorizeSeconds = 2.5;
    private const double ResultSeconds = 1.2;

    // 준비한 그래픽 리소스 5개
    private G2Texture? _gameStartTexture;
    private G2Texture? _playUiTexture;
    private G2Texture? _inGameTexture;
    private G2Texture? _correctUiTexture;
    private G2Texture? _wrongUiTexture;

    private G2Font? _font;
    private GameState _state = GameState.Start;
    private double _stateTime;
    private readonly StringBuilder _input = new();

    // 과제 확인이 쉽도록 첫 번째 문제는 준비된 ingame 이미지의 숫자와 동일하게 사용
    private const string FirstCode = "729418";

    protected override void Initialize()
    {
        const string texUiDir = "resource/tex_ui/";

        // 이미지 파일을 실제 프로젝트 리소스에서 불러옵니다.
        _gameStartTexture = new G2Texture(texUiDir + "gamestart.png");
        _playUiTexture = new G2Texture(texUiDir + "playui.png");
        _inGameTexture = new G2Texture(texUiDir + "ingame.png");
        _correctUiTexture = new G2Texture(texUiDir + "correctui.png");
        _wrongUiTexture = new G2Texture(texUiDir + "wrongui.png");



        _font = new G2Font(
         "Consolas",
         25,
        Vortice.DirectWrite.FontWeight.Bold,
        Vortice.DirectWrite.FontStyle.Normal,
        Vortice.DirectWrite.TextAlignment.Center,
        Vortice.DirectWrite.ParagraphAlignment.Center);

        ClearGameState();
    }

    protected override void Update()
    {
        _stateTime += DeltaTime;

        if (Input.IsKeyDown(Keys.Escape))
        {
            Close();
            return;
        }

        switch (_state)
        {
            case GameState.Start:
                // 시작 화면에서 ENTER를 누르면 실제 게임 시작
                if (Input.IsKeyDown(Keys.Enter) || Input.IsKeyDown(Keys.Space))
                {
                    _input.Clear();
                    SetState(GameState.Memorize);
                }
                break;

            case GameState.Memorize:
                // 일정 시간 숫자를 보여준 뒤 입력 화면으로 이동
                if (_stateTime >= MemorizeSeconds)
                {
                    _input.Clear();
                    SetState(GameState.Input);
                }
                break;

            case GameState.Input:
                UpdateInput();
                break;

            case GameState.Correct:
            case GameState.Wrong:
                if (_stateTime >= ResultSeconds)
                {
                    SetState(GameState.Start);
                }
                break;
        }

        ClearColor = new Color4(0.0f, 0.005f, 0.012f, 1.0f);
    }

    private void UpdateInput()
    {
        for (int digit = 0; digit <= 9; digit++)
        {
            Keys normalKey = (Keys)((int)Keys.D0 + digit);
            Keys numPadKey = (Keys)((int)Keys.NumPad0 + digit);

            if ((Input.IsKeyDown(normalKey) || Input.IsKeyDown(numPadKey)) &&
                _input.Length < FirstCode.Length)
            {
                _input.Append((char)('0' + digit));
            }
        }

        if (Input.IsKeyDown(Keys.Back) && _input.Length > 0)
        {
            _input.Remove(_input.Length - 1, 1);
        }

        if (Input.IsKeyDown(Keys.Enter) && _input.Length == FirstCode.Length)
        {
            if (_input.ToString() == FirstCode)
            {
                SetState(GameState.Correct);
            }
            else
            {
                SetState(GameState.Wrong);
            }
        }
    }

    private void ClearGameState()
    {
        _input.Clear();
        SetState(GameState.Start);
    }

    private void SetState(GameState state)
    {
        _state = state;
        _stateTime = 0.0;
    }

    protected override void Render()
    {
        switch (_state)
        {
            case GameState.Start:
                RenderStart();
                break;

            case GameState.Memorize:
                RenderMemorize();
                break;

            case GameState.Input:
                RenderInput();
                break;

            case GameState.Correct:
                _correctUiTexture?.Draw(TextureX, TextureY);
                break;

            case GameState.Wrong:
                _wrongUiTexture?.Draw(TextureX, TextureY);
                break;
        }
    }

    private void RenderStart()
    {
        _gameStartTexture?.Draw(TextureX, TextureY);

        _font?.DrawText(
            "ENTER / SPACE : GAME START        ESC : EXIT",
            new Rect(120, 540, 720, 40),
            new Color4(0.40f, 0.95f, 0.95f, 1.0f));
    }

    private void RenderMemorize()
    {
        // 준비한 ingame.png를 실제 게임 화면에 출력합니다.
        _inGameTexture?.Draw(TextureX, TextureY);

        // 아래 안내 문구만 실제 동작에 맞게 추가합니다.
        _font?.DrawText(
            $"MEMORIZE... {Math.Max(0.0, MemorizeSeconds - _stateTime):0.0}s",
            new Rect(180, 545, 600, 40),
            new Color4(0.55f, 0.95f, 0.95f, 1.0f));
    }

    private void RenderInput()
    {
        // 준비한 playui.png를 실제 게임 화면에 출력합니다.
        _playUiTexture?.Draw(TextureX, TextureY);

        // 입력값을 이미지의 입력칸 위에 표시합니다.
        string shown = _input.Length == 0
            ? "_"
            : _input.ToString();

        _font?.DrawText(
            shown,
            new Rect( 180, 345, 575, 65),
            new Color4(1.0f, 0.0f, 0.0f, 1.0f));

        _font?.DrawText(
            "0~9 : INPUT     BACKSPACE : DELETE     ENTER : CHECK",
            new Rect(90, 545, 780, 40),
            new Color4(0.80f, 0.90f, 0.92f, 1.0f));
    }

    public override void Dispose()
    {
        _gameStartTexture?.Dispose();
        _playUiTexture?.Dispose();
        _inGameTexture?.Dispose();
        _correctUiTexture?.Dispose();
        _wrongUiTexture?.Dispose();

        _font?.Dispose();
        _gameStartTexture = null;
        _playUiTexture = null;
        _inGameTexture = null;
        _correctUiTexture = null;
        _wrongUiTexture = null;
        _font = null;

        base.Dispose();
    }
}
