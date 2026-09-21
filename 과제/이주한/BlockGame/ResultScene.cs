using System.Windows.Forms;
using Vortice.Mathematics;

class ResultScene : IDisposable
{
	private readonly GameMain game;
	private int menuIndex;

	private GameTexture? resultPanel;
	private GameTexture? grade;
	private GameTexture? button;
	private GameTexture? selected;
	private G2Font? titleFont;
	private G2Font? labelFont;
	private G2Font? valueFont;
	private G2Font? menuFont;
	private G2AudioSound? moveSound;
	private G2AudioSound? selectSound;

	public ResultScene(GameMain game)
	{
		this.game = game;
	}

	public void Initialize()
	{
		resultPanel = new GameTexture("resource/image/ui/menu/result.png");
		grade = new GameTexture("resource/image/ui/grade/s.png");
		button = new GameTexture("resource/image/ui/menu/button.png");
		selected = new GameTexture("resource/image/ui/menu/selected.png");
		titleFont = new G2Font("Bahnschrift", 44);
		labelFont = new G2Font("Bahnschrift", 22);
		valueFont = new G2Font("Bahnschrift", 36);
		menuFont = new G2Font("Bahnschrift", 28, textAlignment: Vortice.DirectWrite.TextAlignment.Center);
		moveSound = new G2AudioSound("resource/audio/effect/move.wav");
		selectSound = new G2AudioSound("resource/audio/effect/select.wav");
	}

	public void Update()
	{
		if (game.Input.IsKeyDown(Keys.Left) || game.Input.IsKeyDown(Keys.Right))
		{
			menuIndex = menuIndex == 0 ? 1 : 0;
			moveSound?.Play();
		}

		if (!game.Input.IsKeyDown(Keys.Enter))
		{
			return;
		}

		selectSound?.Play();
		game.ChangeScene(menuIndex == 0 ? SceneType.Gameplay : SceneType.Title);
	}

	public void Draw()
	{
		titleFont?.DrawText("RESULT", new Rect(555, 35, 300, 70), new Color4(1, 1, 1, 1));
		resultPanel?.DrawCenter(640, 345);
		grade?.DrawCenter(450, 330);

		labelFont?.DrawText("SCORE", new Rect(650, 205, 200, 45), new Color4(0.75f, 0.78f, 0.9f, 1));
		valueFont?.DrawText("0", new Rect(850, 230, 260, 60), new Color4(1, 1, 1, 1));
		labelFont?.DrawText("CLEARED", new Rect(650, 315, 200, 45), new Color4(0.75f, 0.78f, 0.9f, 1));
		valueFont?.DrawText("0 / 10", new Rect(850, 340, 260, 60), new Color4(1, 1, 1, 1));
		labelFont?.DrawText("MISTAKES", new Rect(650, 425, 200, 45), new Color4(0.75f, 0.78f, 0.9f, 1));
		valueFont?.DrawText("0", new Rect(850, 450, 260, 60), new Color4(1, 1, 1, 1));

		button?.DrawCenter(380, 635);
		button?.DrawCenter(900, 635);
		selected?.DrawCenter(menuIndex == 0 ? 380 : 900, 635);
		menuFont?.DrawText("RETRY", new Rect(170, 615, 420, 45), new Color4(1, 1, 1, 1));
		menuFont?.DrawText("MAIN MENU", new Rect(690, 615, 420, 45), new Color4(0.8f, 0.8f, 0.85f, 1));

		// 점수와 등급 계산은 게임 플레이가 만들어진 뒤 연결
	}

	public void Dispose()
	{
		selectSound?.Dispose();
		moveSound?.Dispose();
		menuFont?.Dispose();
		valueFont?.Dispose();
		labelFont?.Dispose();
		titleFont?.Dispose();
		selected?.Dispose();
		button?.Dispose();
		grade?.Dispose();
		resultPanel?.Dispose();
	}
}
