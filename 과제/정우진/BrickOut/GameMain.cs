// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Mathematics;

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	private G2Texture? _background;
	private G2Texture? _stone;
	private G2Texture? _hardStone;
	private G2Texture? _pickaxe;
	private G2Texture? _oxygen;

	protected override void Initialize()
	{
		_background = new G2Texture("resource/InplayBG.png");
		_stone = new G2Texture("resource/Stone_Tile.png");
		_hardStone = new G2Texture("resource/HardStone_Tile.png");
		_pickaxe = new G2Texture("resource/Pickaxe.png");
		_oxygen = new G2Texture("resource/oxygen.png");
	}

	protected override void Update()
	{

	}

	protected override void Render()
	{
		float width = ScreenSize.Width;
		float height = ScreenSize.Height;
		float imageScale = Math.Min(width / 800.0f, height / 600.0f);
		_background?.Draw(new Rect(0, 0, width, height), new Rect(0, 0, 536, 319));

		float margin = 12 * imageScale;
		float gap = 4 * imageScale;
		float rockSize = (width - margin * 2 - gap * 9) / 10;

		// 상단에 돌을 10열 2행으로 배치합니다. Tile Sheet는 4열 4행입니다.
		for (int row = 0; row < 2; row++)
		{
			for (int column = 0; column < 10; column++)
			{
				Rect destination = new(
					margin + column * (rockSize + gap),
					margin + row * (rockSize + gap),
					rockSize, rockSize);
				int tileIndex = (row * 10 + column) % 16;
				bool isHardStone = row == 0 && column % 3 == 1;
				int tileSize = isHardStone ? 128 : 256;
				Rect source = new(
					(tileIndex % 4) * tileSize,
					(tileIndex / 4) * tileSize,
					tileSize, tileSize);
				(isHardStone ? _hardStone : _stone)?.Draw(destination, source);
			}
		}

		// 곡괭이는 중앙 하단, 산소 이미지는 하단 UI에 출력
		float pickaxeSize = 100 * imageScale;
		float oxygenSize = 52 * imageScale;
		_pickaxe?.Draw(
			new Rect((width - pickaxeSize) / 2, height * 5 / 6 - pickaxeSize / 2, pickaxeSize, pickaxeSize),
			new Rect(0, 0, 640, 640));
		_oxygen?.Draw(
			new Rect(16 * imageScale, height - margin - oxygenSize, oxygenSize, oxygenSize),
			new Rect(0, 0, 360, 360));
	}

	public override void Dispose()
	{
		_oxygen?.Dispose();
		_pickaxe?.Dispose();
		_hardStone?.Dispose();
		_stone?.Dispose();
		_background?.Dispose();
		base.Dispose();
	}
}
