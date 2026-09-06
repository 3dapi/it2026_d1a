using System;
using System.Windows.Forms;
using Vortice.Mathematics;

namespace PlaneSurvivor
{
    /// <summary>
    /// 전투기(플레이어) 객체. glc2d 프레임워크 규칙(README 19번)에 맞춰
    /// Texture는 Initialize()에서 생성, Dispose()에서 해제.
    /// SceneMain이 소유하고 Initialize/Update/Render/Dispose를 그대로 호출해주면 됨.
    /// </summary>
    public class Player : IDisposable
    {
        // ---- 위치 / 크기 (기획서 8번: 플레이어 행동) ----
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; } = 64f;
        public float Height { get; } = 64f;
        public float Speed { get; set; } = 250f; // 초당 이동 픽셀

        // ---- HP / 무적 (기획서 4번: HP 시스템) ----
        public int MaxHP { get; } = 5;
        public int CurrentHP { get; private set; }
        public bool IsInvincible { get; private set; }
        private float _invincibleTimer;
        private const float InvincibleDuration = 1.0f;

        // ---- 자동 발사 (기획서 3번: 조작 방법) ----
        private float _fireCooldown;
        private const float FireInterval = 0.25f;

        /// <summary>총알 발사 시 (총구 x, y) 좌표와 함께 알림. SceneMain에서 구독해 Bullet 생성.</summary>
        public event Action<float, float>? OnFireBullet;
        /// <summary>HP가 0이 되는 순간 알림. SceneMain에서 구독해 게임오버 처리.</summary>
        public event Action? OnDied;

        private G2Texture? _texture;

        public Player(float startX, float startY)
        {
            X = startX;
            Y = startY;
            CurrentHP = MaxHP;
        }

        /// <summary>리소스 로드. SceneMain.Initialize()에서 한 번만 호출 (README 19번 원칙).</summary>
        public void Initialize()
        {
            // 실제 이미지 경로로 교체 필요
            _texture = new G2Texture("resource/texture/player.png");
        }

        public void Update()
        {
            var input = G2AppBase.Instance?.Input
                ?? throw new InvalidOperationException("G2AppBase instance is not initialized.");
            float dt = (float)(G2AppBase.Instance?.DeltaTime ?? 0.0);

            HandleMove(input, dt);
            HandleAutoFire(dt);
            HandleInvincibility(dt);
        }

        private void HandleMove(G2InputContext input, float dt)
        {
            float dx = 0f, dy = 0f;
            // 계속 누르고 있는 동안 이동해야 하므로 IsKeyPress 사용 (README 7.2)
            if (input.IsKeyPress(Keys.Up)) dy -= 1f;
            if (input.IsKeyPress(Keys.Down)) dy += 1f;
            if (input.IsKeyPress(Keys.Left)) dx -= 1f;
            if (input.IsKeyPress(Keys.Right)) dx += 1f;

            if (dx == 0f && dy == 0f) return;

            float len = MathF.Sqrt(dx * dx + dy * dy);
            dx /= len; dy /= len; // 대각선 이동 속도 보정

            X += dx * Speed * dt;
            Y += dy * Speed * dt;

            // 화면 경계를 벗어나지 않도록 clamp (GameGlobal.ScreenSize 기준)
            X = Math.Clamp(X, 0, GameGlobal.ScreenSize.Width - Width);
            Y = Math.Clamp(Y, 0, GameGlobal.ScreenSize.Height - Height);
        }

        private void HandleAutoFire(float dt)
        {
            _fireCooldown -= dt;
            if (_fireCooldown <= 0f)
            {
                _fireCooldown = FireInterval;
                OnFireBullet?.Invoke(X + Width / 2f, Y);
            }
        }

        private void HandleInvincibility(float dt)
        {
            if (!IsInvincible) return;
            _invincibleTimer -= dt;
            if (_invincibleTimer <= 0f) IsInvincible = false;
        }

        /// <summary>몬스터와 충돌 판정이 났을 때 SceneMain.Update()에서 호출.</summary>
        public void OnHitByMonster()
        {
            if (IsInvincible || CurrentHP <= 0) return;

            CurrentHP--;
            IsInvincible = true;
            _invincibleTimer = InvincibleDuration;

            if (CurrentHP <= 0) OnDied?.Invoke();
        }

        public void Render()
        {
            // 무적 시간 동안 깜빡이는 연출 (0.1초 간격으로 On/Off)
            if (IsInvincible && (int)(_invincibleTimer * 10) % 2 == 0) return;

            // 원본 이미지 크기 그대로 (X, Y) 위치에 출력 (G2Texture.Draw(x, y) 오버로드)
            _texture?.Draw(X, Y);
        }

        /// <summary>충돌 판정용 사각형.</summary>
        public Rect GetBounds() => new Rect(X, Y, Width, Height);

        public void Dispose()
        {
            _texture?.Dispose();
        }
    }
}