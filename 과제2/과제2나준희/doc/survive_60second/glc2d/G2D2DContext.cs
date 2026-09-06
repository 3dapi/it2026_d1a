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
			  new RenderTargetProperties()
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
