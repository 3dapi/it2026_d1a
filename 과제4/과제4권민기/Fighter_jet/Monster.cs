using Vortice.Mathematics;

namespace PlaneSurvivor
{
    /// <summary>
    /// 몬스터(적) 객체. 텍스처는 GameMain에서 한 번만 로드해서 공유로 받아씀.
    /// 총알 3대 맞으면 사망, 플레이어와 닿으면 플레이어 HP 10 감소.
    /// </summary>
    public class Monster
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; } = 60f;
        public float Height { get; } = 40f;
        public float Speed { get; set; } = 120f;

        public const int MaxHP = 3;
        public int CurrentHP { get; private set; }
        public bool Active { get; private set; }

        private readonly G2Texture _texture;

        internal Monster(G2Texture sharedTexture)
        {
            _texture = sharedTexture;
        }

        /// <summary>(x, y) 위치에서 스폰. 풀에서 꺼내 쓸 때 호출.</summary>
        public void Spawn(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            CurrentHP = MaxHP;
            Active = true;
        }

        public void Update(float dt)
        {
            if (!Active) return;

            Y += Speed * dt; // 아래(플레이어 방향)로 이동

            if (Y > GameGlobal.ScreenSize.Height)
            {
                Active = false; // 화면 밖으로 나가면 비활성화
            }
        }

        /// <summary>총알에 맞았을 때 호출. 이번 타격으로 죽었으면 true 반환.</summary>
        public bool TakeDamage(int damage)
        {
            if (!Active) return false;

            CurrentHP -= damage;
            if (CurrentHP <= 0)
            {
                Active = false;
                return true;
            }
            return false;
        }

        /// <summary>플레이어와 충돌했을 때 즉시 제거하고 싶을 때 호출.</summary>
        public void Kill()
        {
            Active = false;
        }

        public void Render()
        {
            if (!Active) return;
            _texture.Draw(X, Y);
        }

        public Rect GetBounds() => new Rect(X, Y, Width, Height);
    }
}