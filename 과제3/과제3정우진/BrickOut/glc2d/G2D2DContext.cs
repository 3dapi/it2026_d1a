// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

class G2D2DContext : IDisposable
{
	public static G2D2DContext? Instance { get; private set; }

	public ID2D1HwndRenderTarget RenderTarget { get; private set; }
	public ID2D1Factory1 Factory { get; }
	public IDWriteFactory DWriteFactory { get; }

	public G2D2DContext(IntPtr hwnd, int width, int height)
	{
		if (Instance != null)
		{
			throw new InvalidOperationException("G2D2DContext instance already exists.");
		}
		Factory = D2D1.D2D1CreateFactory<ID2D1Factory1>();
		DWriteFactory = DWrite.DWriteCreateFactory<IDWriteFactory>();
		RenderTarget = Factory.CreateHwndRenderTarget(
			  // ClientSize는 pixel 단위이며 화면 배율은 G2AppBase.Transform에서 적용합니다.
			  // Direct2D의 DPI 확대를 중복 적용하지 않도록 1 DIP = 1 pixel로 고정합니다.
			  new RenderTargetProperties { DpiX = 96.0f, DpiY = 96.0f }
			, new HwndRenderTargetProperties
			{
				Hwnd = hwnd,
				PixelSize = new SizeI(width, height)
			}
		);
		Instance = this;
	}

	public void Resize(int width, int height)
	{
		RenderTarget.Resize(new SizeI(width, height));
	}

	public void Dispose()
	{
		RenderTarget.Dispose();
		DWriteFactory.Dispose();
		Factory.Dispose();
		Instance = null;
	}
}
