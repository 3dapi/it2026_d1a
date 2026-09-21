using System;

namespace GameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "자기소개 프로필 - 권민기";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==================================================");
            Console.WriteLine("  [SYSTEM] 프로필을 불러오려면 아무 키나 누르세요...");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            Console.ReadKey(true);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                GAME DEVELOPER PROFILE                  │");
            Console.WriteLine("└────────────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            PrintField("이 름", "권민기", ConsoleColor.Yellow);
            PrintField("학 번", "26031005", ConsoleColor.Yellow);
            Console.WriteLine("──────────────────────────────────────────────────────────");
            PrintField("입학 동기", "넥슨게임즈 콘텐츠 기획자를 목표로 입학했습니다. 본래는 프로그래밍으로 조기입학 신청을 넣었지만, 제가 좋아하는 건 게임의 스토리였기에, 가장 가까운 기획으로 선택을 번복했습니다.", ConsoleColor.Green);
            PrintField("팀플 이력", "BLACKOUT 팀 기획자 활동 중.", ConsoleColor.Green);
            PrintField("개인 작업", "웹소설을 연재하고 있습니다. 현재 30화를 계획중이고, 노벨피아에 연재중입니다. (Vanitas-미래를 알지라도)", ConsoleColor.Green);
            Console.WriteLine("──────────────────────────────────────────────────────────");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("프로그램을 종료하려면 아무 키나 누르세요...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static void PrintField(string label, string value, ConsoleColor valueColor)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            // 한글 공백 문제를 해결하기 위해 서식 지정자(-8) 대신 직접 간격을 조정했습니다.
            Console.Write($" ■ {label} : ");

            Console.ForegroundColor = valueColor;
            Console.WriteLine(value);

            Console.ResetColor();
        }
    }
}
