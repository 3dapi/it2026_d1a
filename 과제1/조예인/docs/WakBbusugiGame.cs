using System;

namespace WakBbusugi
{
    internal class Program
    {
        // ==========================================
        // 게임 상태
        // ==========================================

        enum GameState
        {
            Title,
            Playing,
            Menu,
            GameOver,
            Clear,
            Exit
        }

        static GameState currentState = GameState.Title;

        // ==========================================
        // 게임 데이터
        // ==========================================

        static int score = 0;
        static int combo = 0;

        static int totalTiles = 64;
        static int remainingTiles = 64;

        static int targetTiles = 8;
        static int hitTargets = 0;

        // ==========================================
        // 프로그램 시작
        // ==========================================

        static void Main()
        {
            Console.OutputEncoding =
                System.Text.Encoding.UTF8;

            Console.Title = "왁뿌수기";

            RunGame();
        }

        // ==========================================
        // 게임 루프
        // ==========================================

        static void RunGame()
        {
            while (currentState != GameState.Exit)
            {
                Console.Clear();

                switch (currentState)
                {
                    case GameState.Title:
                        ShowTitleScreen();
                        break;

                    case GameState.Playing:
                        StartStage();
                        break;

                    case GameState.Menu:
                        ShowMenu();
                        break;

                    case GameState.GameOver:
                        ShowGameOver();
                        break;

                    case GameState.Clear:
                        ShowClear();
                        break;
                }
            }
        }

        // ==========================================
        // 타이틀 화면
        // ==========================================

        static void ShowTitleScreen()
        {
            Console.WriteLine();
            Console.WriteLine(
                "╔══════════════════════════════════════════════════════╗");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                    왁  뿌  수  기                    ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║              ── ASMR TILE BREAKING ──                ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                    [1] 게임 시작                     ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                    [2] 설정                          ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                    [3] 게임 종료                     ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "║             마우스 좌클릭 : 타일 부수기              ║");

            Console.WriteLine(
                "║                    ESC : 메뉴                        ║");

            Console.WriteLine(
                "║                                                      ║");

            Console.WriteLine(
                "╚══════════════════════════════════════════════════════╝");

