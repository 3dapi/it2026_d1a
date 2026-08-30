using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

class SceneMain : IDisposable
{
    private G2Font? _hudFont;
    private G2Font? _titleFont;
    private G2Font? _smallFont;

    // Brushes
    private ID2D1SolidColorBrush? _skyBrush;
    private ID2D1SolidColorBrush? _groundBrush;
    private ID2D1SolidColorBrush? _grassBrush;
    private ID2D1SolidColorBrush? _playerBrush;
    private ID2D1SolidColorBrush? _enemyBrush;
    private ID2D1SolidColorBrush? _towerBrush;
    private ID2D1SolidColorBrush? _bulletBrush;
    private ID2D1SolidColorBrush? _laserBrush;
    private ID2D1SolidColorBrush? _hudBrush;
    private ID2D1SolidColorBrush? _panelBrush;
    private ID2D1SolidColorBrush? _greenBrush;
    private ID2D1SolidColorBrush? _redBrush;

    // 2D Player State (Terraria-style Side-view)
    private Vector2 _playerPos = new(200, 480);
    private Vector2 _playerVel = Vector2.Zero;
    private bool _isGrounded = false;
    private bool _isCrouching = false;
    private float _playerAimAngle = 0f;

    // Camera (Terraria Sniper Scope Camera Look-Ahead)
    private Vector2 _cameraPos = new(200, 480);
    private bool _isScoping = false;
    private float _breathOxygen = 100f;
    private bool _isHoldingBreath = false;

    // Environmental / Ballistics
    private float _wind = 3.5f; // m/s
    private readonly float _gravity = 980f; // px/s^2

    // Bullet System
    private struct Bullet
    {
        public Vector2 Pos;
        public Vector2 Vel;
        public float Life;
    }
    private readonly List<Bullet> _bullets = new();
    private readonly List<Vector2> _impactMarks = new();

    // Target (Distant Watchtower Guard)
    private Vector2 _targetPos = new(1350, 260);
    private float _targetHp = 100f;
    private float _targetHitTimer = 0f;

    public void Initialize()
    {
        var app = G2AppBase.Instance ?? throw new InvalidOperationException("App is null");
        var rt = app.RenderTarget;

        _hudFont = new G2Font("Malgun Gothic", 14, FontWeight.Bold, Vortice.DirectWrite.FontStyle.Normal, TextAlignment.Leading);
        _titleFont = new G2Font("Malgun Gothic", 18, FontWeight.Bold, Vortice.DirectWrite.FontStyle.Normal, TextAlignment.Center);
        _smallFont = new G2Font("Malgun Gothic", 12, FontWeight.Normal, Vortice.DirectWrite.FontStyle.Normal, TextAlignment.Leading);

        _skyBrush = rt.CreateSolidColorBrush(new Color4(0.12f, 0.18f, 0.28f, 1.0f));
        _groundBrush = rt.CreateSolidColorBrush(new Color4(0.35f, 0.22f, 0.12f, 1.0f));
        _grassBrush = rt.CreateSolidColorBrush(new Color4(0.25f, 0.65f, 0.25f, 1.0f));
        _playerBrush = rt.CreateSolidColorBrush(new Color4(0.2f, 0.8f, 1.0f, 1.0f));
        _enemyBrush = rt.CreateSolidColorBrush(new Color4(1.0f, 0.3f, 0.3f, 1.0f));
        _towerBrush = rt.CreateSolidColorBrush(new Color4(0.25f, 0.28f, 0.35f, 1.0f));
        _bulletBrush = rt.CreateSolidColorBrush(new Color4(1.0f, 0.95f, 0.2f, 1.0f));
        _laserBrush = rt.CreateSolidColorBrush(new Color4(0.0f, 0.9f, 1.0f, 0.35f));
        _hudBrush = rt.CreateSolidColorBrush(new Color4(0.0f, 0.9f, 1.0f, 1.0f));
        _panelBrush = rt.CreateSolidColorBrush(new Color4(0.05f, 0.08f, 0.15f, 0.85f));
        _greenBrush = rt.CreateSolidColorBrush(new Color4(0.2f, 1.0f, 0.4f, 1.0f));
        _redBrush = rt.CreateSolidColorBrush(new Color4(1.0f, 0.2f, 0.2f, 1.0f));
    }

