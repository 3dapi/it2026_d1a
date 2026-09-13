# glc2d Framework 사용 설명

`glc2d`는 C#에서 2D 게임 실습을 단순하게 진행하기 위한 소형 프레임워크입니다. WinForms 창을 기반으로 Direct2D/DirectWrite를 이용해 화면과 문자열을 출력하고, XAudio2와 NAudio를 이용해 WAV/MP3 사운드를 재생합니다.

프레임워크 사용자는 주로 `GameGlobal`, `GameMain`, `SceneMain`을 작성하고, `G2AppBase`가 게임 루프와 기본 시스템을 관리합니다.

---

## 1. 라이브러리 의존성

`glc2d`는 .NET의 기본 라이브러리 외에 **Vortice.Windows** 계열 라이브러리와 **NAudio**를 사용합니다.

- **Vortice.Direct2D1**: 2D Texture와 도형 출력
- **Vortice.DirectWrite**: 문자열 출력에 사용하며 `Vortice.Direct2D1`의 의존 패키지로 함께 설치됩니다.
- **Vortice.WIC**: PNG, JPG 등의 이미지 파일 로드에 사용하며 `Vortice.Direct2D1`의 의존 패키지로 함께 설치됩니다.
- **Vortice.WinForms**: WinForms 기반 RenderForm과 RenderLoop 사용
  - 주의: 윈도우 애플리케이션 설정으로 변경해야 설치가 됩니다.
- **Vortice.XAudio2**: WAV/MP3 사운드 출력
- **Vortice.XInput**: 게임패드 입력 기능 확장을 위해 포함
- **NAudio**: MP3 파일을 PCM 데이터로 디코딩

현재 프로젝트에서 사용하는 NuGet 패키지는 다음과 같습니다.

```xml
<ItemGroup>
	<PackageReference Include="Vortice.Direct2D1" Version="xxx" />
	<PackageReference Include="Vortice.WinForms" Version="xxx" />
	<PackageReference Include="Vortice.XAudio2" Version="xxx" />
	<PackageReference Include="Vortice.XInput" Version="xxx" />
	<PackageReference Include="NAudio" Version="xxx" />    
</ItemGroup>
```

Visual Studio에서는 프로젝트의 **NuGet 패키지 관리**에서 위 패키지를 설치하거나 `.csproj` 파일에 `PackageReference`를 추가한 뒤 패키지를 복원합니다.

`glc2d` 소스만 복사하고 위 패키지를 설치하지 않으면 `Vortice.*`, `NAudio.*` 형식을 찾을 수 없다는 컴파일 오류가 발생합니다.

---

## 2. 구성

```text
GameApp
│
├─ glc2d
│  ├─ G2AppBase.cs
│  ├─ G2D2DContext.cs
│  ├─ G2InputContext.cs
│  ├─ G2Font.cs
│  ├─ G2Texture.cs
│  ├─ G2TextureLoader.cs
│  ├─ G2AudioContext.cs
│  ├─ G2AudioSound.cs
│  └─ G2AudioMp3.cs
│
├─ GameGlobal.cs
├─ GameMain.cs
├─ SceneMain.cs
└─ Program.cs
```

### 주요 클래스

| 클래스 | 역할 |
|---|---|
| `G2AppBase` | 게임 창, 게임 루프, 시간, 입력, 렌더링, 전체화면 전환 관리 |
| `G2D2DContext` | Direct2D / DirectWrite 생성과 RenderTarget 관리 |
| `G2InputContext` | 키보드, 마우스 버튼, 마우스 위치, 휠 입력 관리 |
| `G2Font` | DirectWrite 문자열 출력 |
| `G2Texture` | 이미지 파일 로드, 캐시, 화면 출력 |
| `G2TextureLoader` | WIC를 이용한 이미지 디코딩 |
| `G2AudioContext` | XAudio2와 Mastering Voice 관리 |
| `G2AudioSound` | WAV 효과음 재생 |
| `G2AudioMp3` | MP3 파일 디코딩 및 재생 |
| `GameGlobal` | 게임 이름과 가상 화면 크기 설정 |
| `GameMain` | `G2AppBase`를 상속하여 게임 전체 흐름 연결 |
| `SceneMain` | 실제 게임 장면의 초기화, 갱신, 출력 |

