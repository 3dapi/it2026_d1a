using System;
using System.Text;
using System.Threading;

namespace ResumeVisualNovel
{
    internal class Program
    {
        // ==========================================
        // 프로그램 시작
        // ==========================================

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "조예인 - 자기소개 Visual Novel";

            StartGame();
        }

        // ==========================================
        // 공통 출력
        // ==========================================

        static void TypeText(string text, int delay = 10)
        {
            foreach (char c in text)
            {
                Console.Write(c);

                if (delay > 0)
                {
                    Thread.Sleep(delay);
                }
            }

            Console.WriteLine();
        }

        static void Narration(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            TypeText(text);

            Console.ResetColor();
        }

        static void Character(string name, string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("[" + name + "] ");

            Console.ResetColor();

            TypeText(text);
        }

        static void Pause()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("아무 키나 누르면 계속합니다.");
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        static void Section(string title)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("              " + title);
            Console.WriteLine("========================================");

            Console.ResetColor();

            Console.WriteLine();
        }

        // ==========================================
        // 선택지
        // ==========================================

        static int Choice(string question, string[] options)
        {
            Console.WriteLine(question);
            Console.WriteLine();

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(
                    "  [" + (i + 1) + "] " + options[i]);
            }

            Console.WriteLine();

            while (true)
            {
                Console.Write("선택 > ");

                // Console.ReadLine()의 null 가능성 제거
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int number))
                {
                    if (number >= 1 && number <= options.Length)
                    {
                        return number;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }
        }

        // ==========================================
        // START
        // ==========================================

        static void StartGame()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            Console.WriteLine("########################################");
            Console.WriteLine("#                                      #");
            Console.WriteLine("#      CHO YEIN : PLAYABLE LIFE        #");
            Console.WriteLine("#                                      #");
            Console.WriteLine("#          자기소개 VISUAL NOVEL       #");
            Console.WriteLine("#                                      #");
            Console.WriteLine("########################################");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine(
                "      「아티스트라도 게임 기획이 하고 싶어!」");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("              PRESS ANY KEY");
            Console.ResetColor();

            Console.ReadKey(true);

            Prologue();
        }

        // ==========================================
        // PROLOGUE
        // ==========================================

        static void Prologue()
        {
            Section("PROLOGUE");

            Character(
                "조예인",
                "안녕하세요. 2학기 편입생, 조예인입니다.");

            Character(
                "조예인",
                "사실 저는 최적화와 관련된 게 아니라면, 컴퓨터 공학에 대한 지식은 전혀 없어요.");

            Character(
                "조예인",
                "AI가 없었다면 지금쯤 어떻게 됐을지….");

            Console.WriteLine();

            Character(
                "조예인",
                "..그런데 여기는 어디지?");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            TypeText("「날 내보내 줘!」", 30);

            Console.ResetColor();

            Console.WriteLine();

            Narration("날뛰고 있군요.");
            Narration(
                "이곳이 코드 안이란 걸 자각하기 전에 어서 그에 대해 알아봅시다!");

            Pause();

            MainMenu();
        }

        // ==========================================
        // MAIN MENU
        // ==========================================

        static void MainMenu()
        {
            while (true)
            {
                Section("PROFILE");

                int choice = Choice(
                    "조예인에 대해 무엇을 알아볼까요?",
                    new string[]
                    {
                        "PROFILE      - 기본 정보",
                        "PERSONALITY  - 성격",
                        "HISTORY      - 연대기",
                        "SKILL        - 역량",
                        "GAME         - 게임",
                        "LIFE         - 취미와 일상",
                        "ENDING       - 마무리"
                    });

                switch (choice)
                {
                    case 1:
                        Profile();
                        break;

                    case 2:
                        Personality();
                        break;

                    case 3:
                        History();
                        break;

                    case 4:
                        Skill();
                        break;

                    case 5:
                        GameProfile();
                        break;

                    case 6:
                        Life();
                        break;

                    case 7:
                        Ending();
                        return;
                }
            }
        }

        // ==========================================
        // PROFILE
        // ==========================================

        static void Profile()
        {
            Section("PROFILE");

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("                 조 예 인");

            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine("  AGE       : 만 27세");
            Console.WriteLine("  BIRTHDAY  : 2월 26일");
            Console.WriteLine("  MBTI      : ENFP");
            Console.WriteLine("  MOTTO     : 재밌게 살자.");

            Console.WriteLine();

            Narration(
                "나름 시네필이며 서브컬쳐 전반에 대한 관심과 이해도가 높습니다.");

            Pause();
        }

        // ==========================================
        // PERSONALITY
        // ==========================================

        static void Personality()
        {
            Section("PERSONALITY");

            Narration("새로운 자극을 좋아합니다.");
            Narration(
                "익숙한 방식을 반복하기보다는 변화와 도전을 선택하는 편.");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[ 강점 ]");
            Console.ResetColor();

            Console.WriteLine(
                "  * 다양한 요소를 분석하고 통합하는 능력");

            Console.WriteLine(
                "  * 구조를 파악하고 재구성하는 능력");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ 약점 ]");
            Console.ResetColor();

            Console.WriteLine(
                "  * 다중 과제를 동시에 처리하는 멀티태스킹");

            Console.WriteLine(
                "  * 숫자 중심의 직무");

            Console.WriteLine(
                "  * 성과가 정체될 때 기존 작업을 꾸준히 이어가는 것");

            Pause();
        }

        // ==========================================
        // HISTORY
        // ==========================================

        static void History()
        {
            Section("HISTORY");

            Narration("이곳저곳 참 많이도 쏘다녔습니다.");
            Narration("좋아하는 것을 따라가다 보니 다음 관심사가 생겼고,");
            Narration("그 관심사가 다시 새로운 분야로 이어졌습니다.");

            Pause();

            Section("HISTORY - 01");

            Character(
                "어린 시절",
                "초등학교 때부터 순수 미술을 시작했습니다.");

            Character(
                "어린 시절",
                "처음 꿈꿨던 직업은 출판 만화가였고");

            Character(
                "어린 시절",
                "이후 영상 매체에 매료되면서 2D 애니메이터를 꿈꾸게 되었습니다.");

            Pause();

            Section("HISTORY - 02");

            Character(
                "유학",
                "시대에 발맞춰 3D 애니메이션 기술을 배우기 위해 호주로 유학을 떠났습니다.");

            Character(
                "유학",
                "그곳에서 접한 대자연은 또 다른 관심사가 됐고");

            Character(
                "유학",
                "해양 과학, 특히 심해 생물 - 요각류에 푹 빠져 지냈습니다.");

            Pause();

            Section("HISTORY - 03");

            Character(
                "과도기",
                "하지만 결국 연구자나 프리랜서로서의 삶을 이어가기보다는, 게임 산업의 전문가로 성장하기로 결심했습니다.");

            Character(
                "과도기",
                "TA, Technical Artist 분야에도 도전했습니다.");

            Character(
                "과도기",
                "아트와 기술이 만나는 지점에 흥미를 느꼈습니다.");

            Pause();

            Section("HISTORY - 04");

            Character(
                "현재",
                "그러던 중 게임 기획자라는 진로를 추천받았습니다.");

            Character(
                "현재",
                "그리고 지금의 대학에 입학했습니다.");

            Console.WriteLine();

            Narration(
                "돌아보면 진로가 계속 바뀐 것처럼 보이네요.");

            Narration(
                "하지만 분명히 존재하는 공통점은");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            TypeText(
                "「뭐든지 관찰하고, 구조를 이해하고, 새롭게 재구성하는 것. 이것이 제 둘도 없는 강점이라 여깁니다.」",
                15);

            Console.ResetColor();

            Pause();
        }

        // ==========================================
        // SKILL
        // ==========================================

        static void Skill()
        {
            Section("SKILL");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ EDUCATION ]");
            Console.ResetColor();

            Console.WriteLine("  * 산업 애니메이션");
            Console.WriteLine("  * 영화 복수 전공");
            Console.WriteLine("  * 3D 애니메이션");
            Console.WriteLine("  * 해양 과학");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ SOFTWARE ]");
            Console.ResetColor();

            Console.WriteLine("  * Blender");
            Console.WriteLine("  * After Effects");
            Console.WriteLine("  * Unreal Engine");
            Console.WriteLine("  * Clip Studio");
            Console.WriteLine("  * Photoshop");
            Console.WriteLine("  * Premiere Pro");
            Console.WriteLine("  * ZBrush");
            Console.WriteLine("  * etc.");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ PRODUCTION ]");
            Console.ResetColor();

            Console.WriteLine("  * 드로잉");
            Console.WriteLine("  * 캐릭터 / 배경 일러스트");
            Console.WriteLine("  * 컨셉 아트");
            Console.WriteLine("    - 캐릭터");
            Console.WriteLine("    - 배경");
            Console.WriteLine("    - 크리처");
            Console.WriteLine("    - 소품");
            Console.WriteLine("  * 픽셀 아트");
            Console.WriteLine("  * 웹툰 / 만화");
            Console.WriteLine("  * 시나리오 집필");
            Console.WriteLine("  * 스토리보드 작성");
            Console.WriteLine("  * 영상 편집");
            Console.WriteLine("  * 콘텐츠 기획");
            Console.WriteLine("  * and much more….");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ LANGUAGE ]");
            Console.ResetColor();

            Console.WriteLine("  * 영어");
            Console.WriteLine("    - 아카데미 / 비즈니스");
            Console.WriteLine("    - 프레젠테이션 / 프리토킹 가능");
            Console.WriteLine("  * 일본어");
            Console.WriteLine("    - 기초 회화");

            Pause();
        }

        // ==========================================
        // GAME
        // ==========================================

        static void GameProfile()
        {
            Section("GAME");

            Character(
                "첫 게임",
                "제가 처음으로 깊게 몰입했던 게임은 《레프트 4 데드 2》입니다.");

            Character(
                "첫 게임",
                "친구와 PC방에서 10시간 동안 전 캠페인을 끝까지 플레이했어요.");

            Pause();

            Section("GAME - PLAYING NOW");

            Console.WriteLine("현재 플레이 중인 게임");
            Console.WriteLine();

            string[] games =
            {
                "에이리언: 아이솔레이션",
                "어쌔신 크리드 오디세이",
                "바이오쇼크 리마스터",
                "다키스트 던전",
                "데드 스페이스 2",
                "파이널 판타지 7",
                "파이널 판타지 9",
                "파이널 판타지 12",
                "파이널 판타지 14"
            };

            for (int i = 0; i < games.Length; i++)
            {
                Console.WriteLine("  > " + games[i]);
            }

            Pause();

            Section("GAME - TASTE");

            Console.WriteLine("좋아하는 장르");
            Console.WriteLine();

            Console.WriteLine("  호러");
            Console.WriteLine("  미스터리");
            Console.WriteLine("  SF");
            Console.WriteLine("  역사");

            Console.WriteLine();

            Console.WriteLine("기대하는 신작");
            Console.WriteLine("  > 콜 오브 듀티: 모던 워페어 4");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("가장 실망했던 게임");
            Console.ResetColor();

            Console.WriteLine("  > 사일런트 힐 f");

            Pause();

            Section("GAME - LORE");

            Narration(
                "게임을 플레이하는 것만큼 좋아하는 것이 있습니다.");

            Narration(
                "바로 게임 속 세계를 조사하는 것입니다.");

            Console.WriteLine();

            Character(
                "조예인",
                "세계관 설정, 게임 로어, 숨겨진 이야기 같은 것을 찾아보는 걸 좋아합니다.");

            Character(
                "조예인",
                "작품 하나를 보면 '왜 이렇게 만들었을까?'라는 생각을 자주 합니다.");

            Character(
                "조예인",
                "그래서 플레이어가 무엇을 보고, 무엇을 느끼고,");

            Character(
                "조예인",
                "어떤 정보를 발견하게 될지를 생각하는 일에도 관심이 많습니다.");

            Character(
                "조예인",
                "결국 제가 기획에서 가장 중요하게 여기는 건 플레이어 경험이라고 할 수 있겠네요.");

            Pause();
        }

        // ==========================================
        // LIFE
        // ==========================================

        static void Life()
        {
            Section("LIFE");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 취미 ]");
            Console.ResetColor();

            Console.WriteLine("  * 스몰 토크");
            Console.WriteLine("  * 정리정돈");
            Console.WriteLine("  * 흥미로운 작품 찾고 분석하기");
            Console.WriteLine("  * 괴담 읽고 듣기");
            Console.WriteLine("  * 창작");
            Console.WriteLine("  * TRPG");
            Console.WriteLine("  * 새로운 것 시도 - 신메뉴, 산책로");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 즐기는 스포츠 ]");
            Console.ResetColor();

            Console.WriteLine("  * 로드 바이크 라이딩");
            Console.WriteLine("  * 수영");
            Console.WriteLine("  * 사격");

            Console.WriteLine();

            Character(
                "조예인",
                "하고 싶은 건 넘쳐나는데 시간은 늘 부족하죠.");

            Pause();
        }

        // ==========================================
        // ENDING
        // ==========================================

        static void Ending()
        {
            Section("ENDING");

            Narration(
                "10년 이상 그래픽 전공자로서의 실력을 키워왔습니다.");

            Narration(
                "다른 나라로 떠났고, 그곳에서 심해에 관심을 갖기도 했습니다.");

            Narration(
                "TA라는 새로운 영역에도 도전했습니다.");

            Console.WriteLine();

            Narration(
                "그리고 지금은 더 물러날 곳이 없는 것처럼 게임 기획을 공부하고 있습니다.");

            Pause();

            Section("ENDING - THE NEXT STAGE");

            Character(
                "조예인",
                "새로운 것을 발견하고,");

            Character(
                "조예인",
                "그것을 제가 가진 경험과 연결하고,");

            Character(
                "조예인",
                "결국 누군가가 직접 경험할 수 있는 형태로 만드는 것.");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            TypeText(
                "그것이 제가 하고 싶은 일입니다.",
                20);

            Console.WriteLine();

            TypeText(
                "그리고 가능하다면,",
                20);

            TypeText(
                "제가 만든 컨텐츠를 누군가가",
                20);

            TypeText(
                "10시간이 넘게 붙잡고 있을 정도로 즐겨줬으면 하네요.",
                15);

            Console.WriteLine();

            TypeText(
                "재미는 중요하니까요!",
                25);

            Console.ResetColor();

            Pause();

            Section("THE END");

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            Console.WriteLine("        +---------------------+");
            Console.WriteLine("        |                     |");
            Console.WriteLine("        |      조  예  인     |");
            Console.WriteLine("        |                     |");
            Console.WriteLine("        |    LIFE ENJOYER     |");
            Console.WriteLine("        |                     |");
            Console.WriteLine("        |   「목표는 졸업!」  |");
            Console.WriteLine("        |                     |");
            Console.WriteLine("        +---------------------+");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("프로그램을 종료하려면 아무 키나 누르세요.");

            Console.ReadKey(true);
        }
    }
}
