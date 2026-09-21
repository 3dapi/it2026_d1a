using System;
using System.Windows.Forms;
using Vortice.Mathematics;

class SceneMain : IDisposable
{
    // 시작 화면 배경
    private G2Texture? bgStart;

    // START 글자
    private G2Font? startFont;

    // START 버튼 클릭 여부
    private bool startClicked = false;


    // ---------------------------------------------------------
    // 초기화
    // ---------------------------------------------------------
    public void Initialize()
    {
        // 시작 화면 이미지 불러오기
        bgStart = new G2Texture(
          "resource/tex_bg/bg_start_1280.png"
      );

        // START 글자 폰트
        startFont = new G2Font(
            "Arial",
            40
        );
    }


    // ---------------------------------------------------------
    // 입력 처리
    // ---------------------------------------------------------
    public void Update()
    {
        var input = G2AppBase.Instance?.Input;

        if (input == null)
        {
            return;
        }


        var mouse = input.MousePosition;


        // 마우스 왼쪽 클릭
        if (input.IsButtonDown(MouseButtons.Left))
        {
            // START 영역을 클릭했는지 확인
            if (IsStartButton(mouse.X, mouse.Y))
            {
                startClicked = true;
            }
        }


        // ESC 키로 종료
        if (input.IsKeyDown(Keys.Escape))
        {
            G2AppBase.Instance?.Close();
        }
    }


    // ---------------------------------------------------------
    // 화면 출력
    // ---------------------------------------------------------
    public void Render()
    {
        bgStart?.Draw();

        // START 출력 코드...
    }


    // ---------------------------------------------------------
    // START 버튼 클릭 범위
    // ---------------------------------------------------------
    private bool IsStartButton(float x, float y)
    {
        return
            x >= 500 &&
            x <= 780 &&
            y >= 600 &&
            y <= 700;
    }
        

    // ---------------------------------------------------------
    // 종료 시 리소스 정리
    // ---------------------------------------------------------
    public void Dispose()
    {
        bgStart?.Dispose();
        bgStart = null;

        startFont?.Dispose();
        startFont = null;
    }
}