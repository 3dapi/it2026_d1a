using System;
using System.Windows.Forms;
using Vortice.Direct2D1;
using Vortice.Mathematics;

class SceneMain : IDisposable
{
    // =====================================================
    // 이미지
    // =====================================================

    // 시작 화면 배경
    private G2Texture? bgStart;

    // 게임 화면 배경
    private G2Texture? bgGame;

    // 게임 오버 화면 배경
    private G2Texture? bgOver;


    // START 버튼
    private G2Texture? startButton;

    // START 버튼에 마우스를 올렸을 때
    private G2Texture? startButtonHover;


    // 글자
    private G2Font? uiFont;


    // 행성 이미지 1 ~ 9
    private G2Texture? planet1;
    private G2Texture? planet2;
    private G2Texture? planet3;
    private G2Texture? planet4;
    private G2Texture? planet5;
    private G2Texture? planet6;
    private G2Texture? planet7;
    private G2Texture? planet8;
    private G2Texture? planet9;


    // =====================================================
    // 게임 상태
    // =====================================================

    // START 버튼을 눌렀는지
    private bool startClicked = false;

    // START 버튼 위에 마우스가 있는지
    private bool startHover = false;

    // 게임이 끝났는지
    private bool gameOver = false;

    // 점수
    private int score = 0;


    // =====================================================
    // 시간
    // =====================================================

    // 남은 시간
    // 120초 = 2분
    private int timeLeft = 120;

    // 게임을 시작한 시간
    private DateTime gameStartTime;


    // =====================================================
    // 드래그
    // =====================================================

    // 현재 드래그 중인지
    private bool isDragging = false;


    // 드래그 시작 위치
    private float dragStartX = 0;
    private float dragStartY = 0;


    // 현재 드래그 위치
    private float dragEndX = 0;
    private float dragEndY = 0;


    // 선택한 행성 숫자의 합
    private int selectedSum = 0;


    // 기본 드래그 박스
    private ID2D1SolidColorBrush? dragBrush;

    // 숫자 합이 10일 때 빨간 박스
    private ID2D1SolidColorBrush? successBrush;

    // 선택된 행성의 흰색 사각형
    private ID2D1SolidColorBrush? selectedBrush;


    // =====================================================
    // START 버튼 위치
    // =====================================================

    private int startX = 480;
    private int startY = 520;

    private int startW = 320;
    private int startH = 120;


    // =====================================================
    // RETRY 위치
    // =====================================================

    // 게임 오버 화면에서 오른쪽 아래쪽에 배치
    private int retryX = 700;
    private int retryY = 500;

    private int retryW = 200;
    private int retryH = 60;


    // =====================================================
    // 행성 설정
    // =====================================================

    // 행성 크기
    private int planetW = 65;
    private int planetH = 43;


    // 행성 사이 간격
    private int planetGapX = 6;
    private int planetGapY = 16;


    // 첫 번째 행성이 시작되는 위치
    private int planetStartX = 35;
    private int planetStartY = 105;


    // 세로 10칸 × 가로 17칸
    // 총 170개
    private int[,] planetNumber = new int[10, 17];


    // 랜덤 숫자를 만들 때 사용
    private Random random = new Random();


    // =====================================================
    // 처음 실행할 때
    // =====================================================