---

## 3. 프로그램 시작

프로그램 시작점은 `Program.cs`입니다.

```csharp
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        using GameMain app = new();
        app.Run();
    }
}
```

`GameMain` 객체를 생성하면 `G2AppBase`의 생성자가 먼저 실행되어 `RenderForm`, Direct2D/DirectWrite, XAudio2, 입력 시스템을 준비합니다.

```text
GameMain 생성
↓
G2AppBase 생성자
↓
Run()
↓
Initialize()
↓
Update()
↓
Render()
↓
Update()
↓
Render()
↓
...
```

`Run()`은 `Initialize()`를 한 번 호출한 뒤 게임 루프에 진입합니다.

---

## 4. 게임 기본 설정

게임 이름과 기준 해상도는 `GameGlobal.cs`에서 지정합니다.

```csharp
public static class GameGlobal
{
    public static readonly System.Drawing.Size ScreenSize = new(960, 640);
    public static readonly string GameName = "Nemo .....";
}
```

`GameMain`에서 `G2AppBase`의 설정을 override합니다.

```csharp
class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize
        => GameGlobal.ScreenSize;

    public override string GameName
        => GameGlobal.GameName;

    ...
}
```

`G2AppBase`가 제공하는 기본값은 다음과 같습니다.

```csharp
public virtual System.Drawing.Size ScreenSize => new(640, 480);
public virtual string GameName => "G2 Game";
public virtual Color4 ClearColor { get; set; }
    = new(0.0f, 0.0f, 0.0f, 1.0f);
```

`ScreenSize`는 실제 Window 크기와 별개인 **게임의 기준 해상도**입니다. Window 크기가 변경되면 Direct2D 출력은 기준 해상도에 맞춰 자동 확대/축소됩니다.

현재 배율은 다음 값으로 얻을 수 있습니다.

```csharp
float scaleX = G2AppBase.ScreenScaleX;
float scaleY = G2AppBase.ScreenScaleY;
```

---

## 5. GameMain 구성

`GameMain`은 프레임워크와 Scene을 연결합니다.

```csharp
class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize
        => GameGlobal.ScreenSize;

    public override string GameName
        => GameGlobal.GameName;

    private SceneMain _sceneMain = new SceneMain();

    protected override void Initialize()
    {
        _sceneMain.Initialize();
    }

    protected override void Update()
    {
        _sceneMain.Update();
    }

    protected override void Render()
    {
        _sceneMain.Render();
    }

    public override void Dispose()
    {
        _sceneMain.Dispose();
        base.Dispose();
    }
}
```

`Dispose()`에서는 Scene에서 생성한 Texture, Font, Sound 등의 객체를 먼저 해제하고 마지막에 `base.Dispose()`를 호출합니다.

```text
Scene 리소스 해제
↓
G2AppBase 해제
↓
Direct2D / Audio / Input / Window 해제
```

---

## 6. Scene 기본 구조

```csharp
class SceneMain : IDisposable
{
    public void Initialize()
    {
    }

    public void Update()
    {
    }

    public void Render()
    {
    }

    public void Dispose()
    {
    }
}
```

| 함수 | 역할 |
|---|---|
| `Initialize()` | Texture, Font, Sound 등의 리소스 생성 |
| `Update()` | 입력 처리, 위치 계산, 게임 상태 변경 |
| `Render()` | Texture와 문자열 등의 화면 출력 |
| `Dispose()` | 생성한 리소스 해제 |

---

## 7. 시간 사용

`G2AppBase`는 매 프레임 시간을 갱신합니다.

```csharp
double DeltaTime;
double TotalTime;
```

`DeltaTime`은 이전 프레임부터 현재 프레임까지 흐른 시간입니다.

```csharp
float speed = 200.0f;
x += speed * (float)DeltaTime;
```

`TotalTime`은 게임 루프가 시작된 뒤 흐른 전체 시간입니다.

```csharp
double elapsed = TotalTime;
```

