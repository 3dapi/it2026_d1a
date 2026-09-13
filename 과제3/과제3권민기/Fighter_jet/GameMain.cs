// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vortice.Mathematics;
using PlaneSurvivor;

class GameMain : G2AppBase
{
    public override System.Drawing.Size ScreenSize => GameGlobal.ScreenSize;
    public override string GameName => GameGlobal.GameName;

    // ---------------------------------------
    // 게임 상태
    // ---------------------------------------
    private enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    private GameState _currentState = GameState.Start;

    private float _playTimer = 0f;
    private const float MaxPlayTime = 300f; // 5분

    private int _score = 0;
    private bool _isSurvived = false; // true면 5분 생존 성공, false면 몬스터한테 죽음

    // ---------------------------------------
    // 게임 오브젝트
    // ---------------------------------------
    private Player? _player;

    private G2Texture? _bulletTexture;
    private G2Texture? _monsterTexture;

    private const int BulletPoolSize = 40;
    private const int MonsterPoolSize = 20;

    private readonly List<Bullet> _bullets = new();
    private readonly List<Monster> _monsters = new();

    private float _spawnTimer = 0f;
    private const float SpawnInterval = 1.2f;
    private readonly Random _rng = new();

    // ---------------------------------------
    // 폰트
    // ---------------------------------------
    private G2Font? _titleFont;
    private G2Font? _guideFont;
    private G2Font? _hudFontLeft;
    private G2Font? _hudFontRight;
    private G2Font? _timerFont;
    private G2Font? _resultFont;

    protected override void Initialize()
    {
        //---------------------------------------
        // 게임 관련 객체를 생성합니다.
        //---------------------------------------
        this.ClearColor = new Color4(0.05f, 0.05f, 0.1f, 1.0f);

        _player = new Player(
            GameGlobal.ScreenSize.Width / 2f - 22.5f,
            GameGlobal.ScreenSize.Height - 100f);
        _player.Initialize();

        _player.OnFireBullet += (x, y) =>
        {
            var bullet = _bullets.Find(b => !b.Active);
            bullet?.Fire(x, y);
        };

        _player.OnDied += () =>
        {
            _isSurvived = false;
            _currentState = GameState.GameOver;
        };

        //---------------------------------------
        // 총알 / 몬스터 텍스처는 한 번만 로드해서 공유
        //---------------------------------------
        _bulletTexture = new G2Texture("resource/texture/bullet_player.png");
        _monsterTexture = new G2Texture("resource/texture/monster_01.png");

        for (int i = 0; i < BulletPoolSize; i++)
        {
            _bullets.Add(new Bullet(_bulletTexture));
        }

        for (int i = 0; i < MonsterPoolSize; i++)
        {
            _monsters.Add(new Monster(_monsterTexture));
        }

        //---------------------------------------
        // 폰트 생성 (한 번만)
        //---------------------------------------
        _titleFont = new G2Font("Consolas", 48f,
            Vortice.DirectWrite.FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Center,
            Vortice.DirectWrite.ParagraphAlignment.Center);

        _guideFont = new G2Font("Consolas", 20f,
            Vortice.DirectWrite.FontWeight.Normal,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Center,
            Vortice.DirectWrite.ParagraphAlignment.Center);

        _hudFontLeft = new G2Font("Consolas", 20f,
            Vortice.DirectWrite.FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Leading,
            Vortice.DirectWrite.ParagraphAlignment.Near);

        _hudFontRight = new G2Font("Consolas", 20f,
            Vortice.DirectWrite.FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Trailing,
            Vortice.DirectWrite.ParagraphAlignment.Near);

        _timerFont = new G2Font("Consolas", 20f,
            Vortice.DirectWrite.FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Center,
            Vortice.DirectWrite.ParagraphAlignment.Near);

        _resultFont = new G2Font("Consolas", 32f,
            Vortice.DirectWrite.FontWeight.Bold,
            Vortice.DirectWrite.FontStyle.Normal,
            Vortice.DirectWrite.TextAlignment.Center,
            Vortice.DirectWrite.ParagraphAlignment.Center);
    }

    protected override void Update()
    {
        switch (_currentState)
        {
            case GameState.Start:
                UpdateStart();
                break;
            case GameState.Playing:
                UpdatePlaying();
                break;
            case GameState.GameOver:
                UpdateGameOver();
                break;
        }
    }

    private void UpdateStart()
    {
        if (G2AppBase.Instance.Input.IsKeyDown(Keys.Space))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _player?.Reset(
            GameGlobal.ScreenSize.Width / 2f - 22.5f,
            GameGlobal.ScreenSize.Height - 100f);

        _score = 0;
        _playTimer = 0f;
        _spawnTimer = 0f;
        _isSurvived = false;

        foreach (var bullet in _bullets) bullet.Deactivate();
        foreach (var monster in _monsters) monster.Kill();

        _currentState = GameState.Playing;
    }

