// 화면 출력과 독립적인 게임 규칙. 테스트에서는 정답 문 생성기를 전달합니다.
enum GamePhase { Title, Playing, Entering, Clear, GameOver }
enum DoorSide { Left, Right }

sealed class GameSession
{
    public const int TopFloor = 5;
    public const double EntryDuration = 1.3;
    private readonly Func<int> _nextDoor;
    private DoorSide _correctDoor;

    public GamePhase Phase { get; private set; } = GamePhase.Title;
    public int Floor { get; private set; } = 1;
    public DoorSide? SelectedDoor { get; private set; }
    public int Choices { get; private set; }
    public double PlayTime { get; private set; }
    public double SceneTime { get; private set; }
    public double EntryTime { get; private set; }
    public string Message { get; private set; } = "두 문 중 하나를 선택하세요.";

    public GameSession(Func<int>? nextDoor = null)
    {
        _nextDoor = nextDoor ?? (() => Random.Shared.Next(2));
    }

    public void Start()
    {
        Floor = 1;
        Choices = 0;
        PlayTime = 0;
        EnterFloor("두 문 중 하나를 선택하세요.");
    }

    public void ReturnToTitle()
    {
        Phase = GamePhase.Title;
        SceneTime = 0;
        EntryTime = 0;
        SelectedDoor = null;
    }

    public void Select(DoorSide side)
    {
        if (Phase != GamePhase.Playing) return;
        SelectedDoor = side;
    }

    public void Confirm()
    {
        if (Phase != GamePhase.Playing || SelectedDoor == null) return;
        Phase = GamePhase.Entering;
        EntryTime = 0;
        Choices++;
    }

    public void Update(double deltaTime)
    {
        if (!double.IsFinite(deltaTime) || deltaTime < 0) return;
        SceneTime += deltaTime;
        if (Phase is GamePhase.Playing or GamePhase.Entering)
            PlayTime += deltaTime;
        if (Phase != GamePhase.Entering) return;

        EntryTime += deltaTime;
        if (EntryTime < EntryDuration) return;

        if (SelectedDoor == _correctDoor)
        {
            if (Floor == TopFloor)
                Finish(GamePhase.Clear);
            else
            {
                Floor++;
                EnterFloor($"성공! {Floor}층으로 올라왔습니다.");
            }
        }
        else if (Floor == 1)
            Finish(GamePhase.GameOver);
        else
        {
            Floor--;
            EnterFloor($"잘못된 문입니다. {Floor}층으로 돌아왔습니다.");
        }
    }

    private void EnterFloor(string message)
    {
        // 층에 도착할 때마다 두 문 중 정확히 하나를 정답으로 정합니다.
        _correctDoor = _nextDoor() == 0 ? DoorSide.Left : DoorSide.Right;
        SelectedDoor = null;
        EntryTime = 0;
        SceneTime = 0;
        Message = message;
        Phase = GamePhase.Playing;
    }

    private void Finish(GamePhase phase)
    {
        Phase = phase;
        SceneTime = 0;
        EntryTime = 0;
        SelectedDoor = null;
    }
}
