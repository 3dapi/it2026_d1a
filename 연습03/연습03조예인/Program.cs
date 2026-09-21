using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          C# 문제 모음");
            Console.WriteLine("================================");
            Console.WriteLine("1. 띠 계산");
            Console.WriteLine("2. 계절 계산");
            Console.WriteLine("3. 학년별 전공 학점");
            Console.WriteLine("4. 논리 연산자 조건문");
            Console.WriteLine("5. 중첩 조건문 실행 결과");
            Console.WriteLine("0. 프로그램 종료");
            Console.WriteLine("================================");
            Console.Write("문제를 선택하세요: ");

            int menu = int.Parse(s: Console.ReadLine());

            Console.Clear();

            switch (menu)
            {
                case 1:
                    Zodiac();
                    break;

                case 2:
                    Season();
                    break;

                case 3:
                    Credit();
                    break;

                case 4:
                    LogicalOperator();
                    break;

                case 5:
                    NestedCondition();
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("잘못된 메뉴입니다.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("계속하려면 아무 키나 누르세요.");
            Console.ReadKey();
        }
    }

    // 1. 띠 계산
    static void Zodiac()
    {
        Console.WriteLine("================================");
        Console.WriteLine("             띠 계산");
        Console.WriteLine("================================");

        Console.Write("태어난 연도를 입력하세요: ");

        int year = int.Parse(Console.ReadLine());

        int result = year % 12;

        if (result == 0)
        {
            Console.WriteLine("원숭이띠");
        }
        else if (result == 1)
        {
            Console.WriteLine("닭띠");
        }
        else if (result == 2)
        {
            Console.WriteLine("개띠");
        }
        else if (result == 3)
        {
            Console.WriteLine("돼지띠");
        }
        else if (result == 4)
        {
            Console.WriteLine("쥐띠");
        }
        else if (result == 5)
        {
            Console.WriteLine("소띠");
        }
        else if (result == 6)
        {
            Console.WriteLine("범띠");
        }
        else if (result == 7)
        {
            Console.WriteLine("토끼띠");
        }
        else if (result == 8)
        {
            Console.WriteLine("용띠");
        }
        else if (result == 9)
        {
            Console.WriteLine("뱀띠");
        }
        else if (result == 10)
        {
            Console.WriteLine("말띠");
        }
        else if (result == 11)
        {
            Console.WriteLine("양띠");
        }
    }

    // 2. 계절 계산
    static void Season()
    {
        Console.WriteLine("================================");
        Console.WriteLine("             계절 계산");
        Console.WriteLine("================================");

        Console.Write("월을 입력하세요: ");

        int month = int.Parse(Console.ReadLine());

        if (month == 3 || month == 4 || month == 5)
        {
            Console.WriteLine("봄");
        }
        else if (month == 6 || month == 7 || month == 8)
        {
            Console.WriteLine("여름");
        }
        else if (month == 9 || month == 10 || month == 11)
        {
            Console.WriteLine("가을");
        }
        else if (month == 12 || month == 1 || month == 2)
        {
            Console.WriteLine("겨울");
        }
        else
        {
            Console.WriteLine("잘못된 월입니다.");
        }
    }

    // 3. 학년별 전공 학점
    static void Credit()
    {
        Console.WriteLine("================================");
        Console.WriteLine("        학년별 전공 학점");
        Console.WriteLine("================================");

        Console.Write("학년을 입력하세요: ");

        int level = int.Parse(Console.ReadLine());

        switch (level)
        {
            case 1:
                Console.WriteLine("수강해야 하는 전공 학점: 12학점");
                break;

            case 2:
                Console.WriteLine("수강해야 하는 전공 학점: 18학점");
                break;

            case 3:
                Console.WriteLine("수강해야 하는 전공 학점: 10학점");
                break;

            case 4:
                Console.WriteLine("수강해야 하는 전공 학점: 18학점");
                break;

            default:
                Console.WriteLine("잘못된 학년입니다.");
                break;
        }
    }

    // 4. 11번 문제
    static void LogicalOperator()
    {
        Console.WriteLine("================================");
        Console.WriteLine("4번. 논리 연산자 조건문");
        Console.WriteLine("================================");

        Console.WriteLine();
        Console.WriteLine("[문제]");
        Console.WriteLine("논리 연산자를 사용하여 다음 중첩 조건문을");
        Console.WriteLine("if가 한 개인 조건문으로 작성하시오.");
        Console.WriteLine();
        Console.WriteLine("if (x > 10)");
        Console.WriteLine("{");
        Console.WriteLine("    if (x < 20)");
        Console.WriteLine("    {");
        Console.WriteLine("        Console.WriteLine(\"조건에 맞습니다.\");");
        Console.WriteLine("    }");
        Console.WriteLine("}");

        Console.WriteLine();
        Console.WriteLine("[정답]");
        Console.WriteLine();
        Console.WriteLine("if (x > 10 && x < 20)");
        Console.WriteLine("{");
        Console.WriteLine("    Console.WriteLine(\"조건에 맞습니다.\");");
        Console.WriteLine("}");

        Console.WriteLine();
        Console.WriteLine("[해설]");
        Console.WriteLine();
        Console.WriteLine("두 조건을 모두 만족해야 하므로");
        Console.WriteLine("논리 연산자 &&를 사용합니다.");
        Console.WriteLine();
        Console.WriteLine("x > 10 AND x < 20");
        Console.WriteLine("→ x가 10보다 크고 20보다 작을 때");
        Console.WriteLine("→ \"조건에 맞습니다.\"가 출력됩니다.");
    }

    // 5. 12번 문제
    static void NestedCondition()
    {
        Console.WriteLine("================================");
        Console.WriteLine("5번. 중첩 조건문 실행 결과");
        Console.WriteLine("================================");

        Console.WriteLine();
        Console.WriteLine("[문제]");
        Console.WriteLine("다음 코드의 주석 부분이 입력 값일 때의");
        Console.WriteLine("실행 결과를 쓰시오.");
        Console.WriteLine();
        Console.WriteLine("if (x > 4)");
        Console.WriteLine("{");
        Console.WriteLine("    if (y > 2)");
        Console.WriteLine("    {");
        Console.WriteLine("        Console.WriteLine(x * y);");
        Console.WriteLine("    }");
        Console.WriteLine("}");
        Console.WriteLine("else");
        Console.WriteLine("{");
        Console.WriteLine("    Console.WriteLine();");
        Console.WriteLine("}");

        Console.WriteLine();
        Console.WriteLine("① x = 0; y = 0;");
        Console.WriteLine("② x = 10; y = 0;");
        Console.WriteLine("③ x = 10; y = 10;");

        Console.WriteLine();
        Console.WriteLine("[정답]");
        Console.WriteLine();
        Console.WriteLine("① x = 0; y = 0;");
        Console.WriteLine("→ 빈 줄 출력");

        Console.WriteLine();
        Console.WriteLine("② x = 10; y = 0;");
        Console.WriteLine("→ 출력 없음");

        Console.WriteLine();
        Console.WriteLine("③ x = 10; y = 10;");
        Console.WriteLine("→ 100");

        Console.WriteLine();
        Console.WriteLine("[해설]");
        Console.WriteLine();
        Console.WriteLine("① x = 0, y = 0");
        Console.WriteLine("- x > 4 → 0 > 4 → 거짓");
        Console.WriteLine("- 따라서 else 실행");
        Console.WriteLine("- Console.WriteLine() → 빈 줄 출력");

        Console.WriteLine();
        Console.WriteLine("② x = 10, y = 0");
        Console.WriteLine("- x > 4 → 10 > 4 → 참");
        Console.WriteLine("- y > 2 → 0 > 2 → 거짓");
        Console.WriteLine("- 아무것도 출력하지 않음");

        Console.WriteLine();
        Console.WriteLine("③ x = 10, y = 10");
        Console.WriteLine("- x > 4 → 10 > 4 → 참");
        Console.WriteLine("- y > 2 → 10 > 2 → 참");
        Console.WriteLine("- x * y 실행");
        Console.WriteLine("- 10 × 10 = 100");
    }
}