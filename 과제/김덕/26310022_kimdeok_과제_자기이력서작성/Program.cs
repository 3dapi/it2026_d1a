namespace _26310022_kimdeok_과제_자기이력서작성
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "객체지향프로그래밍Ⅰ - 과제1 [자기이력서]";

            int width = 74;
            int startX = 4;
            int currentY = 2;

            try
            {
                if (Console.WindowHeight < 40)
                {
                    Console.SetWindowSize(Math.Max(Console.WindowWidth, 84), 42);
                }
            }
            catch { }

            // 1. 헤더 테두리
            DrawBox(startX, currentY, width, 3, ConsoleColor.Cyan);
            Console.SetCursorPosition(startX + 28, currentY + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("★ 자 기 이 력 서 ★");
            currentY += 4;

            // 2. 인적 사항 섹션
            DrawSectionHeader(startX, ref currentY, width, "1. 기본 인적 사항", ConsoleColor.Green);
            DrawRow(startX, ref currentY, "성      명", "김덕", "학      번", "26310022");
            DrawRow(startX, ref currentY, "생 년 월 일", "2007.01.11 (만 19세)", "성      별", "남성");
            DrawRow(startX, ref currentY, "소      속", "한국IT직업전문학교", "학      과", "게임학과 (기획 전과)");
            DrawRow(startX, ref currentY, "거 주 형 태", "고시원 자취 중", "과  목  명", "객체지향프로그래밍Ⅰ");
            currentY++;

            // 3. 학력 사항 섹션
            DrawSectionHeader(startX, ref currentY, width, "2. 학력 사항", ConsoleColor.Green);
            DrawTableLine(startX, ref currentY, "기 간", "학 교 및 과 정 명", "구 분", ConsoleColor.DarkYellow);
            DrawTableLine(startX, ref currentY, "2023 ~ 2026", "제주남녕고등학교", "졸업", ConsoleColor.White);
            DrawTableLine(startX, ref currentY, "2026 ~ 재학", "한국IT직업전문학교 게임학과", "재학 (최종 고졸)", ConsoleColor.White);
            currentY++;

            // 4. 프로젝트 및 실무/알바 경험
            DrawSectionHeader(startX, ref currentY, width, "3. 프로젝트 및 활동 경험", ConsoleColor.Green);
            DrawBullet(startX, ref currentY, "게임 프로젝트: [백드래프트 (Backdraft)] 개발 참여 (중단/파기)");
            DrawBullet(startX, ref currentY, "아르바이트 경험: BHC 치킨 매장 근무 (약 5개월 근무)");
            currentY++;

            // 5. 보유 자격증 및 기술
            DrawSectionHeader(startX, ref currentY, width, "4. 자격증 및 기술 스택", ConsoleColor.Green);
            DrawBullet(startX, ref currentY, "자격증: GTQ (그래픽기술자격) 1급 취득 (2024.11.15)");
            DrawBullet(startX, ref currentY, "기술 스택: C# / .NET 객체지향 기초, 2D 게임 기획 및 시뮬레이션");
            currentY++;

            // 6. 특이사항 및 각오
            DrawSectionHeader(startX, ref currentY, width, "5. 특이사항 및 수업 각오", ConsoleColor.Green);
            DrawBullet(startX, ref currentY, "특이사항: 현재 고시원에서 자취하며 학업에 집중하고 있음");
            DrawBullet(startX, ref currentY, "수업목표: C# OOP 설계를 탄탄히 다져 4주차 게임 프로젝트 '오퍼레이션 아포리아' 완성");
            currentY += 2;

            // 바닥선
            Console.SetCursorPosition(startX, currentY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('=', width));
            Console.ResetColor();

            Console.SetCursorPosition(startX, currentY + 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 아무 키나 누르면 종료됩니다...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static void DrawBox(int x, int y, int width, int height, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write("┌" + new string('─', width - 2) + "┐");
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│" + new string(' ', width - 2) + "│");
            }
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("└" + new string('─', width - 2) + "┘");
            Console.ResetColor();
        }

        static void DrawSectionHeader(int x, ref int y, int width, string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"[ {title} ]");
            y++;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('-', width));
            y++;
            Console.ResetColor();
        }

        static void DrawRow(int x, ref int y, string label1, string val1, string label2, string val2)
        {
            Console.SetCursorPosition(x + 2, y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"• {label1} : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x + 18, y);
            Console.Write(val1);

            Console.SetCursorPosition(x + 42, y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"• {label2} : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x + 58, y);
            Console.WriteLine(val2);

            y++;
            Console.ResetColor();
        }

        static void DrawTableLine(int x, ref int y, string col1, string col2, string col3, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x + 2, y);
            Console.Write(col1);

            Console.SetCursorPosition(x + 20, y);
            Console.Write(col2);

            Console.SetCursorPosition(x + 56, y);
            Console.WriteLine(col3);

            y++;
            Console.ResetColor();
        }

        static void DrawBullet(int x, ref int y, string text)
        {
            Console.SetCursorPosition(x + 2, y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("- ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            y++;
            Console.ResetColor();
        }
    }
}
