// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

class GameMain : G2AppBase
{
	public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
	public override string GameName => GameGlobal.GameName;
	private readonly GameSession _session = new();
	private GameScene? _scene;

	protected override void Initialize()
	{
		//---------------------------------------
		// 게임 관련 객체를 생성합니다.
		//---------------------------------------
		_scene = new GameScene(_session);
		_scene.Initialize();
	}

	protected override void Update()
	{
		//---------------------------------------
		// 게임 관련 객체를 갱신합니다.
		//---------------------------------------
		// 다른 창을 사용하는 동안 게임 입력과 진행을 멈춥니다.
		if (Form.ActiveForm == null) return;

		bool alt = Input.KeyState(Keys.Menu) is
			G2InputContext.InputState.Down or G2InputContext.InputState.Press;
		// Alt+Enter는 교수님 프레임워크의 전체 화면 전환에만 사용합니다.
		bool confirm = Input.IsKeyDown(Keys.Enter) && !alt;
		_session.Update(Math.Min(DeltaTime, 0.1));
		if (Input.IsKeyDown(Keys.Escape))
		{
			_session.ReturnToTitle();
			return;
		}

		switch (_session.Phase)
		{
			case GamePhase.Title:
				if (confirm) _session.Start();
				break;
			case GamePhase.Playing:
				if (Input.IsKeyDown(Keys.A)) _session.Select(DoorSide.Left);
				if (Input.IsKeyDown(Keys.D)) _session.Select(DoorSide.Right);
				if (confirm) _session.Confirm();
				break;
			case GamePhase.Clear:
			case GamePhase.GameOver:
				if (confirm || Input.IsKeyDown(Keys.R)) _session.Start();
				break;
		}
	}

	protected override void Render()
	{
		//---------------------------------------
		// 게임 관련 객체를 렌더링 합니다.
		//---------------------------------------
		_scene?.Render();
	}

	public override void Dispose()
	{
		//---------------------------------------
		// 게임 관련 객체를 해제합니다.
		//---------------------------------------
		_scene?.Dispose();
		_scene = null;
		base.Dispose();
	}
}