시간에 따라 색상을 변화시키는 예:

```csharp
ClearColor = new Color4(
    (float)(Math.Sin(TotalTime) * 0.5 + 0.5),
    0.0f,
    0.0f,
    1.0f);
```

---

## 8. 키보드 입력

입력은 `G2AppBase.Input` 또는 `G2AppBase.Instance.Input`을 통해 사용합니다.

Scene에서는 다음과 같이 얻을 수 있습니다.

```csharp
var input = G2AppBase.Instance?.Input
    ?? throw new InvalidOperationException(
        "G2AppBase instance is not initialized.");
```

### 7.1 키를 처음 누른 순간: Down

```csharp
if (input.IsKeyDown(Keys.Space))
{
    // Space를 누른 첫 프레임
}
```

```text
0 → 1 : Down
```

점프, 공격, 메뉴 선택처럼 한 번만 실행해야 하는 입력에 적합합니다.

### 7.2 키를 계속 누르고 있는 상태: Press

```csharp
if (input.IsKeyPress(Keys.Right))
{
    // 계속 누르고 있는 동안 실행
}
```

```text
1 → 1 : Press
```

### 7.3 키를 놓은 순간: Up

```csharp
if (input.IsKeyUp(Keys.Space))
{
    // Space를 놓은 첫 프레임
}
```

```text
1 → 0 : Up
```

### 7.4 입력 상태 직접 얻기

```csharp
G2InputContext.InputState state = input.KeyState(Keys.Space);
```

가능한 상태:

```csharp
G2InputContext.InputState.None
G2InputContext.InputState.Down
G2InputContext.InputState.Up
G2InputContext.InputState.Press
```

---

## 9. 마우스 입력

### 8.1 마우스 버튼

왼쪽 버튼을 누른 첫 프레임:

```csharp
if (input.IsButtonDown(MouseButtons.Left))
{
}
```

계속 누르는 상태:

```csharp
if (input.IsButtonPress(MouseButtons.Left))
{
}
```

버튼을 놓은 순간:

```csharp
if (input.IsButtonUp(MouseButtons.Left))
{
}
```

지원 버튼:

```text
Left
Right
Middle
XButton1
XButton2
```

### 8.2 마우스 위치

```csharp
PointF mousePos = input.MousePosition;

float x = mousePos.X;
float y = mousePos.Y;
```

마우스 좌표는 실제 Window 크기가 아니라 `ScreenSize` 기준 좌표로 변환됩니다.

### 8.3 마우스 이동량

```csharp
PointF delta = input.MouseDelta;

float dx = delta.X;
float dy = delta.Y;
```

### 8.4 마우스 휠

```csharp
int wheel = input.MouseWheelDelta;

if (wheel > 0)
{
    // 위 방향
}
else if (wheel < 0)
{
    // 아래 방향
}
```

`MouseWheelDelta`는 한 프레임 동안 발생한 휠 변화량입니다.

---

## 10. 문자열 출력

문자열 출력은 `G2Font`를 사용합니다.

### 9.1 Font 생성

```csharp
private G2Font? _font;
```

`Initialize()`에서 생성합니다.

```csharp
_font = new G2Font("Arial", 32);
```

기본 설정은 `Heavy`, `Normal`, `Leading`, `Near`입니다.

세부 옵션 지정:

```csharp
_font = new G2Font(
    "Arial",
    24,
    FontWeight.Normal,
    FontStyle.Normal,
    TextAlignment.Center,
    ParagraphAlignment.Center);
```

### 9.2 문자열 출력

```csharp
_font?.DrawText(
    "Hello G2",
    new Rect(20, 20, 400, 100),
    new Color4(1.0f, 1.0f, 1.0f, 1.0f));
```

`Rect`는 문자열을 배치할 영역입니다.

```csharp
new Rect(x, y, width, height)
```

점수 출력 예:

```csharp
_font?.DrawText(
    "Score : 1000",
    new Rect(20, 20, 300, 50),
    new Color4(1.0f, 1.0f, 0.0f, 1.0f));
```

마우스 좌표 출력 예:

