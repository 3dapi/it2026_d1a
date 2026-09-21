// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;

	private SceneType currentScene = SceneType.Title;
	private GameTexture? background;
	private TitleScene? titleScene;
	private GameplayScene? gameplayScene;
	private ResultScene? resultScene;

	protected override void Initialize()
	{
		background = new GameTexture("resource/image/ui/background.png");
		titleScene = new TitleScene(this);
		gameplayScene = new GameplayScene(this);
		resultScene = new ResultScene(this);

		titleScene.Initialize();
		gameplayScene.Initialize();
		resultScene.Initialize();
	}

	protected override void Update()
	{
		switch (currentScene)
		{
			case SceneType.Title:
				titleScene?.Update();
				break;
			case SceneType.Gameplay:
				gameplayScene?.Update();
				break;
			case SceneType.Result:
				resultScene?.Update();
				break;
		}
	}

	protected override void Render()
	{
		background?.DrawCenter(640, 360);

		switch (currentScene)
		{
			case SceneType.Title:
				titleScene?.Draw();
				break;
			case SceneType.Gameplay:
				gameplayScene?.Draw();
				break;
			case SceneType.Result:
				resultScene?.Draw();
				break;
		}
	}

	public void ChangeScene(SceneType nextScene)
	{
		if (currentScene == SceneType.Gameplay)
		{
			gameplayScene?.Stop();
		}

		currentScene = nextScene;

		if (currentScene == SceneType.Gameplay)
		{
			gameplayScene?.Start();
		}
	}

	public override void Dispose()
	{
		resultScene?.Dispose();
		gameplayScene?.Dispose();
		titleScene?.Dispose();
		background?.Dispose();

		base.Dispose();
	}
}