    private void UpdatePlaying()
    {
        float dt = (float)G2AppBase.Instance.DeltaTime;
        _playTimer += dt;

        if (_playTimer >= MaxPlayTime)
        {
            _isSurvived = true;
            _currentState = GameState.GameOver;
            return;
        }

        //---------------------------------------
        // 게임 관련 객체를 갱신합니다.
        //---------------------------------------
        _player?.Update();

        UpdateMonsterSpawn(dt);

        foreach (var bullet in _bullets) bullet.Update(dt);
        foreach (var monster in _monsters) monster.Update(dt);

        HandleCollisions();
    }

    private void UpdateMonsterSpawn(float dt)
    {
        _spawnTimer -= dt;
        if (_spawnTimer > 0f) return;

        _spawnTimer = SpawnInterval;

        var monster = _monsters.Find(m => !m.Active);
        if (monster == null) return; // 풀이 다 차있으면 이번엔 스킵

        float spawnX = _rng.Next(0, GameGlobal.ScreenSize.Width - 60);
        float speed = _rng.Next(80, 160);
        monster.Spawn(spawnX, -40f, speed);
    }

    private void HandleCollisions()
    {
        // 총알 vs 몬스터
        foreach (var bullet in _bullets)
        {
            if (!bullet.Active) continue;

            foreach (var monster in _monsters)
            {
                if (!monster.Active) continue;

                if (Intersects(bullet.GetBounds(), monster.GetBounds()))
                {
                    bullet.Deactivate();
                    if (monster.TakeDamage(1))
                    {
                        _score += 100; // 몬스터 처치 점수
                    }
                    break; // 이 총알은 이미 소모됐으니 다음 총알로
                }
            }
        }

        // 플레이어 vs 몬스터
        if (_player == null) return;

        foreach (var monster in _monsters)
        {
            if (!monster.Active) continue;

            if (Intersects(_player.GetBounds(), monster.GetBounds()))
            {
                _player.OnHitByMonster();
                monster.Kill();
            }
        }
    }

    private static bool Intersects(Rect a, Rect b)
    {
        return a.X < b.X + b.Width &&
               a.X + a.Width > b.X &&
               a.Y < b.Y + b.Height &&
               a.Y + a.Height > b.Y;
    }

    private void UpdateGameOver()
    {
        // TODO: 필요하면 재시작 입력(예: 스페이스바) 받아서 다시 StartGame() 호출
    }

    protected override void Render()
    {
        switch (_currentState)
        {
            case GameState.Start:
                RenderStart();
                break;
            case GameState.Playing:
                RenderPlaying();
                break;
            case GameState.GameOver:
                RenderGameOver();
                break;
        }
    }

    private void RenderStart()
    {
        _titleFont?.DrawText("Fighter Jet",
            new Rect(0, 250, GameGlobal.ScreenSize.Width, 80),
            new Color4(1f, 1f, 1f, 1f));

        _guideFont?.DrawText("스페이스바를 눌러 시작하세요!",
            new Rect(0, 450, GameGlobal.ScreenSize.Width, 40),
            new Color4(1f, 1f, 1f, 1f));
    }

    private void RenderPlaying()
    {
        //---------------------------------------
        // 게임 관련 객체를 렌더링 합니다.
        //---------------------------------------
        foreach (var monster in _monsters) monster.Render();
        foreach (var bullet in _bullets) bullet.Render();
        _player?.Render();

        _hudFontLeft?.DrawText($"HP: {_player?.CurrentHP}",
            new Rect(10, 10, 150, 30),
            new Color4(1f, 1f, 1f, 1f));

        _hudFontRight?.DrawText($"SCORE: {_score}",
            new Rect(GameGlobal.ScreenSize.Width - 160, 10, 150, 30),
            new Color4(1f, 1f, 1f, 1f));

        float remaining = Math.Max(0f, MaxPlayTime - _playTimer);
        int minutes = (int)remaining / 60;
        int seconds = (int)remaining % 60;

        _timerFont?.DrawText($"{minutes:00}:{seconds:00}",
            new Rect(0, 10, GameGlobal.ScreenSize.Width, 30),
            new Color4(1f, 1f, 1f, 1f));
    }

    private void RenderGameOver()
    {
        string resultText = _isSurvived ? "생존 성공!" : "GAME OVER";
        Color4 resultColor = _isSurvived
            ? new Color4(0.3f, 1f, 0.4f, 1f)  // 초록색
            : new Color4(1f, 0.3f, 0.3f, 1f); // 빨간색

        _resultFont?.DrawText(resultText,
            new Rect(0, 400, GameGlobal.ScreenSize.Width, 60),
            resultColor);

        _resultFont?.DrawText($"SCORE: {_score}",
            new Rect(0, 470, GameGlobal.ScreenSize.Width, 60),
            new Color4(1f, 1f, 1f, 1f));
    }

    public override void Dispose()
    {
        //---------------------------------------
        // 게임 관련 객체를 해제합니다.
        //---------------------------------------
        _player?.Dispose();

        _bulletTexture?.Dispose();
        _monsterTexture?.Dispose();

        _titleFont?.Dispose();
        _guideFont?.Dispose();
        _hudFontLeft?.Dispose();
        _hudFontRight?.Dispose();
        _timerFont?.Dispose();
        _resultFont?.Dispose();

        base.Dispose();
    }
}