    public void Update()
    {
        var app = G2AppBase.Instance;
        if (app == null) return;
        var input = app.Input;
        float dt = (float)app.DeltaTime;

        if (_targetHitTimer > 0) _targetHitTimer -= dt;

        // 1. Player 2D Movement (A / D / Space / S)
        float moveSpeed = _isCrouching ? 120f : 240f;
        float moveX = 0f;
        if (input.IsKeyPress(Keys.A)) moveX -= 1f;
        if (input.IsKeyPress(Keys.D)) moveX += 1f;

        _playerVel.X = moveX * moveSpeed;

        // Jump
        if ((input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Space)) && _isGrounded)
        {
            _playerVel.Y = -420f;
            _isGrounded = false;
        }

        // Crouch / Prone
        _isCrouching = input.IsKeyPress(Keys.S) || input.IsKeyPress(Keys.ControlKey);

        // Apply Gravity
        if (!_isGrounded)
        {
            _playerVel.Y += _gravity * dt;
        }

        // Apply Movement
        _playerPos += _playerVel * dt;

        // Ground Collision (Flat ground at Y = 500)
        float groundY = 500f - (_isCrouching ? 16f : 32f);
        if (_playerPos.Y >= groundY)
        {
            _playerPos.Y = groundY;
            _playerVel.Y = 0f;
            _isGrounded = true;
        }

        // 2. Terraria-style Sniper Scope & Camera Look-Ahead (Right Click Hold)
        _isScoping = input.IsButtonPress(MouseButtons.Right);

        // Hold Breath on Shift
        _isHoldingBreath = input.IsKeyPress(Keys.ShiftKey) && _breathOxygen > 5f;
        if (_isHoldingBreath)
        {
            _breathOxygen = Math.Max(0f, _breathOxygen - dt * 25f);
        }
        else
        {
            _breathOxygen = Math.Min(100f, _breathOxygen + dt * 20f);
        }

        // Mouse to World Coordinates conversion
        Vector2 screenCenter = new(640, 360);
        Vector2 mouseScreen = new(input.MousePosition.X, input.MousePosition.Y);
        Vector2 mouseWorld = _cameraPos + (mouseScreen - screenCenter);

        // Aim Angle from Player to Mouse
        Vector2 aimDir = mouseWorld - _playerPos;
        _playerAimAngle = MathF.Atan2(aimDir.Y, aimDir.X);

        // Camera Follow Target
        Vector2 targetCam = _playerPos;
        if (_isScoping)
        {
            // Terraria Telescope/Sniper Scope mechanism: pan camera toward cursor
            Vector2 lookOffset = (mouseWorld - _playerPos) * 0.75f;
            targetCam = _playerPos + lookOffset;
        }

        // Smooth Camera Lerp
        _cameraPos = Vector2.Lerp(_cameraPos, targetCam, dt * (_isScoping ? 6f : 8f));

        // 3. Firing (Left Mouse Button)
        if (input.IsButtonDown(MouseButtons.Left))
        {
            float muzzleSpeed = 1600f; // px/s
            float sway = _isHoldingBreath ? 0f : (MathF.Sin((float)app.TotalTime * 8f) * 0.035f);
            float finalAngle = _playerAimAngle + sway;

            Vector2 bulletVel = new Vector2(MathF.Cos(finalAngle), MathF.Sin(finalAngle)) * muzzleSpeed;
            _bullets.Add(new Bullet { Pos = _playerPos + new Vector2(MathF.Cos(finalAngle), MathF.Sin(finalAngle)) * 25f, Vel = bulletVel, Life = 2.5f });
        }

        // 4. Update Bullets (Ballistics: Gravity & Wind)
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            var b = _bullets[i];
            b.Vel.Y += 280f * dt;     // Bullet gravity drop
            b.Vel.X += _wind * 15f * dt; // Wind push
            b.Pos += b.Vel * dt;
            b.Life -= dt;

            // Hit Enemy Check
            if (Vector2.Distance(b.Pos, _targetPos) < 28f)
            {
                _targetHp = Math.Max(0f, _targetHp - 40f);
                _targetHitTimer = 0.2f;
                _impactMarks.Add(b.Pos);
                _bullets.RemoveAt(i);
                continue;
            }

