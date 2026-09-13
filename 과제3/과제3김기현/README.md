# Go to Top — 탑 오르기

- 학번 / 작성자: 26031007 / KKH
- 시작일: 2026-08-29
- 3차 과제 구현일: 2026-09-13
- 환경: C# / .NET 9 / Windows Forms / 교수님 glc2d 템플릿

두 문 중 하나를 선택하며 5층의 탑을 오르는 1인칭 확률 선택 게임입니다.
제공된 `resource` 폴더의 PNG 15개를 실제 게임 화면에 적용했습니다.

## 실행

Windows와 .NET 9 SDK가 필요합니다. Visual Studio에서
`RandomDoor_tower/RandomDoor_tower.sln`을 열고 실행하거나, 저장소 루트에서 다음 명령을 사용합니다.

```powershell
dotnet run --project RandomDoor_tower/RandomDoor_tower.csproj
```

Release 빌드:

```powershell
dotnet build RandomDoor_tower/RandomDoor_tower.csproj -c Release
```

실행 파일은 `RandomDoor_tower/bin/Release/net9.0-windows/RandomDoor_tower.exe`입니다.
리소스는 빌드 및 게시 시 실행 폴더의 `resource` 하위에 자동 복사됩니다.
배포할 때는 EXE만 복사하지 말고 같은 폴더의 DLL, 설정 파일, `resource`를 함께 전달합니다.
실행용 PC에는 .NET 9 Desktop Runtime이 필요합니다.

## 조작법

| 화면 | 키 | 동작 |
| --- | --- | --- |
| 시작 | Enter | 1층에서 새 게임 시작 |
| 플레이 | A / D | 왼쪽 / 오른쪽 문 선택 또는 변경 |
| 플레이 | Enter | 선택한 문에 진입 |
| 플레이 / 결과 | Esc | 시작 화면으로 복귀 |
| 결과 | Enter / R | 1층부터 재도전 |
| 전체 | Alt + Enter | 전체 화면 전환 |

메뉴와 문 선택은 키보드로 조작합니다. 창의 닫기 버튼으로 종료합니다.
선택 전 Enter 입력은 무시하며, 문 진입 연출 중에는 문 변경과 중복 확정을 막습니다.

## 구현 내용

- 시작 화면: 탑 배경, 제목, 시작 안내.
- 플레이 화면: 1~5층 배경, 닫힌 문 두 개, 플레이어의 양손, 현재 층과 조작 안내.
- 선택 표현: 선택한 문을 반쯤 열린 이미지로 바꾸고 해당 손을 선택 자세로 변경.
- 진입 연출: 문을 완전히 연 뒤 선택한 문 방향으로 확대하고 검은색으로 페이드.
- 게임 규칙: 층에 도착할 때마다 왼쪽/오른쪽 중 정확히 하나를 같은 확률로 정답으로 선정.
- 정답이면 한 층 상승, 오답이면 한 층 하강. 1층 오답은 게임 오버.
- 5층의 정답 문까지 통과하면 보물 엔딩. 결과 화면에 문 선택 횟수와 플레이 시간을 표시.
- 결과 화면에서 재도전 시 층, 선택, 횟수, 시간 초기화.
- 창이 비활성화된 동안 진행과 입력을 멈춤. 짧은 키 입력도 인식하도록 입력 이벤트를 보관.

사운드 파일은 제공 리소스에 없어 이번 구현에는 포함하지 않았습니다.

## 코드 구조

| 파일 | 역할 |
| --- | --- |
| `AppMain.cs` | 실제 실행 진입점 |
| `GameMain.cs` | 교수님 템플릿의 객체 생성, 갱신, 렌더, 해제 연결 |
| `GameSession.cs` | 화면 상태, 문 선택, 층 이동, 승리·실패 규칙 |
| `GameScene.cs` | 이미지와 글꼴 생성, 화면 합성, 줌·페이드, 리소스 해제 |
| `GameGlobal.cs` | 창 제목과 기준 해상도 1170×659 |
| `glc2d/G2InputContext.cs` | 기존 입력 처리에 짧은 키 입력 보관 및 포커스 해제 시 초기화 추가 |

`Program.cs`는 기존에 주석 처리된 파일이며, 실행은 `AppMain.cs`에서 시작합니다.

## 실제 실행 화면

![시작 화면](doc/screenshots/title.png)
![1층 게임 화면](doc/screenshots/floor1.png)
![왼쪽 문 선택](doc/screenshots/selection.png)

## 검증 및 제출

게임 규칙 자동 검사:

```powershell
dotnet run --project tests/GameSession.Tests.csproj
```

시작, 선택과 중복 입력, 1층 실패, 오답 하강과 재추첨, 5층 최종 문 클리어,
재도전 초기화, 진입 중 메뉴 복귀를 검사합니다.

- [게임 기획서](doc/gameDesignFile.md)
- [3차 과제 구현 및 검증 기록](doc/assignment3.md)
- 제출 태그: `assignment-3`