```csharp
PointF mousePos = input.MousePosition;
string text = $"Mouse ({(int)mousePos.X}, {(int)mousePos.Y})";

_font?.DrawText(
    text,
    new Rect(20, 20, 500, 50),
    new Color4(0.0f, 1.0f, 1.0f, 1.0f));
```

### 9.3 Font 해제

```csharp
public void Dispose()
{
    _font?.Dispose();
}
```

동일한 설정으로 생성한 Font의 `TextFormat`은 내부에서 공유되고 참조 횟수로 관리됩니다.

---

## 11. Texture 사용

### 10.1 Texture 생성

```csharp
private G2Texture? _texture;
```

`Initialize()`에서 이미지 파일을 로드합니다.

```csharp
_texture = new G2Texture(
    "resource/texture/res_checker.png");
```

상대 경로는 실행 프로그램 기준 경로로 변환됩니다. 같은 이미지 파일을 여러 `G2Texture`가 사용하면 내부 Bitmap은 공유되고 참조 횟수로 관리됩니다.

### 10.2 Texture 전체 출력

```csharp
_texture?.Draw();
```

투명도 지정:

```csharp
_texture?.Draw(0.5f);
```

```text
1.0 : 완전 불투명
0.5 : 반투명
0.0 : 완전 투명
```

### 10.3 Texture 일부 영역 출력

```csharp
_texture?.Draw(
    new Rect(400, 300, 300, 200),
    new Rect(200, 100, 300, 200));
```

첫 번째 `Rect`는 화면에 출력할 영역이고, 두 번째 `Rect`는 원본 Texture에서 사용할 영역입니다.

Sprite Sheet 예:

```csharp
Rect source = new Rect(
    32,
    0,
    32,
    32);

Rect destination = new Rect(
    100,
    100,
    64,
    64);

_texture?.Draw(
    destination,
    source);
```

### 10.4 Texture 보간 방식

기본값은 `Linear`입니다.

```csharp
_texture?.Draw(
    1.0f,
    BitmapInterpolationMode.Linear);
```

픽셀 아트 확대 시:

```csharp
_texture?.Draw(
    1.0f,
    BitmapInterpolationMode.NearestNeighbor);
```

부분 출력에도 동일하게 적용할 수 있습니다.

```csharp
_texture?.Draw(
    destination,
    source,
    1.0f,
    BitmapInterpolationMode.NearestNeighbor);
```

---

## 12. WAV 효과음

짧은 효과음은 `G2AudioSound`를 사용합니다.

### 생성

```csharp
private G2AudioSound? _soundEffect;
```

```csharp
_soundEffect = new G2AudioSound(
    "resource/audio/effect/move3.wav");
```

### 재생

```csharp
_soundEffect?.Play();
```

기본값은 반복하지 않는 1회 재생입니다.

```csharp
_soundEffect?.Play(false);
```

반복 재생:

```csharp
_soundEffect?.Play(true);
```

`Play()`를 다시 호출하면 기존 재생을 정지하고 처음부터 다시 재생합니다.

### 재생 상태

```csharp
if (_soundEffect?.IsPlaying() == true)
{
}
```

### 정지

```csharp
_soundEffect?.Stop();
```

### 마우스 클릭 효과음

```csharp
if (input.IsButtonDown(MouseButtons.Left))
{
    _soundEffect?.Play();
}
```

---

## 13. MP3 배경 음악

```csharp
private G2AudioMp3? _backgroundMusic;
```

`Initialize()`에서 생성합니다.

```csharp
_backgroundMusic = new G2AudioMp3(
    "resource/audio/bgm/background.mp3");
```

배경 음악 반복 재생:

```csharp
_backgroundMusic.Play(true);
```

`G2AudioMp3.Play()`의 기본값은 반복 재생입니다.

```csharp
_backgroundMusic.Play();
```

1회 재생:

```csharp
_backgroundMusic.Play(false);
```

정지:

```csharp
_backgroundMusic.Stop();
```

재생 여부:

```csharp
if (_backgroundMusic.IsPlaying())
{
}
```

현재 `G2AudioMp3`는 MP3 전체를 PCM 데이터로 디코딩한 뒤 메모리에 보관하고 XAudio2로 재생합니다.