            // Hit Ground
            if (b.Pos.Y >= 500f || b.Life <= 0f)
            {
                if (b.Pos.Y >= 500f) _impactMarks.Add(new Vector2(b.Pos.X, 500f));
                _bullets.RemoveAt(i);
            }
            else
            {
                _bullets[i] = b;
            }
        }
    }

    public void Render()
    {
        var app = G2AppBase.Instance;
        if (app == null) return;
        var rt = app.RenderTarget;
        Vector2 screenCenter = new(640, 360);

        // Helper function: World to Screen
        Vector2 WorldToScreen(Vector2 w) => w - _cameraPos + screenCenter;

        // 1. Clear Background (Sky)
        rt.Clear(new Color4(0.1f, 0.14f, 0.22f, 1.0f));

        // 2. Distant Parallax Mountains / Sky elements
        float parallax = _cameraPos.X * 0.15f;
        for (int i = -2; i < 6; i++)
        {
            float mx = i * 400 - parallax;
            rt.FillRectangle(new Rect(mx, 220, 380, 280), _towerBrush!);
        }

        // 3. Ground & Dirt Layer (Terraria Style)
        Vector2 groundScreen = WorldToScreen(new Vector2(0, 500));
        rt.FillRectangle(new Rect(0, groundScreen.Y, 1280, 720 - groundScreen.Y + 200), _groundBrush!);
        rt.FillRectangle(new Rect(0, groundScreen.Y, 1280, 14), _grassBrush!);

        // 4. Watchtower & Target (Distant Sniper Target at X=1350)
        Vector2 towerScreen = WorldToScreen(new Vector2(1300, 200));
        rt.FillRectangle(new Rect(towerScreen.X, towerScreen.Y, 100, 300), _towerBrush!);
        rt.DrawRectangle(new Rect(towerScreen.X, towerScreen.Y, 100, 300), _hudBrush!, 1.5f);

        // Target Enemy Sprite
        Vector2 enemyScreen = WorldToScreen(_targetPos);
        var targetBrush = (_targetHitTimer > 0) ? _redBrush! : _enemyBrush!;
        rt.FillRectangle(new Rect(enemyScreen.X - 12, enemyScreen.Y - 24, 24, 48), targetBrush);

        // Target HP Bar & Tag (Korean)
        if (_targetHp > 0)
        {
            rt.DrawRectangle(new Rect(enemyScreen.X - 30, enemyScreen.Y - 38, 60, 6), _hudBrush!, 1.0f);
            rt.FillRectangle(new Rect(enemyScreen.X - 29, enemyScreen.Y - 37, (_targetHp / 100f) * 58, 4), _redBrush!);
            _smallFont?.DrawText("VIP 표적 [650m]", new Rect(enemyScreen.X - 45, enemyScreen.Y - 58, 120, 18), new Color4(1f, 0.4f, 0.4f, 1f));
        }

        // 5. Impact Marks on Ground / Walls
        foreach (var mark in _impactMarks)
        {
            Vector2 sm = WorldToScreen(mark);
            rt.FillEllipse(new Ellipse(sm, 3f, 3f), _redBrush!);
        }

        // 6. Player Character (Terraria-style 2D Sprite Rendering)
        Vector2 pScreen = WorldToScreen(_playerPos);
        float pHeight = _isCrouching ? 24f : 44f;
        float pWidth = _isCrouching ? 36f : 20f;

        // Player Body
        rt.FillRectangle(new Rect(pScreen.X - pWidth / 2, pScreen.Y - pHeight / 2, pWidth, pHeight), _playerBrush!);
        rt.DrawRectangle(new Rect(pScreen.X - pWidth / 2, pScreen.Y - pHeight / 2, pWidth, pHeight), _hudBrush!, 1.5f);

        // Player Head / Hat
        rt.FillRectangle(new Rect(pScreen.X - 8, pScreen.Y - pHeight / 2 - 14, 16, 14), _grassBrush!);

        // Sniper Rifle Barrel Aimed at Mouse
        Vector2 gunMuzzle = pScreen + new Vector2(MathF.Cos(_playerAimAngle), MathF.Sin(_playerAimAngle)) * 32f;
        rt.DrawLine(pScreen, gunMuzzle, _hudBrush!, 4.0f);

        // Aim Laser Line (Long Range Guide)
        Vector2 laserEnd = pScreen + new Vector2(MathF.Cos(_playerAimAngle), MathF.Sin(_playerAimAngle)) * 600f;
        rt.DrawLine(gunMuzzle, laserEnd, _laserBrush!, 1.0f);

        // 7. Bullets in Flight
        foreach (var b in _bullets)
        {
            Vector2 bs = WorldToScreen(b.Pos);
            rt.DrawLine(bs - b.Vel * 0.015f, bs, _bulletBrush!, 3.0f);
        }

        // 8. Scope Lens Overlay (When Right-Clicking / Scoping)
        if (_isScoping)
        {
            // Lens Reticle in Center of View
            rt.DrawEllipse(new Ellipse(screenCenter, 220f, 220f), _hudBrush!, 2.0f);
            rt.DrawLine(new Vector2(screenCenter.X - 250, screenCenter.Y), new Vector2(screenCenter.X + 250, screenCenter.Y), _hudBrush!, 1.5f);
            rt.DrawLine(new Vector2(screenCenter.X, screenCenter.Y - 250), new Vector2(screenCenter.X, screenCenter.Y + 250), _hudBrush!, 1.5f);

            // Mil-dots
            for (int d = -4; d <= 4; d++)
            {
                if (d == 0) continue;
                rt.DrawLine(new Vector2(screenCenter.X - 6, screenCenter.Y + d * 30), new Vector2(screenCenter.X + 6, screenCenter.Y + d * 30), _hudBrush!, 1.5f);
                rt.DrawLine(new Vector2(screenCenter.X + d * 30, screenCenter.Y - 6), new Vector2(screenCenter.X + d * 30, screenCenter.Y + 6), _hudBrush!, 1.5f);
            }
        }

        // 9. Top HUD Bar & Controls (Korean)
        rt.FillRectangle(new Rect(15, 15, 420, 120), _panelBrush!);
        rt.DrawRectangle(new Rect(15, 15, 420, 120), _hudBrush!, 1.5f);

        float distToTarget = Vector2.Distance(_playerPos, _targetPos);
        string hudText = $"[ 2D 전술 저격: 오퍼레이션 아포리아 ]\n" +
                         $"좌표: ({_playerPos.X:F0}, {_playerPos.Y:F0})  |  풍속: {_wind:F1} m/s (동풍)\n" +
                         $"목표 거리: {distToTarget:F0} px (약 650m)\n" +
                         $"조준 상태: {(_isScoping ? "저격 스코프 활성화 (시야 확장)" : "일반 시야")}\n" +
                         $"호흡/산소량: {_breathOxygen:F0}% " + (_isHoldingBreath ? "[호흡 참는 중]" : "[정상]");
        _hudFont?.DrawText(hudText, new Rect(25, 22, 400, 95), new Color4(0.0f, 0.95f, 1.0f, 1.0f));

        // Oxygen Bar
        rt.DrawRectangle(new Rect(25, 115, 200, 10), _hudBrush!, 1.0f);
        rt.FillRectangle(new Rect(26, 116, 1.98f * _breathOxygen, 8), _isHoldingBreath ? _greenBrush! : _hudBrush!);

        // Bottom Controls Guide (Korean)
        rt.FillRectangle(new Rect(15, 660, 1250, 45), _panelBrush!);
        rt.DrawRectangle(new Rect(15, 660, 1250, 45), _hudBrush!, 1.0f);
        string guide = "[A/D]: 좌우 이동  |  [W/Space]: 점프  |  [S/Ctrl]: 엎드리기  |  [우클릭 유지]: 저격 스코프 시야 확장  |  [Shift]: 호흡 참기  |  [좌클릭]: 발사";
        _smallFont?.DrawText(guide, new Rect(30, 675, 1200, 25), new Color4(0.8f, 0.95f, 1.0f, 1.0f));
    }

    public void Dispose()
    {
        _hudFont?.Dispose();
        _titleFont?.Dispose();
        _smallFont?.Dispose();
        _skyBrush?.Dispose();
        _groundBrush?.Dispose();
        _grassBrush?.Dispose();
        _playerBrush?.Dispose();
        _enemyBrush?.Dispose();
        _towerBrush?.Dispose();
        _bulletBrush?.Dispose();
        _laserBrush?.Dispose();
        _hudBrush?.Dispose();
        _panelBrush?.Dispose();
        _greenBrush?.Dispose();
        _redBrush?.Dispose();
    }
}