            Console.WriteLine();
            Console.Write("선택 > ");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    currentState = GameState.Playing;
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    ShowSettings();
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    currentState = GameState.Exit;
                    break;
            }
        }

        // ==========================================
        // 스테이지 시작
        // ==========================================

        static void StartStage()
        {
            score = 0;
            combo = 0;

            totalTiles = 64;
            remainingTiles = totalTiles;

            targetTiles = 8;
            hitTargets = 0;

            GenerateField();

            currentState = GameState.Playing;

            PlayStage();
        }

        // ==========================================
        // 필드 생성
        // ==========================================

        static void GenerateField()
        {
            Console.WriteLine("필드를 생성하고 있습니다...");
            Console.WriteLine();

            // 실제 구현에서는 여기에서
            // 타일과 목표 타일의 위치를
            // 랜덤으로 생성한다.
        }

        // ==========================================
        // 게임 플레이
        // ==========================================

        static void PlayStage()
        {
            while (true)
            {
                Console.Clear();

                DrawGameUI();

                Console.WriteLine();
                Console.WriteLine("타일을 선택하세요.");
                Console.WriteLine();
                Console.WriteLine("[1] 타일 부수기");
                Console.WriteLine("[ESC] 메뉴");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    currentState = GameState.Menu;
                    return;
                }

                if (key.Key == ConsoleKey.D1 ||
                    key.Key == ConsoleKey.NumPad1)
                {
                    BreakTile();
                }

                // 모든 일반 타일 파괴
                if (remainingTiles <= 0)
                {
                    currentState = GameState.Clear;
                    return;
                }

                // 목표 타일을 너무 많이 파괴
                if (hitTargets >= 3)
                {
                    currentState = GameState.GameOver;
                    return;
                }
            }
        }

        // ==========================================
        // 게임 화면
        // ==========================================

        static void DrawGameUI()
        {
            Console.WriteLine(
                "╔══════════════════════════════════════════════════════╗");

            Console.WriteLine(
                $"║ 점수 : {score,-10} 남은 타일 : {remainingTiles,-8}      ║");

            Console.WriteLine(
                $"║ 목표 타일 : {targetTiles,-5}   콤보 : {combo,-8}          ║");

            Console.WriteLine(
                "╠══════════════════════════════════════════════════════╣");

            Console.WriteLine();

            DrawField();

            Console.WriteLine();

            Console.WriteLine(
                "╚══════════════════════════════════════════════════════╝");
        }

        // ==========================================
        // 필드 표시
        // ==========================================

        static void DrawField()
        {
            const int width = 8;
            const int height = 8;

            for (int y = 0; y < height; y++)
            {
                Console.Write("        ");

                for (int x = 0; x < width; x++)
                {
                    Console.Write("[■]");
                }

                Console.WriteLine();
            }
        }

        // ==========================================
        // 타일 부수기
        // ==========================================

        static void BreakTile()
        {
            Random random = new Random();

            bool targetHit =
                random.Next(0, 10) == 0;

            if (targetHit)
            {
                // 목표 타일
                hitTargets++;

                combo = 0;

                score -= 100;

                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("       목표 타일!");
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!");

                Console.Beep(200, 300);

                Console.WriteLine();
                Console.WriteLine("아무 키나 누르세요.");
                Console.ReadKey(true);
            }
            else
            {
                // 일반 타일
                remainingTiles--;

                combo++;

                score += 100 + combo * 10;

                Console.WriteLine();
                Console.WriteLine("★ 타일 파괴!");

                Console.Beep(600, 80);

                Console.WriteLine();
                Console.WriteLine("아무 키나 누르세요.");
                Console.ReadKey(true);
            }
        }

        // ==========================================
        // 메뉴
        // ==========================================

        static void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(
                "╔════════════════════════════════════╗");

            Console.WriteLine(
                "║              메뉴                  ║");

            Console.WriteLine(
                "╠════════════════════════════════════╣");

            Console.WriteLine(
                "║                                    ║");

            Console.WriteLine(
                "║        [1] 게임 재시작             ║");

            Console.WriteLine(
                "║        [2] 사운드 설정             ║");

            Console.WriteLine(
                "║        [3] 메인 메뉴               ║");

            Console.WriteLine(
                "║        [4] 게임 종료               ║");

            Console.WriteLine(
                "║                                    ║");

            Console.WriteLine(
                "╚════════════════════════════════════╝");

            Console.WriteLine();

            Console.Write("선택 > ");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    currentState = GameState.Playing;
                    break;

                case ConsoleKey.D2:
                    ShowSettings();
                    break;

                case ConsoleKey.D3:
                    currentState = GameState.Title;
                    break;

                case ConsoleKey.D4:
                    currentState = GameState.Exit;
                    break;
            }
        }

        // ==========================================
        // 설정
        // ==========================================

        static void ShowSettings()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("========== 설정 ==========");
            Console.WriteLine();
            Console.WriteLine("사운드 설정");
            Console.WriteLine();
            Console.WriteLine("[1] 효과음 ON");
            Console.WriteLine("[2] 효과음 OFF");
            Console.WriteLine("[ESC] 뒤로");
            Console.WriteLine();

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
        }

        // ==========================================
        // 게임 오버
        // ==========================================

        static void ShowGameOver()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(
                "╔════════════════════════════════════╗");

            Console.WriteLine(
                "║            GAME OVER               ║");

            Console.WriteLine(
                "╠════════════════════════════════════╣");

            Console.WriteLine();

            Console.WriteLine(
                $"        점수 : {score}");

            Console.WriteLine(
                $"        목표 타일 : {hitTargets}");

            Console.WriteLine();

            Console.WriteLine(
                "        [1] 다시 시작");

            Console.WriteLine(
                "        [2] 타이틀로");

            Console.WriteLine();

            Console.WriteLine(
                "╚════════════════════════════════════╝");

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.D1)
            {
                currentState = GameState.Playing;
            }
            else
            {
                currentState = GameState.Title;
            }
        }

        // ==========================================
        // 클리어
        // ==========================================

        static void ShowClear()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(
                "╔════════════════════════════════════╗");

            Console.WriteLine(
                "║             STAGE CLEAR!           ║");

            Console.WriteLine(
                "╠════════════════════════════════════╣");

            Console.WriteLine();

            Console.WriteLine(
                $"        SCORE : {score}");

            Console.WriteLine(
                $"        COMBO : {combo}");

            Console.WriteLine();

            Console.WriteLine(
                "        모든 타일을 부쉈습니다!");

            Console.WriteLine();

            Console.WriteLine(
                "        [1] 다음 스테이지");

            Console.WriteLine(
                "        [2] 타이틀로");

            Console.WriteLine();

            Console.WriteLine(
                "╚════════════════════════════════════╝");

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.D1)
            {
                currentState = GameState.Playing;
            }
            else
            {
                currentState = GameState.Title;
            }
        }
    }
}