using System;
// 26031003_겜기1A_석승민
class Resume
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================");
            Console.WriteLine("          이력서");
            Console.WriteLine("================================");
            Console.WriteLine("1. 기본 정보");
            Console.WriteLine("2. 자기소개");
            Console.WriteLine("3. 나의 목표");
            Console.WriteLine("0. 프로그램 종료");
            Console.WriteLine("================================");
            Console.Write("번호를 선택하세요 : ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ShowInfo();
                    break;

                case "2":
                    Console.ForegroundColor = ConsoleColor.Green;
                    ShowIntroduction();
                    break;
          
                case "3":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ShowGoal();
                    break;

                case "0":                
                    Console.WriteLine("프로그램을 종료합니다.");
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ♥ 오희성 교수님 사랑합니다 ♥");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ShowInfo()
    {
        Console.Clear();
        
        Console.WriteLine("[ 기본 정보 ]");
        Console.WriteLine();
        Console.WriteLine("이름     : 석승민");
        Console.WriteLine("나이     : 24살");
        Console.WriteLine("전공     : 게임기획");
        Console.WriteLine("희망직무 : 게임 기획자");

        Console.WriteLine();
        Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
        Console.ReadKey();
    }

    static void ShowIntroduction()
    {
        Console.Clear();

        Console.WriteLine("[ 자기소개 ]");
        Console.WriteLine();
        Console.WriteLine("안녕하세요.");
        Console.WriteLine("게임 기획자를 목표로 공부하고 있습니다.");
        Console.WriteLine("잘 부탁 드립니다.");
 

        Console.WriteLine();
        Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
        Console.ReadKey();
    }


    static void ShowGoal()
    {
        Console.Clear();

        Console.WriteLine("[ 나의 목표 ]");
        Console.WriteLine();
        Console.WriteLine("게임의 재미를 설계할 수 있는");
        Console.WriteLine("게임 기획자가 되는 것이 목표입니다.");
        Console.WriteLine("특히 전투 시스템과 보스 패턴을");
        Console.WriteLine("전문적으로 기획하고 싶습니다.");

        Console.WriteLine();
        Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
        Console.ReadKey();
    }
}