    public void Initialize()
    {
        // -------------------------------------------------
        // 배경 이미지
        // -------------------------------------------------

        bgStart = new G2Texture(
            "resource/tex_bg/bg_start_1280.png"
        );

        bgGame = new G2Texture(
            "resource/tex_bg/bg_in.png"
        );

        bgOver = new G2Texture(
            "resource/tex_bg/over_bg.png"
        );


        // -------------------------------------------------
        // START 버튼
        // -------------------------------------------------

        startButton = new G2Texture(
            "resource/tex_ui/start.png"
        );

        startButtonHover = new G2Texture(
            "resource/tex_ui/start_hover.png"
        );


        // -------------------------------------------------
        // 글자
        // -------------------------------------------------

        uiFont = new G2Font(
            "Arial",
            30
        );


        // -------------------------------------------------
        // 행성 이미지
        // -------------------------------------------------

        planet1 = new G2Texture(
            "resource/tex_planet/planet1.png"
        );

        planet2 = new G2Texture(
            "resource/tex_planet/planet2.png"
        );

        planet3 = new G2Texture(
            "resource/tex_planet/planet3.png"
        );

        planet4 = new G2Texture(
            "resource/tex_planet/planet4.png"
        );

        planet5 = new G2Texture(
            "resource/tex_planet/planet5.png"
        );

        planet6 = new G2Texture(
            "resource/tex_planet/planet6.png"
        );

        planet7 = new G2Texture(
            "resource/tex_planet/planet7.png"
        );

        planet8 = new G2Texture(
            "resource/tex_planet/planet8.png"
        );

        planet9 = new G2Texture(
            "resource/tex_planet/planet9.png"
        );


        // -------------------------------------------------
        // 드래그 박스 색
        // -------------------------------------------------

        // 평소 드래그 박스
        // 하늘색
        dragBrush =
            G2AppBase.Instance?.RenderTarget.CreateSolidColorBrush(
                new Color4(
                    0.2f,
                    0.8f,
                    1.0f,
                    1.0f
                )
            );


        // 숫자의 합이 10일 때
        // 빨간색
        successBrush =
            G2AppBase.Instance?.RenderTarget.CreateSolidColorBrush(
                new Color4(
                    1.0f,
                    0.2f,
                    0.2f,
                    1.0f
                )
            );


        // 선택된 행성 테두리
        // 흰색
        selectedBrush =
            G2AppBase.Instance?.RenderTarget.CreateSolidColorBrush(
                new Color4(
                    1.0f,
                    1.0f,
                    1.0f,
                    1.0f
                )
            );
    }


    // =====================================================
    // 입력 처리
    // =====================================================

    public void Update()
    {
        // 입력 정보를 가져옴
        var input = G2AppBase.Instance?.Input;


        // 입력 정보가 없으면 종료
        if (input == null)
        {
            return;
        }


        // 현재 마우스 위치
        var mouse = input.MousePosition;


        // =================================================
        // 시작 화면
        // =================================================

        if (startClicked == false)
        {
            // 마우스가 START 버튼 위에 있는지 확인
            startHover =
                IsStartButton(
                    mouse.X,
                    mouse.Y
                );


            // START 버튼을 클릭했을 때
            if (startHover == true &&
                input.IsButtonDown(MouseButtons.Left))
            {
                startClicked = true;


                // 게임 시작
                StartGame();
            }
        }


        // =================================================
        // 게임 오버 화면
        // =================================================

        else if (gameOver == true)
        {
            // RETRY 부분을 클릭했을 때
            if (input.IsButtonDown(MouseButtons.Left) &&
                IsRetryButton(mouse.X, mouse.Y))
            {
                // 게임 다시 시작
                StartGame();
            }
        }


        // =================================================
        // 실제 게임 화면
        // =================================================

        else
        {
            // -------------------------------------------------
            // 시간 계산
            // -------------------------------------------------

            // 게임을 시작하고 몇 초가 지났는지 계산
            int passedTime =
                (int)(DateTime.Now - gameStartTime).TotalSeconds;


            // 120초에서 지난 시간을 뺌
            timeLeft =
                120 - passedTime;


            // -------------------------------------------------
            // 시간이 끝났을 때
            // -------------------------------------------------

            if (timeLeft <= 0)
            {
                // 시간이 음수가 되지 않도록 0으로 고정
                timeLeft = 0;


                // 게임 오버
                gameOver = true;


                // 드래그 중이었다면 종료
                isDragging = false;


                // 선택 합도 초기화
                selectedSum = 0;
            }


            // -------------------------------------------------
            // 시간이 남아 있을 때
            // -------------------------------------------------

            else
            {
                // ---------------------------------------------
                // 마우스를 처음 눌렀을 때
                // ---------------------------------------------

                if (input.IsButtonDown(MouseButtons.Left))
                {
                    // 드래그 시작
                    isDragging = true;


                    // 시작 위치 저장
                    dragStartX = mouse.X;
                    dragStartY = mouse.Y;


                    // 처음에는 끝 위치도 같은 위치
                    dragEndX = mouse.X;
                    dragEndY = mouse.Y;


                    // 합계 초기화
                    selectedSum = 0;
                }


                // ---------------------------------------------
                // 마우스를 계속 누르고 있을 때
                // ---------------------------------------------

                if (isDragging == true &&
                    input.IsButtonPress(MouseButtons.Left))
                {
                    // 현재 마우스 위치
                    dragEndX = mouse.X;
                    dragEndY = mouse.Y;


                    // 선택된 행성의 숫자 합 계산
                    selectedSum =
                        CalculateSelectedSum();
                }


                // ---------------------------------------------
                // 마우스를 놓았을 때
                // ---------------------------------------------

                if (isDragging == true &&
                    input.IsButtonUp(MouseButtons.Left))
                {
                    // 마지막 마우스 위치
                    dragEndX = mouse.X;
                    dragEndY = mouse.Y;


                    // 마지막 숫자 합 확인
                    selectedSum =
                        CalculateSelectedSum();


                    // 선택한 숫자의 합이 10이면
                    if (selectedSum == 10)
                    {
                        // 선택한 행성 삭제
                        RemoveSelectedPlanets();
                    }


                    // 드래그 종료
                    isDragging = false;


                    // 선택 합 초기화
                    selectedSum = 0;
                }
            }
        }


        // =================================================
        // ESC를 누르면 게임 종료
        // =================================================

        if (input.IsKeyDown(Keys.Escape))
        {
            G2AppBase.Instance?.Close();
        }
    }


