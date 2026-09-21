class GameTexture : IDisposable
{
	private readonly G2Texture texture;
	private readonly float width;
	private readonly float height;

	public GameTexture(string filePath)
	{
		texture = new G2Texture(filePath);

		string fullPath = G2Util.FindFilePath(filePath);
		using System.Drawing.Image image = System.Drawing.Image.FromFile(fullPath);
		width = image.Width;
		height = image.Height;
	}

	public void DrawCenter(float centerX, float centerY)
	{
		float x = centerX - width / 2.0f;
		float y = centerY - height / 2.0f;
		texture.Draw(x, y);
	}

	public void Dispose()
	{
		texture.Dispose();
	}
}
