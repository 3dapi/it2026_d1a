namespace _26031018_sulljiun_assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////연습 문제


            //9번 사용자에게 태어난 년도를 입력받아 그 해의 띠를 출력하시오.

            { 
            string input = Console.ReadLine();
            int ivalue = Int32.Parse(input);
            switch (ivalue % 12)
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
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            }

            { 
            //10번 사용자에게 월을 입력받아 계절을 출력하는 코드를 작성하시오  
            var input = Int32.Parse(Console.ReadLine());
            if (input == 12 || input == 1 || input == 2)
            {
                Console.WriteLine("계절: 겨울 ");
            }
            else if (input >= 3 && input <= 5)
            {
                Console.WriteLine("계절: 봄 ");
            }
            else if (input >= 6 && input <= 8)
            {
                Console.WriteLine("계절: 여름 ");
            }
            else if (input >= 9 && input <= 11)
            {
                Console.WriteLine("계절: 가을 ");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            }

            //11번
            { 
            var x = 10;

            if (x > 10 || x < 20)
            {
                Console.WriteLine("조건이 맞습니다");
            }

            }


            //12번 다음 코드의 주석 부분이 입력 값일 때 실행 결과를 쓰시오.
            {
            var x = 0;
            var y = 0;
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
                //실행 답
                //x = 0, y = 0; -> "개행"
                //x = 10, y = 0; -> ""
                //x = 10, y = 10; -> 100

            }

            //13번

            var x = 40;
            Console.WriteLine(0 == x % 2 % 2 ? "짝수" : "홀수");





            //14번 다음 if 조건문으로 작성된 프로그램을 swith문으로 출력하는 코드를 작성하시오

            Console.WriteLine("학년을 입력하시오");
            int level = Int32.Parse(Console.ReadLine());

            switch (level)
            {
                case 1:
                    Console.WriteLine("수강해야하는 전공 학점: 12학점");
                    break;
                case 2:
                    Console.WriteLine("수강해야하는 전공 학점: 18학점");
                    break;
                case 3:
                    Console.WriteLine("수강해야하는 전공 학점: 10학점");
                    break;
                case 4:
                    Console.WriteLine("수강해야하는 전공 학점: 18학점");
                    break;
                default:
                    Console.WriteLine("잘못된 입력");
                    break;
            }
        }
    }
}
