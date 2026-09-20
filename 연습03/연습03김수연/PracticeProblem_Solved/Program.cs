namespace PracticeProblem_Solved
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //---------------------------------------------------
            //실습과제(zip 파일 제출할거)

            //9번문제_태어난 연도에 따라 그 해의 띠 출력
            {
                Console.WriteLine("9번문제_태어난 연도에 따라 그 해의 띠 출력");
                string input = Console.ReadLine();
                int iValue = Int32.Parse(input);

                switch (iValue % 12)
                {
                    case 0:
                        Console.WriteLine("원숭이");
                        break;

                    case 1:
                        Console.WriteLine("닭");
                        break;

                    case 2:
                        Console.WriteLine("개");
                        break;
                    case 3:
                        Console.WriteLine("돼지");
                        break;
                    case 4:
                        Console.WriteLine("쥐");
                        break;
                    case 5:
                        Console.WriteLine("소");
                        break;
                    case 6:
                        Console.WriteLine("범");
                        break;
                    case 7:
                        Console.WriteLine("토끼");
                        break;
                    case 8:
                        Console.WriteLine("용");
                        break;
                    case 9:
                        Console.WriteLine("뱀");
                        break;
                    case 10:
                        Console.WriteLine("말");
                        break;
                    case 11:
                        Console.WriteLine("양");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;

                }
            }


            //10번 문제_ 월 입력받아 계절 출력
            {
                Console.WriteLine("10번 문제_ 월 입력받아 계절 출력");
                Console.WriteLine("이번달은 몇 월 인가요?");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 12:
                    case 1:
                    case 2:
                        Console.WriteLine("겨울입니다");
                        break;
                    case 3:
                    case 4:
                    case 5:
                        Console.WriteLine("봄 입니다");
                        break;
                    case 6:
                    case 7:
                    case 8:
                        Console.WriteLine("여름입니다");
                        break;
                    case 9:
                    case 10:
                    case 11:
                        Console.WriteLine("가을 입니다");
                        break;
                }
            }

            //11번 문제_ if 하나 조건문으로 만들기
            {
                Console.WriteLine("11번 문제_ if 하나 조건문으로 만들기");
                string input = Console.ReadLine();
                int x = Int32.Parse(input);

                if (x > 10 && x < 20)
                {
                    Console.WriteLine("조건에 맞습니다");
                }
                else
                {
                    Console.WriteLine("조건에 맞지않습니다");
                }
            }


            //13번 문제_짝수홀수 가리기
            {
                Console.WriteLine("13번 문제_짝수홀수 가리기");
                string input = Console.ReadLine();
                int number = int.Parse(input);

                if (0 == number % 2)
                {
                    Console.WriteLine("짝수");
                }
                else
                {
                    Console.WriteLine("홀수");
                }
            }


            //14번 문제_if 조건문을 Switch 로 변경

            {
                Console.WriteLine("14번 문제_if 조건문을 Switch 로 변경");

                string? input = Console.ReadLine();
                int level = Int32.Parse(input);

                switch (level)
                {
                    case 1:
                        Console.WriteLine($"수강해야 하는 전공 학점 : 12학점");
                        break;

                    case 2:
                        Console.WriteLine("수강해야 하는 전공 학점 : 18학점");
                        break;

                    case 3:
                        Console.WriteLine("수강해야 하는 전공 학점 : 10학점");
                        break;

                    case 4:
                        Console.WriteLine("수강해야 하는 전공 학점 : 18학점");
                        break;
                }
            }
        }
    }
}