    // =====================================================
    // 화면에 그림 그리기
    // =====================================================

    public void Render()
    {
        // =================================================
        // 시작 화면
        // =================================================

        if (startClicked == false)
        {
            // 시작 화면 배경
            bgStart?.Draw();


            // -------------------------------------------------
            // 평소 START 버튼
            // -------------------------------------------------

            if (startHover == false)
            {
                startButton?.Draw(

                    // 화면에서 보일 위치와 크기
                    new Rect(
                        startX,
                        startY,
                        startW,
                        startH
                    ),

                    // start.png 원본 크기
                    new Rect(
                        0,
                        0,
                        455,
                        170
                    )
                );
            }


            // -------------------------------------------------
            // 마우스를 올렸을 때 START 버튼
            // -------------------------------------------------

            else
            {
                startButtonHover?.Draw(

                    new Rect(
                        startX,
                        startY,
                        startW,
                        startH
                    ),

                    // start_hover.png 원본 크기
                    new Rect(
                        0,
                        0,
                        610,
                        240
                    )
                );
            }
        }


        // =================================================
        // 게임 오버 화면
        // =================================================

        else if (gameOver == true)
        {
            // -------------------------------------------------
            // 게임 오버 배경
            // -------------------------------------------------

            bgOver?.Draw(

                // 화면 크기
                new Rect(
                    0,
                    0,
                    1280,
                    720
                ),

                // over_bg.png 원본 크기
                new Rect(
                    0,
                    0,
                    1672,
                    941
                )
            );


            // -------------------------------------------------
            // SCORE
            // -------------------------------------------------

            // 왼쪽
            uiFont?.DrawText(

                "SCORE : " + score,

                new Rect(
                    380,
                    500,
                    250,
                    60
                ),

                new Color4(
                    1.0f,
                    1.0f,
                    1.0f,
                    1.0f
                )
            );


            // -------------------------------------------------
            // RETRY
            // -------------------------------------------------

            // SCORE와 같은 높이의 오른쪽
            uiFont?.DrawText(

                "RETRY",

                new Rect(
                    retryX,
                    retryY,
                    retryW,
                    retryH
                ),

                new Color4(
                    1.0f,
                    1.0f,
                    1.0f,
                    1.0f
                )
            );


            // TIME OVER 글자는 따로 출력하지 않음
            // over_bg.png 안에 이미 TIME OVER가 있기 때문
        }


        // =================================================
        // 게임 플레이 화면
        // =================================================

        else
        {
            // -------------------------------------------------
            // 게임 배경
            // -------------------------------------------------

            bgGame?.Draw(

                new Rect(
                    0,
                    0,
                    1280,
                    720
                ),

                new Rect(
                    0,
                    0,
                    1672,
                    941
                )
            );


            // -------------------------------------------------
            // SCORE
            // -------------------------------------------------

            uiFont?.DrawText(

                "SCORE : " + score,

                new Rect(
                    1010,
                    20,
                    250,
                    50
                ),

                new Color4(
                    1.0f,
                    1.0f,
                    1.0f,
                    1.0f
                )
            );


            // -------------------------------------------------
            // TIME
            // -------------------------------------------------

            // 남은 시간을 분으로 계산
            int minute =
                timeLeft / 60;


            // 남은 초 계산
            int second =
                timeLeft % 60;


            // 2:00 형식으로 출력
            uiFont?.DrawText(

                "TIME : " +
                minute +
                ":" +
                second.ToString("00"),

                new Rect(
                    1010,
                    60,
                    250,
                    50
                ),

                new Color4(
                    1.0f,
                    1.0f,
                    1.0f,
                    1.0f
                )
            );


            // -------------------------------------------------
            // 행성 170개 출력
            // -------------------------------------------------

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 17; x++)
                {
                    // 현재 행성의 X 위치
                    int drawX =
                        planetStartX +
                        x * (planetW + planetGapX);


                    // 현재 행성의 Y 위치
                    int drawY =
                        planetStartY +
                        y * (planetH + planetGapY);


                    // 현재 칸에 들어있는 숫자
                    int number =
                        planetNumber[y, x];


                    // 0이 아니면 행성을 그림
                    // 0은 삭제된 빈칸
                    if (number != 0)
                    {
                        DrawPlanet(
                            number,
                            drawX,
                            drawY
                        );
                    }


                    // -------------------------------------------------
                    // 현재 선택된 행성 표시
                    // -------------------------------------------------

                    if (number != 0 &&
                        IsPlanetSelected(drawX, drawY) == true &&
                        selectedBrush != null)
                    {
                        G2AppBase.Instance?.RenderTarget.DrawRectangle(

                            new Rect(
                                drawX,
                                drawY,
                                planetW,
                                planetH
                            ),

                            selectedBrush,

                            2.0f
                        );
                    }
                }
            }


