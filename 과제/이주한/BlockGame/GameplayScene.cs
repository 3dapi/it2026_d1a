using System.Windows.Forms;
using Vortice.Mathematics;

class GameplayScene : IDisposable
{
	private enum Direction
	{
		Left,
		Down,
		Up,
		Right
	}

	private readonly GameMain game;
	private Direction? lastDirection;
	private double lastInputTime;

	private GameTexture? frame;
	private GameTexture? currentFrame;
	private GameTexture? perfect;
	private readonly GameTexture?[] grayArrows = new GameTexture?[4];
	private readonly GameTexture?[] blueArrows = new GameTexture?[4];
	private readonly G2AudioSound?[] keySounds = new G2AudioSound?[4];
	private G2AudioSound? bgm;
	private G2Font? titleFont;
	private G2Font? valueFont;
	private G2Font? guideFont;

	public GameplayScene(GameMain game)
	{
		this.game = game;
	}

	public void Initialize()
	{
		frame = new GameTexture("resource/image/frame/frame.png");
		currentFrame = new GameTexture("resource/image/ui/command/current.png");
		perfect = new GameTexture("resource/image/ui/judgment/perfect.png");

		grayArrows[(int)Direction.Left] = new GameTexture("resource/image/note/gray/left.png");
		grayArrows[(int)Direction.Down] = new GameTexture("resource/image/note/gray/down.png");
		grayArrows[(int)Direction.Up] = new GameTexture("resource/image/note/gray/up.png");
		grayArrows[(int)Direction.Right] = new GameTexture("resource/image/note/gray/right.png");

		blueArrows[(int)Direction.Left] = new GameTexture("resource/image/note/blue/left.png");
		blueArrows[(int)Direction.Down] = new GameTexture("resource/image/note/blue/down.png");
		blueArrows[(int)Direction.Up] = new GameTexture("resource/image/note/blue/up.png");
		blueArrows[(int)Direction.Right] = new GameTexture("resource/image/note/blue/right.png");

		keySounds[(int)Direction.Left] = new G2AudioSound("resource/audio/effect/left.wav");
		keySounds[(int)Direction.Down] = new G2AudioSound("resource/audio/effect/down.wav");
		keySounds[(int)Direction.Up] = new G2AudioSound("resource/audio/effect/up.wav");
		keySounds[(int)Direction.Right] = new G2AudioSound("resource/audio/effect/right.wav");
		bgm = new G2AudioSound("resource/audio/bgm/game.wav");

		titleFont = new G2Font("Bahnschrift", 24);
		valueFont = new G2Font("Bahnschrift", 44);
		guideFont = new G2Font("Bahnschrift", 18);
	}

	public void Start()
	{
		lastDirection = null;
		bgm?.Play(true);
	}

	public void Stop()
	{
		bgm?.Stop();
	}

	public void Update()
	{
		if (game.Input.IsKeyDown(Keys.Left))
		{
			PlayInputSound(Direction.Left);
		}
		if (game.Input.IsKeyDown(Keys.Down))
		{
			PlayInputSound(Direction.Down);
		}
		if (game.Input.IsKeyDown(Keys.Up))
		{
			PlayInputSound(Direction.Up);
		}
		if (game.Input.IsKeyDown(Keys.Right))
		{
			PlayInputSound(Direction.Right);
		}

		if (lastDirection != null && game.TotalTime - lastInputTime > 0.18)
		{
			lastDirection = null;
		}

		// 테스트용으로 방향키 입력 시 사운드만 출력한다.
		// 다음 단계에서 랜덤 커맨드, 점수, 스테이지와 제한 시간을 구현한다.

		if (game.Input.IsKeyDown(Keys.Enter))
		{
			game.ChangeScene(SceneType.Result);
		}
		else if (game.Input.IsKeyDown(Keys.Escape))
		{
			game.ChangeScene(SceneType.Title);
		}
	}

	private void PlayInputSound(Direction direction)
	{
		lastDirection = direction;
		lastInputTime = game.TotalTime;
		keySounds[(int)direction]?.Play();
	}

	public void Draw()
	{
		DrawFrame();

		titleFont?.DrawText("SCORE", new Rect(35, 27, 190, 45), new Color4(1, 1, 1, 1));
		valueFont?.DrawText("0", new Rect(35, 60, 190, 65), new Color4(1, 1, 1, 1));
		titleFont?.DrawText("STAGE 01", new Rect(565, 69, 220, 50), new Color4(1, 1, 1, 1));
		titleFont?.DrawText("TIME", new Rect(1150, 27, 120, 45), new Color4(1, 1, 1, 1));
		valueFont?.DrawText("60.0", new Rect(1150, 60, 130, 65), new Color4(1, 1, 1, 1));

		Direction[] directions = { Direction.Left, Direction.Down, Direction.Up, Direction.Right };
		for (int i = 0; i < directions.Length; i++)
		{
			Direction direction = directions[i];
			GameTexture? texture = lastDirection == direction
				? blueArrows[(int)direction]
				: grayArrows[(int)direction];
			float centerX = 400 + i * 160;
			texture?.DrawCenter(centerX, 350);

			if (lastDirection == direction)
			{
				currentFrame?.DrawCenter(centerX, 350);
			}
		}

		if (lastDirection != null)
		{
			perfect?.DrawCenter(640, 482);
		}

		guideFont?.DrawText("ENTER : RESULT    ESC : TITLE", new Rect(500, 620, 420, 45), new Color4(0.7f, 0.75f, 0.85f, 1));
	}

	private void DrawFrame()
	{
		frame?.DrawCenter(640, 360);
	}

	public void Dispose()
	{
		bgm?.Dispose();
		foreach (G2AudioSound? sound in keySounds)
		{
			sound?.Dispose();
		}

		guideFont?.Dispose();
		valueFont?.Dispose();
		titleFont?.Dispose();
		perfect?.Dispose();
		currentFrame?.Dispose();

		foreach (GameTexture? texture in blueArrows)
		{
			texture?.Dispose();
		}
		foreach (GameTexture? texture in grayArrows)
		{
			texture?.Dispose();
		}

		frame?.Dispose();
	}
}