---

## 14. 사운드 리소스 해제

Scene에서 만든 Sound와 Music은 Scene이 종료될 때 해제합니다.

```csharp
public void Dispose()
{
    _backgroundMusic?.Dispose();
    _soundEffect?.Dispose();
}
```

같은 파일을 여러 객체에서 사용하면 내부 PCM 데이터는 공유되고 참조 횟수로 관리됩니다.

---

## 15. 화면 배경색

`ClearColor`로 매 프레임 화면을 지울 색을 설정할 수 있습니다.

`GameMain`에서 직접 변경:

```csharp
ClearColor = new Color4(
    0.1f,
    0.1f,
    0.2f,
    1.0f);
```

Scene에서 변경:

```csharp
var app = G2AppBase.Instance
    ?? throw new InvalidOperationException(
        "G2AppBase instance is not initialized.");

app.ClearColor = new Color4(
    0.0f,
    0.0f,
    0.0f,
    1.0f);
```

---

## 16. 전체화면 전환

프레임워크는 `ALT + ENTER`를 내부에서 처리합니다.

```text
ALT + ENTER
```

을 누르면 창 모드와 전체화면 모드를 전환합니다. 게임 코드에서 별도로 처리할 필요가 없습니다.

---

## 17. ESC로 게임 종료

`G2AppBase`는 `Close()`를 제공합니다.

```csharp
G2AppBase.Instance?.Close();
```

ESC를 눌렀을 때 종료 여부를 묻는 예:

```csharp
if (input.IsKeyDown(Keys.Escape))
{
    DialogResult result = MessageBox.Show(
        "정말 종료하시겠습니까?",
        "프로그램 종료",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

    if (result == DialogResult.Yes)
    {
        G2AppBase.Instance?.Close();
    }
}
```

`IsKeyDown()`을 사용하므로 ESC를 계속 누르고 있어도 첫 프레임에만 처리됩니다.

---

## 18. 기본 SceneMain 예제

Texture, Font, Input, Sound, Music을 한 번에 사용하는 예제입니다.

```csharp
using System.Windows.Forms;
using Vortice.Mathematics;

class SceneMain : IDisposable
{
    private G2Font? _font;
    private G2Texture? _texture;
    private G2AudioSound? _soundEffect;
    private G2AudioMp3? _backgroundMusic;

    private string _mouseText = string.Empty;

    public void Initialize()
    {
        _font = new G2Font(
            "Arial",
            24);

        _texture = new G2Texture(
            "resource/texture/res_checker.png");

        _soundEffect = new G2AudioSound(
            "resource/audio/effect/move3.wav");

        _backgroundMusic = new G2AudioMp3(
            "resource/audio/bgm/background.mp3");

        _backgroundMusic.Play(true);
    }

    public void Update()
    {
        var input = G2AppBase.Instance?.Input
            ?? throw new InvalidOperationException(
                "G2AppBase instance is not initialized.");

        if (input.IsKeyDown(Keys.Escape))
        {
            DialogResult result = MessageBox.Show(
                "정말 종료하시겠습니까?",
                "프로그램 종료",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                G2AppBase.Instance?.Close();
                return;
            }
        }

        if (input.IsButtonDown(MouseButtons.Left))
        {
            _soundEffect?.Play();
        }

        var mousePos = input.MousePosition;
        _mouseText =
            $"Mouse : {(int)mousePos.X}, {(int)mousePos.Y}";
    }

    public void Render()
    {
        _texture?.Draw();

        _font?.DrawText(
            _mouseText,
            new Rect(20, 20, 500, 50),
            new Color4(1.0f, 1.0f, 1.0f, 1.0f));
    }

    public void Dispose()
    {
        _backgroundMusic?.Dispose();
        _soundEffect?.Dispose();
        _texture?.Dispose();
        _font?.Dispose();
    }
}
```

---

## 19. 리소스 생성과 해제 원칙

Texture, Font, Sound, Music은 매 프레임 생성하지 않습니다.

잘못된 예:

```csharp
public void Render()
{
    G2Texture texture = new G2Texture("player.png");
    texture.Draw();
}
```

리소스는 `Initialize()`에서 한 번 생성합니다.

```csharp
public void Initialize()
{
    _texture = new G2Texture("player.png");
}
```

`Render()`에서는 생성된 객체를 사용합니다.

```csharp
public void Render()
{
    _texture?.Draw();
}
```

Scene이 끝날 때 해제합니다.

```csharp
public void Dispose()
{
    _texture?.Dispose();
}
```

```text
Initialize
↓
Resource 생성
↓
Update / Render 반복
↓
Dispose
↓
Resource 해제
```

---

## 20. 게임 루프에서 각 함수의 역할

### Initialize

```csharp
public void Initialize()
{
    // Texture
    // Font
    // Sound
    // Music
    // 게임 데이터 초기화
}
```

### Update

```csharp
public void Update()
{
    // Input
    // 이동
    // 충돌
    // 상태 변경
    // 게임 규칙 처리
}
```

### Render

```csharp
public void Render()
{
    // Texture 출력
    // 문자열 출력
}
```

`Render()`에서 게임 상태를 변경하거나 `Update()`에서 Texture를 출력하는 식으로 역할을 섞지 않는 것이 좋습니다.

---

## 21. 자주 사용하는 코드 모음

키 한 번 입력:

```csharp
if (input.IsKeyDown(Keys.Space))
{
}
```

키 계속 입력:

```csharp
if (input.IsKeyPress(Keys.Left))
{
}
```

키를 놓음:

```csharp
if (input.IsKeyUp(Keys.Space))
{
}
```

마우스 클릭:

```csharp
if (input.IsButtonDown(MouseButtons.Left))
{
}
```

마우스 위치:

```csharp
PointF pos = input.MousePosition;
```

효과음:

```csharp
_soundEffect?.Play();
```

배경 음악:

```csharp
_backgroundMusic?.Play(true);
```

Texture 전체 출력:

```csharp
_texture?.Draw();
```

Texture 영역 출력:

```csharp
_texture?.Draw(destination, source);
```

문자열 출력:

```csharp
_font?.DrawText(
    "Hello",
    new Rect(10, 10, 300, 50),
    new Color4(1, 1, 1, 1));
```

게임 종료:

```csharp
G2AppBase.Instance?.Close();
```

---

## 22. 전체 실행 구조

```text
Program.Main
    ↓
new GameMain
    ↓
G2AppBase 생성
    ├─ RenderForm
    ├─ G2D2DContext
    ├─ G2AudioContext
    └─ G2InputContext
    ↓
GameMain.Run
    ↓
GameMain.Initialize
    ↓
SceneMain.Initialize
    ↓
┌──────────────────────────────┐
│          Game Loop           │
│                              │
│  Input Update                │
│      ↓                       │
│  GameMain.Update             │
│      ↓                       │
│  SceneMain.Update            │
│      ↓                       │
│  GameMain.Render             │
│      ↓                       │
│  SceneMain.Render            │
│                              │
└──────────────────────────────┘
    ↓
Close
    ↓
Scene Dispose
    ↓
G2AppBase Dispose
```

---

## 23. 최소 사용 예제

### GameGlobal.cs

```csharp
public static class GameGlobal
{
    public static readonly System.Drawing.Size ScreenSize
        = new(960, 640);

    public static readonly string GameName
        = "My Game";
}
```

### GameMain.cs

```csharp
class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize
        => GameGlobal.ScreenSize;

    public override string GameName
        => GameGlobal.GameName;

    private SceneMain _scene = new SceneMain();

    protected override void Initialize()
    {
        _scene.Initialize();
    }

    protected override void Update()
    {
        _scene.Update();
    }

    protected override void Render()
    {
        _scene.Render();
    }

    public override void Dispose()
    {
        _scene.Dispose();
        base.Dispose();
    }
}
```

### Program.cs

```csharp
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        using GameMain app = new();
        app.Run();
    }
}
```

게임의 실제 작업은 대부분 `SceneMain`과 이후 추가되는 Scene 또는 게임 객체 클래스에서 작성합니다.
