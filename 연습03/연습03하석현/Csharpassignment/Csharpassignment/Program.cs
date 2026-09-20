using System;

// 프로그램 시작점
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("조건문 연습문제 9~14");
        Console.Write("실행할 문제 번호를 입력하세요: ");

        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("문제 번호는 정수로 입력하세요.");
            return;
        }

        switch (number)
        {
            case 9:
                {
                    Exercise09 problem = new Exercise09();
                    problem.Run();
                    break;
                }

            case 10:
                {
                    Exercise10 problem = new Exercise10();
                    problem.Run();
                    break;
                }

            case 11:
                {
                    Exercise11 problem = new Exercise11();
                    problem.Run();
                    break;
                }

            case 12:
                {
                    Exercise12 problem = new Exercise12();
                    problem.Run();
                    break;
                }

            case 13:
                {
                    Exercise13 problem = new Exercise13();
                    problem.Run();
                    break;
                }

            case 14:
                {
                    Exercise14 problem = new Exercise14();
                    problem.Run();
                    break;
                }

            default:
                {
                    Console.WriteLine("9~14 중에서 선택하세요.");
                    break;
                }
        }
    }
}


// 문제 9번
// 풀이: 태어난 연도를 12로 나눈 나머지에 따라 띠를 출력한다.
class Exercise09
{
    public void Run()
    {
        Console.Write("태어난 연도를 입력하세요: ");
        int year = int.Parse(Console.ReadLine() ?? "");

        switch (year % 12)
        {
            case 0:
                Console.WriteLine("원숭이띠입니다.");
                break;

            case 1:
                Console.WriteLine("닭띠입니다.");
                break;

            case 2:
                Console.WriteLine("개띠입니다.");
                break;

            case 3:
                Console.WriteLine("돼지띠입니다.");
                break;

            case 4:
                Console.WriteLine("쥐띠입니다.");
                break;

            case 5:
                Console.WriteLine("소띠입니다.");
                break;

            case 6:
                Console.WriteLine("범띠입니다.");
                break;

            case 7:
                Console.WriteLine("토끼띠입니다.");
                break;

            case 8:
                Console.WriteLine("용띠입니다.");
                break;

            case 9:
                Console.WriteLine("뱀띠입니다.");
                break;

            case 10:
                Console.WriteLine("말띠입니다.");
                break;

            case 11:
                Console.WriteLine("양띠입니다.");
                break;
        }
    }
}


// 문제 10번
// 풀이: 월의 범위에 따라 봄, 여름, 가을, 겨울을 출력한다.
// 3~5월: 봄
// 6~8월: 여름
// 9~11월: 가을
// 12월, 1월, 2월: 겨울
class Exercise10
{
    public void Run()
    {
        Console.Write("현재 월을 입력하세요: ");
        int month = int.Parse(Console.ReadLine() ?? "");

        if (month < 1 || month > 12)
        {
            Console.WriteLine("1부터 12까지의 월을 입력하세요.");
        }
        else if (month >= 3 && month <= 5)
        {
            Console.WriteLine("봄입니다.");
        }
        else if (month >= 6 && month <= 8)
        {
            Console.WriteLine("여름입니다.");
        }
        else if (month >= 9 && month <= 11)
        {
            Console.WriteLine("가을입니다.");
        }
        else
        {
            Console.WriteLine("겨울입니다.");
        }
    }
}


// 문제 11번
// 풀이: 두 조건을 모두 만족해야 하므로 && 연산자로 연결한다.
// x가 10보다 크고 20보다 작을 때만 문장을 출력한다.
class Exercise11
{
    public void Run()
    {
        Console.Write("정수 x를 입력하세요: ");
        int x = int.Parse(Console.ReadLine() ?? "");

        if (x > 10 && x < 20)
        {
            Console.WriteLine("조건에 맞습니다.");
        }
    }
}


// 문제 12번
class Exercise12
{
    public void Run()
    {
        // 아래 값을 바꾸어 각 경우를 실행합니다.
        // ① x = 0,  y = 0
        // ② x = 10, y = 0
        // ③ x = 10, y = 10
        //
        // 여기서는 각각의 중괄호 안에 변수를 선언하여
        // 세 가지 경우를 순서대로 실행합니다.
        // 각 블록의 x, y는 서로 다른 지역 변수입니다.

        // (1번)
        // 풀이: x > 4가 거짓이므로 바깥쪽 else가 실행된다.
        // 실행 결과: 빈 줄 한 줄 출력
        {
            Console.WriteLine("[① x = 0, y = 0 실행 결과 시작]");

            int x = 0;
            int y = 0;

            if (x > 4)
            {
                if (y > 2)
                {
                    Console.WriteLine(x * y);
                }
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine("[① 실행 결과 끝]");
        }

        // (2번)
        // 풀이: x > 4는 참이지만 y > 2는 거짓이다.
        // 바깥쪽 if가 참이므로 바깥쪽 else도 실행되지 않는다.
        // 실행 결과: 출력 없음(줄바꿈도 없음)
        {
            Console.WriteLine("[② x = 10, y = 0 실행 결과 시작]");

            int x = 10;
            int y = 0;

            if (x > 4)
            {
                if (y > 2)
                {
                    Console.WriteLine(x * y);
                }
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine("[② 실행 결과 끝]");
        }

        // (3번)
        // 풀이: x > 4와 y > 2가 모두 참이므로 x * y를 출력한다.
        // 실행 결과: 100
        {
            Console.WriteLine("[③ x = 10, y = 10 실행 결과 시작]");

            int x = 10;
            int y = 10;

            if (x > 4)
            {
                if (y > 2)
                {
                    Console.WriteLine(x * y);
                }
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine("[③ 실행 결과 끝]");
        }
    }
}


// 문제 13번
// 풀이: 2로 나눈 나머지가 0이면 짝수, 아니면 홀수이다.
// 삼항 연산자로 조건에 따른 문자열을 선택한다.
class Exercise13
{
    public void Run()
    {
        Console.Write("정수 x를 입력하세요: ");
        int x = int.Parse(Console.ReadLine() ?? "");

        string result = (x % 2 == 0)
            ? "짝수입니다."
            : "홀수입니다.";

        Console.WriteLine(result);
    }
}


// 문제 14번
// 풀이: 학년을 switch 문으로 검사하여 전공 학점을 출력한다.
// 1학년: 12학점
// 2학년: 18학점
// 3학년: 10학점
// 4학년: 18학점
// 원래 코드와 동일하게 1~4 이외의 값에는 학점을 출력하지 않는다.
class Exercise14
{
    public void Run()
    {
        Console.Write("학년을 입력하세요: ");
        int level = int.Parse(Console.ReadLine() ?? "");

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
        }
    }
}