// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Drawing.Imaging;
using Vortice.Mathematics;

static class G2TextureLoader
{
    public static ID2D1Bitmap? LoadBitmap(string filePath)
    {
        var renderTarget = G2AppBase.Instance?.RenderTarget
            ?? throw new InvalidOperationException(
                "ID2D1Bitmap::G2AppBase instance is not initialized.");

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException(
                "파일 경로가 비어 있습니다.",
                nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            return null;
        }

        using Bitmap bitmap = new Bitmap(filePath);

        Rectangle rect = new Rectangle(
            0,
            0,
            bitmap.Width,
            bitmap.Height);

        BitmapData bitmapData = bitmap.LockBits(
            rect,
            ImageLockMode.ReadOnly,
            PixelFormat.Format32bppPArgb);

        try
        {
            var size = new SizeI(
                bitmap.Width,
                bitmap.Height);

            var bitmapProperties = new BitmapProperties(
                new Vortice.DCommon.PixelFormat(
                    Vortice.DXGI.Format.B8G8R8A8_UNorm,
                    Vortice.DCommon.AlphaMode.Premultiplied),
                96.0f,
                96.0f);

            return renderTarget.CreateBitmap(
                size,
                bitmapData.Scan0,
                (uint)bitmapData.Stride,
                bitmapProperties);

        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }
    }

    public static void Release()
    {
        // WIC를 사용하지 않으므로 해제할 리소스가 없습니다.
    }
}

