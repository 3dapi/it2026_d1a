// 외부 테스트 패키지 없이 실행하는 게임 규칙 회귀 검사.
int passed = 0;
Run("시작 화면과 1층 초기화", () =>
{
    var game = new GameSession(() => 0);
    Check(game.Phase == GamePhase.Title, "시작 화면");
    game.Select(DoorSide.Left);
    game.Confirm();
    Check(game.Phase == GamePhase.Title && game.SelectedDoor == null, "메뉴 입력 차단");
    game.Start();
    Check(game.Phase == GamePhase.Playing && game.Floor == 1, "1층 시작");
    Check(game.SelectedDoor == null && game.Choices == 0 && game.PlayTime == 0, "초기값");
    game.Confirm();
    Check(game.Phase == GamePhase.Playing, "선택 없이 확정 불가");
});
Run("선택 변경 및 진입 중 중복 입력 차단", () =>
{
    var game = new GameSession(() => 1);
    game.Start();
    game.Select(DoorSide.Left);
    game.Select(DoorSide.Right);
    game.Confirm();
    game.Select(DoorSide.Left);
    game.Confirm();
    Check(game.SelectedDoor == DoorSide.Right && game.Choices == 1, "선택 고정");
    game.Update(GameSession.EntryDuration - 0.1);
    Check(game.Floor == 1 && game.Phase == GamePhase.Entering, "연출 완료 전 대기");
    game.Update(0.11);
    Check(game.Floor == 2 && game.Phase == GamePhase.Playing, "정답 상승");
    Check(game.SelectedDoor == null, "새 층에서 선택 초기화");
});
Run("1층에서 오답이면 게임 오버, 타이머 정지", () =>
{
    var game = new GameSession(() => 0);
    game.Start();
    Choose(game, DoorSide.Right);
    Check(game.Phase == GamePhase.GameOver && game.Floor == 1, "1층 실패");
    double time = game.PlayTime;
    game.Update(10);
    game.Confirm();
    Check(game.PlayTime == time && game.Choices == 1, "결과 화면 진행 정지");
});
Run("오답 하강 및 재방문 시 정답 재추첨", () =>
{
    int calls = 0;
    var game = new GameSession(() => calls++ % 2);
    game.Start();                         // 1층 정답: 왼쪽
    Choose(game, DoorSide.Left);           // 2층 정답: 오른쪽
    Choose(game, DoorSide.Left);           // 오답, 1층 정답: 왼쪽
    Check(game.Floor == 1 && game.Phase == GamePhase.Playing, "2층 오답은 즉시 종료하지 않음");
    Check(calls == 3 && game.SelectedDoor == null, "도착마다 한 번 추첨");
    Choose(game, DoorSide.Left);
    Check(game.Floor == 2, "재방문 정답으로 재상승");
});
Run("5층 진입만으로 클리어하지 않고 마지막 문까지 통과", () =>
{
    var game = new GameSession(() => 0);
    game.Start();
    for (int i = 0; i < 4; i++) Choose(game, DoorSide.Left);
    Check(game.Floor == 5 && game.Phase == GamePhase.Playing, "5층 플레이");
    Choose(game, DoorSide.Left);
    Check(game.Phase == GamePhase.Clear && game.Choices == 5, "다섯 문 통과");
});
Run("5층 오답은 4층, 재도전 시 모든 진행 초기화", () =>
{
    var game = new GameSession(() => 0);
    game.Start();
    for (int i = 0; i < 4; i++) Choose(game, DoorSide.Left);
    Choose(game, DoorSide.Right);
    Check(game.Floor == 4 && game.Phase == GamePhase.Playing, "5층 하강");
    game.Start();
    Check(game.Floor == 1 && game.Choices == 0 && game.PlayTime == 0, "재도전 초기화");
    Check(game.SelectedDoor == null && game.EntryTime == 0, "연출 초기화");
});
Run("진입 중 메뉴 복귀 및 재시작", () =>
{
    var game = new GameSession(() => 0);
    game.Start();
    game.Select(DoorSide.Left);
    game.Confirm();
    game.Update(0.5);
    game.ReturnToTitle();
    game.Update(5);
    Check(game.Phase == GamePhase.Title && game.EntryTime == 0, "진입 취소");
    game.Start();
    Check(game.Floor == 1 && game.Choices == 0, "메뉴에서 새 게임");
});
Console.WriteLine($"PASS: {passed} game-rule checks");

void Run(string name, Action test)
{
    test();
    passed++;
    Console.WriteLine($"PASS {name}");
}
static void Check(bool condition, string label)
{
    if (!condition) throw new InvalidOperationException(label);
}
static void Choose(GameSession game, DoorSide side)
{
    game.Select(side);
    game.Confirm();
    game.Update(GameSession.EntryDuration);
}
