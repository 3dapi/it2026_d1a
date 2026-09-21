using Vortice.Mathematics;

namespace PlaneSurvivor
{
    /// <summary>
    /// 플레이어 총알. 텍스처는 GameMain에서 한 번만 로드해서 공유로 받아씀
    /// (총알마다 텍스처를 새로 로드하면 낭비이므로).
    /// </summary>
    public class Bullet
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; } = 14f;
        public float Height { get; } = 32f;
        public float Speed { get; set; } = 500f;
        public bool Active { get; private set; }

        private readonly G2Texture _texture;

        internal Bullet(G2Texture sharedTexture)
        {
            _texture = sharedTexture;
        }

        /// <summary>(x, y) 위치에서 발사 시작. 풀에서 꺼내 쓸 때 호출.</summary>
        public void Fire(float x, float y)
        {
            X = x - Width / 2f;
            Y = y - Height;
            Active = true;
        }

        public void Update(float dt)
        {
            if (!Active) return;

            Y -= Speed * dt; // 정면(위쪽)으로 이동

            if (Y + Height < 0)
            {
                Active = false; // 화면 밖으로 나가면 비활성화 (풀로 반납되는 효과)
            }
        }

        public void Render()
        {
            if (!Active) return;
            _texture.Draw(X, Y);
        }

        public Rect GetBounds() => new Rect(X, Y, Width, Height);

        public void Deactivate() => Active = false;
    }
}