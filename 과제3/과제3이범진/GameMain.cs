// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
<<<<<<< HEAD
    Vortice.Direct2D1.ID2D1Bitmap? bgImg;
    Vortice.Direct2D1.ID2D1Bitmap? titleImg;
    Vortice.Direct2D1.ID2D1Bitmap? startImg;

    float titleTimer = 0f;
    float startBtnScale = 1.0f;

    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
=======
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
>>>>>>> fd5e7d46f24505cfd4dba3f6bd8bc653f5619870
	public override string GameName => GameGlobal.GameName;

	protected override void Initialize()
	{
<<<<<<< HEAD
        bgImg = G2TextureLoader.LoadBitmap("resource/background1.png");
        titleImg = G2TextureLoader.LoadBitmap("resource/title.png");
        startImg = G2TextureLoader.LoadBitmap("resource/start.png");
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------
    }
=======
		//---------------------------------------
		// 게임 관련 객체를 생성합니다.
		//---------------------------------------
	}
>>>>>>> fd5e7d46f24505cfd4dba3f6bd8bc653f5619870

	protected override void Update()
	{
		double elapsed = TotalTime;

		this.ClearColor = new Color4(
			red: (float)(Math.Sin(elapsed) * 0.5 + 0.5),
			green: (float)(Math.Sin(elapsed + Math.PI / 2.0) * 0.5 + 0.5),
			blue: (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5),
			alpha: 1.0f);

<<<<<<< HEAD
        titleTimer += 0.05f;

        float startX = (800 / 2) - (startImg.PixelSize.Width / 2);
        float startY = 450;

        float mouseX = G2InputContext.Instance.MousePosition.X; 
        float mouseY = G2InputContext.Instance.MousePosition.Y;

        if (mouseX >= startX && mouseX <= startX + startImg.PixelSize.Width &&
            mouseY >= startY && mouseY <= startY + startImg.PixelSize.Height)
        {
            startBtnScale = 1.15f; // 마우스 올리면 커짐
        }
        else
        {
            startBtnScale = 1.0f;  // 벗어나면 원래대로
        }
        //---------------------------------------
        // 게임 관련 객체를 갱신합니다.
        //---------------------------------------
    }

	protected override void Render()
	{
        G2D2DContext.Instance.RenderTarget.DrawBitmap(bgImg, 0, 0);
        float titleX = (800 / 2) - (titleImg.PixelSize.Width / 2);
        float titleY = 100 + (float)Math.Sin(titleTimer) * 15f;
        new Vortice.Mathematics.(titleX, titleY, titleX + titleImg.PixelSize.Width, titleY + titleImg.PixelSize.Height);
        
        float drawStartX = ((800 / 2) - (startImg.PixelSize.Width / 2)) - (startImg.PixelSize.Width * (startBtnScale - 1.0f) / 2);
        float drawStartY = 450 - (startImg.PixelSize.Height * (startBtnScale - 1.0f) / 2);

        G2D2DContext.Instance.RenderTarget.Mathematics.RawRectF(titleX, titleY, titleX + titleImg.PixelSize.Width, titleY + titleImg.PixelSize.Height)
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------
    }
=======
		//---------------------------------------
		// 게임 관련 객체를 갱신합니다.
		//---------------------------------------
	}

	protected override void Render()
	{
		//---------------------------------------
		// 게임 관련 객체를 렌더링 합니다.
		//---------------------------------------
	}
>>>>>>> fd5e7d46f24505cfd4dba3f6bd8bc653f5619870

	public override void Dispose()
	{
		base.Dispose();
		//---------------------------------------
		// 게임 관련 객체를 해제합니다.
		//---------------------------------------
	}
}
