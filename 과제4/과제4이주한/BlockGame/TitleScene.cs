using System.Windows.Forms;
using Vortice.Mathematics;

class TitleScene : IDisposable
{
	private readonly GameMain game;
	private int menuIndex;

	private GameTexture? title;
	private GameTexture? button;
	private GameTexture? selected;
	private GameTexture? grayArrow;
	private GameTexture? redArrow;
	private GameTexture? blueArrow;
	private G2Font? menuFont;
	private G2AudioSound? moveSound;
	private G2AudioSound? selectSound;

	public TitleScene(GameMain game)
	{
		this.game = game;
	}

	public void Initialize()
	{
		title = new GameTexture("resource/image/ui/menu/title.png");
		button = new GameTexture("resource/image/ui/menu/button.png");
		selected = new GameTexture("resource/image/ui/menu/selected.png");
		grayArrow = new GameTexture("resource/image/note/gray/right.png");
		redArrow = new GameTexture("resource/image/note/red/right.png");
		blueArrow = new GameTexture("resource/image/note/blue/right.png");
		menuFont = new G2Font("Bahnschrift", 32);
		moveSound = new G2AudioSound("resource/audio/effect/move.wav");
		selectSound = new G2AudioSound("resource/audio/effect/select.wav");
	}

	public void Update()
	{
		if (game.Input.IsKeyDown(Keys.Up) && menuIndex > 0)
		{
			menuIndex--;
			moveSound?.Play();
		}
		else if (game.Input.IsKeyDown(Keys.Down) && menuIndex < 1)
		{
			menuIndex++;
			moveSound?.Play();
		}

		if (!game.Input.IsKeyDown(Keys.Enter))
		{
			return;
		}

		selectSound?.Play();
		if (menuIndex == 0)
		{
			game.ChangeScene(SceneType.Gameplay);
		}
		else
		{
			game.Close();
		}
	}

	public void Draw()
	{
		title?.DrawCenter(640, 160);

		grayArrow?.DrawCenter(486, 326);
		redArrow?.DrawCenter(640, 326);
		blueArrow?.DrawCenter(793, 326);

		button?.DrawCenter(640, 445);
		button?.DrawCenter(640, 555);
		selected?.DrawCenter(640, menuIndex == 0 ? 445 : 555);

		menuFont?.DrawText("START GAME", new Rect(535, 425, 420, 60), new Color4(1, 1, 1, 1));
		menuFont?.DrawText("EXIT", new Rect(605, 535, 300, 60), new Color4(0.75f, 0.75f, 0.8f, 1));
	}

	public void Dispose()
	{
		selectSound?.Dispose();
		moveSound?.Dispose();
		menuFont?.Dispose();
		blueArrow?.Dispose();
		redArrow?.Dispose();
		grayArrow?.Dispose();
		selected?.Dispose();
		button?.Dispose();
		title?.Dispose();
	}
}
