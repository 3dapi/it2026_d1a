// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------
using System.Windows.Forms;
using NAudio.Wave;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using System.Collections.Generic;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private enum ButtonAction
    {
        None,
        Start,
        Quit,
        Hit,
        Stand,
        Restart
    }

    // Scene
    private enum SceneType
    {
        Main,
        Play,
        Ending
    }

    private SceneType _scene = SceneType.Main;

    private ButtonAction _pendingAction = ButtonAction.None;

    // 클릭된 Sprite를 잠깐 보여주기 위한 시간
    private double _actionTime = 0.0;

    private const double ButtonPressTime = 0.05;

    // Texture
    private G2Font? _titleFont = null;
    private G2Font? _scoreFont = null;

    // 공통
    private G2Texture? _bgTable = null;

    // Main
    private G2Texture? _bgStart = null;
    private G2Texture? _bgStartClicked = null;
    private G2Texture? _bgQuit = null;
    private G2Texture? _bgQuitClicked = null;

    // Play
    private G2Texture? _bgHit = null;
    private G2Texture? _bgHitClicked = null;
    private G2Texture? _bgStand = null;
    private G2Texture? _bgStandClicked = null;
    private G2Texture? _bgCardBack = null;

    // Ending
    private G2Texture? _bgWin = null;
    private G2Texture? _bgLose = null;
    private G2Texture? _bgDraw = null;
    private G2Texture? _bgRestart = null;
    private G2Texture? _bgRestartClicked = null;

    // 카드
    private G2Texture? _spade2 = null;
    private G2Texture? _spade3 = null;
    private G2Texture? _spade4 = null;
    private G2Texture? _spade5 = null;
    private G2Texture? _spade6 = null;
    private G2Texture? _spade7 = null;
    private G2Texture? _spade8 = null;
    private G2Texture? _spade9 = null;
    private G2Texture? _spade10 = null;

    private G2Texture? _heart2 = null;
    private G2Texture? _heart3 = null;
    private G2Texture? _heart4 = null;
    private G2Texture? _heart5 = null;
    private G2Texture? _heart6 = null;
    private G2Texture? _heart7 = null;
    private G2Texture? _heart8 = null;
    private G2Texture? _heart9 = null;
    private G2Texture? _heart10 = null;

    private G2Texture? _clover2 = null;
    private G2Texture? _clover3 = null;
    private G2Texture? _clover4 = null;
    private G2Texture? _clover5 = null;
    private G2Texture? _clover6 = null;
    private G2Texture? _clover7 = null;
    private G2Texture? _clover8 = null;
    private G2Texture? _clover9 = null;
    private G2Texture? _clover10 = null;

    private G2Texture? _diamond2 = null;
    private G2Texture? _diamond3 = null;
    private G2Texture? _diamond4 = null;
    private G2Texture? _diamond5 = null;
    private G2Texture? _diamond6 = null;
    private G2Texture? _diamond7 = null;
    private G2Texture? _diamond8 = null;
    private G2Texture? _diamond9 = null;
    private G2Texture? _diamond10 = null;
    
    // Button State
    private bool _startPressed = false;
    private bool _quitPressed = false;
    private bool _hitPressed = false;
    private bool _standPressed = false;
    private bool _restartPressed = false;

    // Card Size / Position

    // 원본 카드 크기
    private const float CardSourceWidth = 290.0f;
    private const float CardSourceHeight = 531.0f;

    // 화면에 표시할 카드 크기
    private const float CardWidth = 145.0f;
    private const float CardHeight = 265.5f;

    // 딜러 : 중앙 위
    private const float DealerY = 45.0f;

    // 딜러 카드 사이 간격
    private const float DealerCardGap = 165.0f;

    // 플레이어 : 중앙 아래
    private const float PlayerY = 340.0f;

    // 플레이어 카드는 여러 장이 생기므로 조금씩 겹쳐 배치
    private const float PlayerCardGap = 75.0f;



    // 공통
    private const float TableX = 100.0f;
    private const float TableY = 10.0f;

    private const float TableWidth = 800.0f;
    private const float TableHeight = 600.0f;

    // Main
    private const float StartX = 200.0f;
    private const float StartY = 300.0f;

    private const float QuitX = 200.0f;
    private const float QuitY = 430.0f;

    // Play
    private const float HitX = 100.0f;
    private const float HitY = 200.0f;

    private const float StandX = 100.0f;
    private const float StandY = 300.0f;


    // Ending
    private const float RestartX = 200.0f;
    private const float RestartY = 430.0f;


    //button Scale
    private const float ButtonWidth = 190.0f;
    private const float ButtonHeight = 100.0f;

    private const float ButtonSourceWidth = 382.0f;
    private const float ButtonSourceHeight = 203.0f;

    private const float ResultX = 330.0f;
    private const float ResultY = 170.0f;

    private const float ResultWidth = 300.0f;
    private const float ResultHeight = 150.0f;

    private const float ResultSourceWidth = 436;
    private const float ResultSourceHeight = 186;

    // Sound

    private WaveOut? _buttonSoundOutput = null;
    private AudioFileReader? _buttonSoundReader = null;

    private WaveOut? _cardSoundOutput = null;
    private AudioFileReader? _cardSoundReader = null;

    private WaveOut? _winSoundOutput = null;
    private AudioFileReader? _winSoundReader = null;

    private WaveOut? _loseSoundOutput = null;
    private AudioFileReader? _loseSoundReader = null;

    private WaveOut? _drawSoundOutput = null;
    private AudioFileReader? _drawSoundReader = null;


    // Game Data

    private int _playerTotal = 0;
    private int _dealerTotal = 0;

    private bool _playerTurn = true;
    private bool _isWin = false;
    private bool _isLose = false;
    private bool _isDraw = false;

    private bool _resultWaiting = false;
    private bool _nextResultWin = false;

    private double _resultTime = 0.0;

    private const double ResultWaitTime = 1.2;

    private string _playMessage = "";

    // 딜러가 현재 카드를 뽑고 있는 중인지
    private bool _dealerPlaying = false;

    // 다음 딜러 행동 시간
    private double _dealerNextActionTime = 0.0;

    // 딜러 카드 한 장당 대기 시간
    private const double DealerDrawDelay = 0.7;


    // Card Data

    private class CardInfo
    {
        public G2Texture Texture;
        public int Value;

        public CardInfo(G2Texture texture, int value)
        {
            Texture = texture;
            Value = value;
        }
    }


    // 전체 카드 덱
    private readonly List<CardInfo> _deck = new();

    // 플레이어가 현재 가지고 있는 카드
    private readonly List<CardInfo> _playerCards = new();

    // 딜러가 현재 가지고 있는 카드
    private readonly List<CardInfo> _dealerCards = new();

    // 랜덤 카드 선택용
    private readonly Random _random = new();

    // 딜러의 숨겨진 카드를 공개했는지
    private bool _dealerReveal = false;


    protected override void Initialize()
    {
        _titleFont = new G2Font(
        "Malgun Gothic",
        48.0f,
        textAlignment: TextAlignment.Center,
        paragraphAlignment: ParagraphAlignment.Center);

        _scoreFont = new G2Font(
        "Malgun Gothic",
        28.0f,
        textAlignment: TextAlignment.Leading,
        paragraphAlignment: ParagraphAlignment.Center);

        _cardSoundReader =
        new AudioFileReader("resource/Sound/Card_Flip.wav");

        _winSoundReader =
        new AudioFileReader("resource/Sound/Clear_Game.wav");

        _loseSoundReader =
        new AudioFileReader("resource/Sound/Lose_Game.wav");

        _loseSoundOutput = new WaveOut();
        _loseSoundOutput.Init(_loseSoundReader);


        _drawSoundReader =
        new AudioFileReader("resource/Sound/Draw_Game.wav");

        _drawSoundOutput = new WaveOut();
        _drawSoundOutput.Init(_drawSoundReader);

        _winSoundOutput = new WaveOut();
        _winSoundOutput.Init(_winSoundReader);

        _cardSoundOutput = new WaveOut();
        _cardSoundOutput.Init(_cardSoundReader);

        //sound
        _buttonSoundReader =
        new AudioFileReader("resource/Sound/Clicked_Button.wav");

        _buttonSoundOutput = new WaveOut();
        _buttonSoundOutput.Init(_buttonSoundReader);

        // 공통
        _bgTable = new("resource/Table_UI.png");

        // Main
        _bgStart = new("resource/GameStart_UI.png");
        _bgStartClicked = new("resource/GameStart_Clicked_UI.png");
        _bgQuit = new("resource/GameQuit_UI.png");
        _bgQuitClicked = new("resource/GameQuit_Clicked_UI.png");

        // Play
        _bgHit = new("resource/Hit_UI.png");
        _bgHitClicked = new("resource/Hit_Clicked_UI.png");
        _bgStand = new("resource/Stand_UI.png");
        _bgStandClicked = new("resource/Stand_Clicked_UI.png");
        _bgCardBack = new("resource/Card_Back_UI.png");

        // Ending
        _bgWin = new("resource/Win_UI.png");
        _bgLose = new("resource/Lose_UI.png");
        _bgDraw = new("resource/Draw_UI.png");
        _bgRestart = new("resource/ReStart_UI.png");
        _bgRestartClicked = new("resource/ReStart_Clicked_UI.png");

        // 카드
        _spade2 = new("resource/Spade_2.png");
        _spade3 = new("resource/Spade_3.png");
        _spade4 = new("resource/Spade_4.png");
        _spade5 = new("resource/Spade_5.png");
        _spade6 = new("resource/Spade_6.png");
        _spade7 = new("resource/Spade_7.png");
        _spade8 = new("resource/Spade_8.png");
        _spade9 = new("resource/Spade_9.png");
        _spade10 = new("resource/Spade_10.png");

        _heart2 = new("resource/Heart_2.png");
        _heart3 = new("resource/Heart_3.png");
        _heart4 = new("resource/Heart_4.png");
        _heart5 = new("resource/Heart_5.png");
        _heart6 = new("resource/Heart_6.png");
        _heart7 = new("resource/Heart_7.png");
        _heart8 = new("resource/Heart_8.png");
        _heart9 = new("resource/Heart_9.png");
        _heart10 = new("resource/Heart_10.png");

        _clover2 = new("resource/Clover_2.png");
        _clover3 = new("resource/Clover_3.png");
        _clover4 = new("resource/Clover_4.png");
        _clover5 = new("resource/Clover_5.png");
        _clover6 = new("resource/Clover_6.png");
        _clover7 = new("resource/Clover_7.png");
        _clover8 = new("resource/Clover_8.png");
        _clover9 = new("resource/Clover_9.png");
        _clover10 = new("resource/Clover_10.png");

        _diamond2 = new("resource/Diamond_2.png");
        _diamond3 = new("resource/Diamond_3.png");
        _diamond4 = new("resource/Diamond_4.png");
        _diamond5 = new("resource/Diamond_5.png");
        _diamond6 = new("resource/Diamond_6.png");
        _diamond7 = new("resource/Diamond_7.png");
        _diamond8 = new("resource/Diamond_8.png");
        _diamond9 = new("resource/Diamond_9.png");
        _diamond10 = new("resource/Diamond_10.png");
    }

    private void DrawButton(
        G2Texture? texture,
        float x,
        float y)
    {
        texture?.Draw(
            new Rect(
                x,
                y,
                ButtonWidth,
                ButtonHeight),
            new Rect(
                0,
                0,
                ButtonSourceWidth,
                ButtonSourceHeight)
        );
    }

    private void DrawCard(
    G2Texture? texture,
    float x,
    float y)
    {
        texture?.Draw(
            new Rect(
                x,
                y,
                CardWidth,
                CardHeight),
            new Rect(
                0,
                0,
                CardSourceWidth,
                CardSourceHeight)
        );
    }


    protected override void Update()
    {
        switch (_scene)
        {
            case SceneType.Main:
                UpdateMainScene();
                break;

            case SceneType.Play:
                UpdatePlayScene();
                break;

            case SceneType.Ending:
                UpdateEndingScene();
                break;
        }

        UpdatePendingAction();
        UpdateDealerTurn();
        UpdateWaitingResult();
    }

    private void PlayButtonSound()
    {
        if (_buttonSoundOutput == null ||
            _buttonSoundReader == null)
        {
            return;
        }

        // 이미 재생 중이어도 처음부터 다시 재생
        _buttonSoundOutput.Stop();
        _buttonSoundReader.Position = 0;
        _buttonSoundOutput.Play();
    }

    private void PlayWinSound()
    {
        if (_winSoundOutput == null ||
            _winSoundReader == null)
        {
            return;
        }

        _winSoundOutput.Stop();
        _winSoundReader.Position = 0;
        _winSoundOutput.Play();
    }

    private void PlayLoseSound()
    {
        if (_loseSoundOutput == null ||
            _loseSoundReader == null)
            return;

        _loseSoundOutput.Stop();
        _loseSoundReader.Position = 0;
        _loseSoundOutput.Play();
    }

    private void PlayDrawSound()
    {
        if (_drawSoundOutput == null ||
            _drawSoundReader == null)
            return;

        _drawSoundOutput.Stop();
        _drawSoundReader.Position = 0;
        _drawSoundOutput.Play();
    }

    private void PlayCardSound()
    {
        if (_cardSoundOutput == null ||
            _cardSoundReader == null)
        {
            return;
        }

        _cardSoundOutput.Stop();
        _cardSoundReader.Position = 0;
        _cardSoundOutput.Play();
    }

    // Render
    protected override void Render()
    {
        // 모든 Scene 공통
        _bgTable?.Draw(
            new Rect(TableX, TableY, TableWidth, TableHeight),
            new Rect(0, 0, 550, 550)
        );

        switch (_scene)
        {
            case SceneType.Main:
                RenderMainScene();
                break;

            case SceneType.Play:
                RenderPlayScene();
                break;

            case SceneType.Ending:
                RenderEndingScene();
                break;
        }
    }

    // Main Scene

    private void UpdateMainScene()
    {
        // 버튼 동작 대기 중에는 추가 클릭 방지
        if (_pendingAction != ButtonAction.None)
            return;

        var mouse = Input.MousePosition;

        bool startInside = IsInside(
            mouse.X,
            mouse.Y,
            StartX,
            StartY,
            ButtonWidth,
            ButtonHeight);

        bool quitInside = IsInside(
            mouse.X,
            mouse.Y,
            QuitX,
            QuitY,
            ButtonWidth,
            ButtonHeight);


        if (Input.IsButtonDown(MouseButtons.Left))
        {
            if (startInside)
            {
                _startPressed = true;

                QueueAction(ButtonAction.Start);

                // 나중에 여기에:
                // PlayButtonSound();
            }
            else if (quitInside)
            {
                _quitPressed = true;

                QueueAction(ButtonAction.Quit);

                // PlayButtonSound();
            }
        }
    }

    private void RenderMainScene()
    {
        if (_startPressed)
            DrawButton(_bgStartClicked, StartX, StartY);
        else
            DrawButton(_bgStart, StartX, StartY);

        if (_quitPressed)
            DrawButton(_bgQuitClicked, QuitX, QuitY);
        else
            DrawButton(_bgQuit, QuitX, QuitY);
    }


    // Play Scene
    private void UpdatePlayScene()
    {
        // 딜러 턴 또는 결과 처리 중에는
        // 플레이어 버튼 입력 금지
        if (_pendingAction != ButtonAction.None ||
            _dealerPlaying ||
            _resultWaiting)
        {
            return;
        }

        if (_pendingAction != ButtonAction.None)
            return;

        var mouse = Input.MousePosition;

        bool hitInside = IsInside(
            mouse.X,
            mouse.Y,
            HitX,
            HitY,
            ButtonWidth,
            ButtonHeight);

        bool standInside = IsInside(
            mouse.X,
            mouse.Y,
            StandX,
            StandY,
            ButtonWidth,
            ButtonHeight);


        if (Input.IsButtonDown(MouseButtons.Left))
        {
            if (hitInside)
            {
                _hitPressed = true;

                QueueAction(ButtonAction.Hit);

                // PlayButtonSound();
            }
            else if (standInside)
            {
                _standPressed = true;

                QueueAction(ButtonAction.Stand);

                // PlayButtonSound();
            }
        }
    }

    private void RenderPlayScene()
    {

        // 딜러 카드 - 중앙 위
        if (_dealerCards.Count > 0)
        {
            float totalDealerWidth =
                CardWidth +
                (_dealerCards.Count - 1) * DealerCardGap;

            float dealerStartX =
                (ScreenSize.Width - totalDealerWidth) / 2.0f;


            for (int i = 0; i < _dealerCards.Count; i++)
            {
                // 두 번째 카드는 Stand 전까지 뒷면
                if (i == 1 && !_dealerReveal)
                {
                    DrawCard(
                        _bgCardBack,
                        dealerStartX + i * DealerCardGap,
                        DealerY);
                }
                else
                {
                    DrawCard(
                        _dealerCards[i].Texture,
                        dealerStartX + i * DealerCardGap,
                        DealerY);
                }
            }
        }



        // 플레이어 카드 - 중앙 아래
        if (_playerCards.Count > 0)
        {
            float totalPlayerWidth =
                CardWidth +
                (_playerCards.Count - 1) * PlayerCardGap;

            float playerStartX =
                (ScreenSize.Width - totalPlayerWidth) / 2.0f;


            for (int i = 0; i < _playerCards.Count; i++)
            {
                DrawCard(
                    _playerCards[i].Texture,
                    playerStartX + i * PlayerCardGap,
                    PlayerY);
            }
        }

        // Hit
        if (_hitPressed)
            DrawButton(_bgHitClicked, HitX, HitY);
        else
            DrawButton(_bgHit, HitX, HitY);



        // Stand
        if (_standPressed)
            DrawButton(_bgStandClicked, StandX, StandY);
        else
            DrawButton(_bgStand, StandX, StandY);

        _scoreFont?.DrawText(
            $"Score : {_playerTotal}",
            new Rect(
                730,
                100,
                200,
                60),
            new Color4(
                1.0f,
                1.0f,
                1.0f,
                1.0f)
        );

        if (!string.IsNullOrEmpty(_playMessage))
        {
            _titleFont?.DrawText(
                _playMessage,
                new Rect(
                    250,
                    280,
                    500,
                    70),
                new Color4(
                    1.0f,
                    0.2f,
                    0.2f,
                    1.0f)
            );
        }
    }

    private void WaitForResult(bool win, string message)
    {
        _isDraw = false;

        _nextResultWin = win;
        _playMessage = message;

        _resultWaiting = true;
        _resultTime = TotalTime + ResultWaitTime;
    }

    private void UpdateWaitingResult()
    {
        if (!_resultWaiting)
            return;

        if (TotalTime < _resultTime)
            return;

        _resultWaiting = false;

        if (_isDraw)
        {
            PlayDrawSound();
        }

        else
        {
            _isWin = _nextResultWin;
            _isLose = !_nextResultWin;

            if (_isWin)
            {
                PlayWinSound();
            }
            else
            {
                PlayLoseSound();
            }
        }


        _scene = SceneType.Ending;
    }

    private void QueueAction(ButtonAction action)
    {
        PlayButtonSound();

        _pendingAction = action;
        _actionTime = TotalTime + ButtonPressTime;
    }

    private void UpdatePendingAction()
    {
        if (_pendingAction == ButtonAction.None)
            return;

        if (TotalTime < _actionTime)
            return;


        ButtonAction action = _pendingAction;
        _pendingAction = ButtonAction.None;


        _startPressed = false;
        _quitPressed = false;
        _hitPressed = false;
        _standPressed = false;
        _restartPressed = false;


        switch (action)
        {
            case ButtonAction.Start:
                StartGame();
                break;

            case ButtonAction.Quit:
                Close();
                break;

            case ButtonAction.Hit:
                PlayerHit();
                break;

            case ButtonAction.Stand:
                DealerTurn();
                break;

            case ButtonAction.Restart:
                ResetGame();
                _scene = SceneType.Main;
                break;
        }
    }

    // Ending Scene
    private void UpdateEndingScene()
    {
        if (_pendingAction != ButtonAction.None)
            return;

        var mouse = Input.MousePosition;

        bool restartInside = IsInside(
            mouse.X,
            mouse.Y,
            RestartX,
            RestartY,
            ButtonWidth,
            ButtonHeight);


        if (Input.IsButtonDown(MouseButtons.Left))
        {
            if (restartInside)
            {
                _restartPressed = true;

                QueueAction(ButtonAction.Restart);

                // 나중에:
                // PlayButtonSound();
            }
        }
    }

    private void DrawResult(
    G2Texture? texture,
    float sourceWidth,
    float sourceHeight)
    {
        texture?.Draw(
            new Rect(
                ResultX,
                ResultY,
                ResultWidth,
                ResultHeight),
            new Rect(
                0,
                0,
                sourceWidth,
                sourceHeight)
        );
    }

    private void RenderEndingScene()
    {
        if (_isDraw)
        {
            DrawResult(
                _bgDraw,
                ResultSourceWidth,
                ResultSourceHeight);
        }
        else if (_isWin)
        {
            DrawResult(
                _bgWin,
                ResultSourceWidth,
                ResultSourceHeight);
        }
        else
        {
            DrawResult(
                _bgLose,
                ResultSourceWidth,
                ResultSourceHeight);
        }

        if (_restartPressed)
            DrawButton(_bgRestartClicked, RestartX, RestartY);
        else
            DrawButton(_bgRestart, RestartX, RestartY);
    }


    // Game Logic
    private void StartGame()
    {
        PlayCardSound();

        _playMessage = "";
        _resultWaiting = false;
        _dealerPlaying = false;

        _scene = SceneType.Play;

        _playerCards.Clear();
        _dealerCards.Clear();

        CreateDeck();

        // 플레이어 2장
        DrawRandomCard(_playerCards);
        DrawRandomCard(_playerCards);

        // 딜러 2장
        DrawRandomCard(_dealerCards);
        DrawRandomCard(_dealerCards);

        // 처음에는 딜러의 두 번째 카드 숨김
        _dealerReveal = false;

        _isWin = false;
        _isLose = false;
        _isDraw = false;

        _playerTotal = GetCardTotal(_playerCards);
        _dealerTotal = GetCardTotal(_dealerCards);
    }

    private void CreateDeck()
    {
        _deck.Clear();

        G2Texture?[][] suits =
        {
        new G2Texture?[]
        {
            _spade2, _spade3, _spade4,
            _spade5, _spade6, _spade7,
            _spade8, _spade9, _spade10
        },

        new G2Texture?[]
        {
            _heart2, _heart3, _heart4,
            _heart5, _heart6, _heart7,
            _heart8, _heart9, _heart10
        },

        new G2Texture?[]
        {
            _clover2, _clover3, _clover4,
            _clover5, _clover6, _clover7,
            _clover8, _clover9, _clover10
        },

        new G2Texture?[]
        {
            _diamond2, _diamond3, _diamond4,
            _diamond5, _diamond6, _diamond7,
            _diamond8, _diamond9, _diamond10
        }
    };

        foreach (G2Texture?[] suit in suits)
        {
            for (int i = 0; i < suit.Length; i++)
            {
                if (suit[i] != null)
                {
                    _deck.Add(
                        new CardInfo(
                            suit[i]!,
                            i + 2
                        )
                    );
                }
            }
        }
    }

    private void DrawRandomCard(List<CardInfo> target)
    {
        if (_deck.Count == 0)
            return;

        int index = _random.Next(_deck.Count);

        target.Add(_deck[index]);

        // 같은 카드가 다시 나오지 않도록 제거
        _deck.RemoveAt(index);
    }

    private int GetCardTotal(List<CardInfo> cards)
    {
        int total = 0;

        foreach (CardInfo card in cards)
        {
            total += card.Value;
        }

        return total;
    }

    private void PlayerHit()
    {
        if (_resultWaiting)
            return;

        DrawRandomCard(_playerCards);
        PlayCardSound();

        _playerTotal = GetCardTotal(_playerCards);

        if (_playerTotal > 21)
        {
            WaitForResult(
                false,
                "21을 넘었습니다!"
            );
        }
        else if (_playerTotal == 21)
        {
            WaitForResult(
                true,
                "21!"
            );
        }
    }

    private void DealerTurn()
    {
        if (_resultWaiting || _dealerPlaying)
            return;

        // 숨겨진 카드 공개
        _dealerReveal = true;

        // 딜러 진행 시작
        _dealerPlaying = true;

        _dealerTotal = GetCardTotal(_dealerCards);

        // 숨겨진 카드를 보여줄 시간을 조금 줌
        _dealerNextActionTime =
            TotalTime + DealerDrawDelay;
    }

    private void UpdateDealerTurn()
    {
        if (!_dealerPlaying)
            return;

        // 아직 다음 행동 시간이 안 됐으면 기다림
        if (TotalTime < _dealerNextActionTime)
            return;


        _dealerTotal = GetCardTotal(_dealerCards);

        if (_dealerTotal <= 16)
        {
            DrawRandomCard(_dealerCards);

            PlayCardSound();

            _dealerTotal =
                GetCardTotal(_dealerCards);

            // 새로 받은 카드를 잠시 보여주기
            _dealerNextActionTime =
                TotalTime + DealerDrawDelay;

            return;
        }

        _dealerPlaying = false;

        CheckDealerResult();
    }

    private void CheckDealerResult()
    {
        _dealerTotal =
            GetCardTotal(_dealerCards);


        // 딜러 Bust
        if (_dealerTotal > 21)
        {
            WaitForResult(
                true,
                $"Dealer Bust! ({_dealerTotal})"
            );
        }

        // 같은 점수
        else if (_playerTotal == _dealerTotal)
        {
            WaitForDrawResult(
                $"Player {_playerTotal} / Dealer {_dealerTotal}"
            );
        }

        // 플레이어가 더 높음
        else if (_playerTotal > _dealerTotal)
        {
            WaitForResult(
                true,
                $"Player {_playerTotal} / Dealer {_dealerTotal}"
            );
        }

        // 딜러가 더 높음
        else
        {
            WaitForResult(
                false,
                $"Player {_playerTotal} / Dealer {_dealerTotal}"
            );
        }
    }

    private void WaitForDrawResult(string message)
    {
        _playMessage = message;

        _isDraw = true;
        _resultWaiting = true;

        _resultTime = TotalTime + ResultWaitTime;
    }

    private void ResetGame()
    {
        _playMessage = "";
        _resultWaiting = false;
        _dealerPlaying = false;

        _playerTotal = 0;
        _dealerTotal = 0;

        _isWin = false;
        _isLose = false;
        _isDraw = false;

        _playerCards.Clear();
        _dealerCards.Clear();
        _deck.Clear();

        _dealerReveal = false;
    }

    // Helper
    private static bool IsInside(
        float pointX,
        float pointY,
        float x,
        float y,
        float width,
        float height)
    {
        const float padding = 10.0f;

        return pointX >= x - padding &&
               pointX <= x + width + padding &&
               pointY >= y - padding &&
               pointY <= y + height + padding;
    }

    // Dispose
    public override void Dispose()
    {
        base.Dispose();
        _bgTable?.Dispose();

        _winSoundOutput?.Stop();
        _winSoundOutput?.Dispose();
        _winSoundReader?.Dispose();

        _loseSoundOutput?.Stop();
        _loseSoundOutput?.Dispose();
        _loseSoundReader?.Dispose();

        _drawSoundOutput?.Stop();
        _drawSoundOutput?.Dispose();
        _drawSoundReader?.Dispose();

        _bgStart?.Dispose();
        _bgStartClicked?.Dispose();
        _bgQuit?.Dispose();
        _bgQuitClicked?.Dispose();

        _bgHit?.Dispose();
        _bgHitClicked?.Dispose();
        _bgStand?.Dispose();
        _bgStandClicked?.Dispose();
        _bgCardBack?.Dispose();

        _bgWin?.Dispose();
        _bgLose?.Dispose();
        _bgDraw?.Dispose();
        _bgRestart?.Dispose();
        _bgRestartClicked?.Dispose();

        _spade2?.Dispose();
        _spade3?.Dispose();
        _spade4?.Dispose();
        _spade5?.Dispose();
        _spade6?.Dispose();
        _spade7?.Dispose();
        _spade8?.Dispose();
        _spade9?.Dispose();
        _spade10?.Dispose();

        _heart2?.Dispose();
        _heart3?.Dispose();
        _heart4?.Dispose();
        _heart5?.Dispose();
        _heart6?.Dispose();
        _heart7?.Dispose();
        _heart8?.Dispose();
        _heart9?.Dispose();
        _heart10?.Dispose();

        _clover2?.Dispose();
        _clover3?.Dispose();
        _clover4?.Dispose();
        _clover5?.Dispose();
        _clover6?.Dispose();
        _clover7?.Dispose();
        _clover8?.Dispose();
        _clover9?.Dispose();
        _clover10?.Dispose();

        _diamond2?.Dispose();
        _diamond3?.Dispose();
        _diamond4?.Dispose();
        _diamond5?.Dispose();
        _diamond6?.Dispose();
        _diamond7?.Dispose();
        _diamond8?.Dispose();
        _diamond9?.Dispose();
        _diamond10?.Dispose();
    }

}