            // -------------------------------------------------
            // 드래그 박스 출력
            // -------------------------------------------------

            if (isDragging == true)
            {
                // 드래그 박스의 왼쪽 위치
                float boxX =
                    Math.Min(
                        dragStartX,
                        dragEndX
                    );


                // 드래그 박스의 위쪽 위치
                float boxY =
                    Math.Min(
                        dragStartY,
                        dragEndY
                    );


                // 박스 가로 크기
                float boxW =
                    Math.Abs(
                        dragEndX - dragStartX
                    );


                // 박스 세로 크기
                float boxH =
                    Math.Abs(
                        dragEndY - dragStartY
                    );


                // ---------------------------------------------
                // 합이 10이면 빨간색
                // ---------------------------------------------

                if (selectedSum == 10 &&
                    successBrush != null)
                {
                    G2AppBase.Instance?.RenderTarget.DrawRectangle(

                        new Rect(
                            boxX,
                            boxY,
                            boxW,
                            boxH
                        ),

                        successBrush,

                        3.0f
                    );
                }


                // ---------------------------------------------
                // 합이 10이 아니면 하늘색
                // ---------------------------------------------

                else if (dragBrush != null)
                {
                    G2AppBase.Instance?.RenderTarget.DrawRectangle(

                        new Rect(
                            boxX,
                            boxY,
                            boxW,
                            boxH
                        ),

                        dragBrush,

                        3.0f
                    );
                }
            }
        }
    }


    // =====================================================
    // 게임 시작
    // =====================================================

    private void StartGame()
    {
        // 점수 초기화
        score = 0;


        // 시간 2분으로 초기화
        timeLeft = 120;


        // 게임 오버 상태 해제
        gameOver = false;


        // 드래그 초기화
        isDragging = false;


        // 선택 합 초기화
        selectedSum = 0;


        // 새로운 행성 배치 생성
        CreatePlanets();


        // 현재 시간을 게임 시작 시간으로 저장
        gameStartTime =
            DateTime.Now;
    }


    // =====================================================
    // 행성 170개 생성
    // =====================================================

    private void CreatePlanets()
    {
        // 세로 10줄
        for (int y = 0; y < 10; y++)
        {
            // 가로 17칸
            for (int x = 0; x < 17; x++)
            {
                // 1부터 9까지 랜덤
                planetNumber[y, x] =
                    random.Next(1, 10);
            }
        }
    }


    // =====================================================
    // 행성이 선택됐는지 확인
    // =====================================================

    private bool IsPlanetSelected(
        int x,
        int y
    )
    {
        // 드래그 중이 아니면 선택되지 않음
        if (isDragging == false)
        {
            return false;
        }


        // 드래그 영역의 왼쪽
        float left =
            Math.Min(
                dragStartX,
                dragEndX
            );


        // 드래그 영역의 오른쪽
        float right =
            Math.Max(
                dragStartX,
                dragEndX
            );


        // 드래그 영역의 위
        float top =
            Math.Min(
                dragStartY,
                dragEndY
            );


        // 드래그 영역의 아래
        float bottom =
            Math.Max(
                dragStartY,
                dragEndY
            );


        // 행성 가운데 X 위치
        float centerX =
            x +
            planetW / 2.0f;


        // 행성 가운데 Y 위치
        float centerY =
            y +
            planetH / 2.0f;


        // 행성의 가운데가 드래그 박스 안에 있는지 확인
        if (centerX >= left &&
            centerX <= right &&
            centerY >= top &&
            centerY <= bottom)
        {
            return true;
        }


        return false;
    }


    // =====================================================
    // 선택된 숫자의 합 계산
    // =====================================================

    private int CalculateSelectedSum()
    {
        // 선택된 숫자의 합
        int sum = 0;


        // 드래그 범위
        float left =
            Math.Min(
                dragStartX,
                dragEndX
            );


        float right =
            Math.Max(
                dragStartX,
                dragEndX
            );


        float top =
            Math.Min(
                dragStartY,
                dragEndY
            );


        float bottom =
            Math.Max(
                dragStartY,
                dragEndY
            );


        // 모든 행성 검사
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 17; x++)
            {
                // 0이면 이미 삭제된 행성
                if (planetNumber[y, x] == 0)
                {
                    continue;
                }


                // 행성 위치 계산
                int drawX =
                    planetStartX +
                    x * (planetW + planetGapX);


                int drawY =
                    planetStartY +
                    y * (planetH + planetGapY);


                // 행성 가운데 위치
                float centerX =
                    drawX +
                    planetW / 2.0f;


                float centerY =
                    drawY +
                    planetH / 2.0f;


                // 드래그 영역 안에 들어왔다면
                if (centerX >= left &&
                    centerX <= right &&
                    centerY >= top &&
                    centerY <= bottom)
                {
                    // 숫자를 합에 더함
                    sum =
                        sum +
                        planetNumber[y, x];
                }
            }
        }


        // 계산한 합을 돌려줌
        return sum;
    }


    // =====================================================
    // 선택된 행성 삭제
    // =====================================================

    private void RemoveSelectedPlanets()
    {
        // 드래그 범위
        float left =
            Math.Min(
                dragStartX,
                dragEndX
            );


        float right =
            Math.Max(
                dragStartX,
                dragEndX
            );


        float top =
            Math.Min(
                dragStartY,
                dragEndY
            );


        float bottom =
            Math.Max(
                dragStartY,
                dragEndY
            );


        // 모든 행성을 확인
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 17; x++)
            {
                // 이미 빈칸이면 넘어감
                if (planetNumber[y, x] == 0)
                {
                    continue;
                }


                // 행성 위치
                int drawX =
                    planetStartX +
                    x * (planetW + planetGapX);


                int drawY =
                    planetStartY +
                    y * (planetH + planetGapY);


                // 행성 가운데 위치
                float centerX =
                    drawX +
                    planetW / 2.0f;


                float centerY =
                    drawY +
                    planetH / 2.0f;


                // 선택 범위 안에 있는 행성이라면
                if (centerX >= left &&
                    centerX <= right &&
                    centerY >= top &&
                    centerY <= bottom)
                {
                    // 행성을 0으로 변경
                    // 0 = 빈칸
                    planetNumber[y, x] = 0;


                    // 행성 하나를 없앨 때마다 1점
                    score =
                        score + 1;
                }
            }
        }
    }


    // =====================================================
    // 숫자에 맞는 행성 이미지 출력
    // =====================================================

    private void DrawPlanet(
        int number,
        int x,
        int y
    )
    {
        // 원본 행성 이미지 크기
        // 240 × 170


        if (number == 1)
        {
            planet1?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 2)
        {
            planet2?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 3)
        {
            planet3?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 4)
        {
            planet4?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 5)
        {
            planet5?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 6)
        {
            planet6?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 7)
        {
            planet7?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 8)
        {
            planet8?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }


        else if (number == 9)
        {
            planet9?.Draw(
                new Rect(x, y, planetW, planetH),
                new Rect(0, 0, 240, 170)
            );
        }
    }


    // =====================================================
    // START 버튼 안에 마우스가 있는지 확인
    // =====================================================

    private bool IsStartButton(
        float x,
        float y
    )
    {
        if (x >= startX &&
            x <= startX + startW &&
            y >= startY &&
            y <= startY + startH)
        {
            return true;
        }


        return false;
    }


    // =====================================================
    // RETRY 안에 마우스가 있는지 확인
    // =====================================================

    private bool IsRetryButton(
        float x,
        float y
    )
    {
        if (x >= retryX &&
            x <= retryX + retryW &&
            y >= retryY &&
            y <= retryY + retryH)
        {
            return true;
        }


        return false;
    }


    // =====================================================
    // 게임이 종료될 때 이미지 정리
    // =====================================================

    public void Dispose()
    {
        // 배경
        bgStart?.Dispose();
        bgStart = null;

        bgGame?.Dispose();
        bgGame = null;

        bgOver?.Dispose();
        bgOver = null;


        // START 버튼
        startButton?.Dispose();
        startButton = null;

        startButtonHover?.Dispose();
        startButtonHover = null;


        // 글자
        uiFont?.Dispose();
        uiFont = null;


        // 행성
        planet1?.Dispose();
        planet1 = null;

        planet2?.Dispose();
        planet2 = null;

        planet3?.Dispose();
        planet3 = null;

        planet4?.Dispose();
        planet4 = null;

        planet5?.Dispose();
        planet5 = null;

        planet6?.Dispose();
        planet6 = null;

        planet7?.Dispose();
        planet7 = null;

        planet8?.Dispose();
        planet8 = null;

        planet9?.Dispose();
        planet9 = null;


        // 드래그 박스
        dragBrush?.Dispose();
        dragBrush = null;

        successBrush?.Dispose();
        successBrush = null;

        selectedBrush?.Dispose();
        selectedBrush = null;
    }
}