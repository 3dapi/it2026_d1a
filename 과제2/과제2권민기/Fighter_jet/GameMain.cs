// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------
using Vortice.Mathematics;
using PlaneSurvivor;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    private Player? _player;

    protected override void Initialize()
    {
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------
        this.ClearColor = new Color4(0.05f, 0.05f, 0.1f, 1.0f);

        _player = new Player(
            GameGlobal.ScreenSize.Width / 2f - 32f,
            GameGlobal.ScreenSize.Height - 100f);
        _player.Initialize();

        _player.OnFireBullet += (x, y) =>
        {
            // TODO: Bullet 생성 로직 (Bullet 클래스 만들면 여기서 연결)
        };

        _player.OnDied += () =>
        {
            // TODO: 게임 오버 처리 (결과 화면 전환 등)
        };
    }

    protected override void Update()
    {
        //---------------------------------------
        // 게임 관련 객체를 갱신합니다.
        //---------------------------------------
        _player?.Update();

        // TODO: 몬스터/총알 갱신, 충돌 판정
        // if (몬스터와 _player.GetBounds() 충돌) _player?.OnHitByMonster();
    }

    protected override void Render()
    {
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------
        _player?.Render();

        // TODO: 몬스터/총알/HUD(점수, HP, 생존시간) 렌더링
    }

    public override void Dispose()
    {
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------
        _player?.Dispose();

        base.Dispose();
    }
}