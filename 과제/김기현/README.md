# Go to Top — 3차 과제

교수님 템플릿에 리소스 출력과 게임 시작 부분을 구현했습니다.

## 구현 범위

- 실행 시 `main.png`로 시작 화면을 출력합니다.
- Enter를 누르면 1층 화면으로 전환합니다.
- `firstfloor.png` 배경 위에 `door_closed.png`를 두 번 그립니다.
- `leftidle.png`, `rightidle.png`로 플레이어의 양손을 출력합니다.
- 현재 층은 `1층`으로 표시합니다.

이번 단계는 여기까지이며, 문 선택·층 이동·확률 판정·엔딩은 구현하지 않았습니다.
창의 닫기 버튼으로 종료합니다.

## 코드 위치

- `GameMain.cs`: 객체 생성(`Initialize`), 시작 상태 갱신(`Update`), 이미지 출력(`Render`), 자원 해제(`Dispose`).
- `GameGlobal.cs`: 배경 원본 크기인 1170×659 해상도와 창 제목.
- `RandomDoor_tower.csproj`: 실행 폴더에 `resource`를 자동 복사하는 설정.

`AppMain.cs`와 `glc2d` 프레임워크 코드는 교수님이 제공한 상태 그대로입니다.
짧은 Enter 입력은 `GameMain`에서 창의 `KeyDown` 이벤트로 받아 처리합니다.

## 실행

Windows와 .NET 9 SDK 환경에서 `RandomDoor_tower/RandomDoor_tower.sln`을 열어 실행합니다.
명령으로 실행할 때는 저장소 루트에서 다음과 같이 입력합니다.

```powershell
dotnet run --project RandomDoor_tower/RandomDoor_tower.csproj
```
