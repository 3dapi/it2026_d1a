namespace _0916_c_
{
    internal class Program
    {
        static void Main(string[] args)
        {


            ////9번
            //Console.Write("태어난 연도를 입력하세요: ");
            //string input = Console.ReadLine();
            //int year = int.Parse(input);

            //// 연도를 12로 나눈 나머지 계산
            //int remainder = year % 12;
            //string animal = "";

            //// 나머지에 따라 띠 판별
            //switch (remainder)
            //{
            //    case 0: animal = "원숭이"; break;
            //    case 1: animal = "닭"; break;
            //    case 2: animal = "개"; break;
            //    case 3: animal = "돼지"; break;
            //    case 4: animal = "쥐"; break;
            //    case 5: animal = "소"; break;
            //    case 6: animal = "범"; break;
            //    case 7: animal = "토끼"; break;
            //    case 8: animal = "용"; break;
            //    case 9: animal = "뱀"; break;
            //    case 10: animal = "말"; break;
            //    case 11: animal = "양"; break;
            //}

            //Console.WriteLine($"{animal}띠입니다.");

            //10번
            //Console.Write("현재 월을 입력하세요 (1~12): ");
            //string input = Console.ReadLine();
            //int month = int.Parse(input);

            //if (month == 3 || month == 4 || month == 5)
            //{
            //    Console.WriteLine("봄입니다.");
            //}
            //else if (month == 6 || month == 7 || month == 8)
            //{
            //    Console.WriteLine("여름입니다.");
            //}
            //else if (month == 9 || month == 10 || month == 11)
            //{
            //    Console.WriteLine("가을입니다.");
            //}
            //else if (month == 12 || month == 1 || month == 2)
            //{
            //    Console.WriteLine("겨울입니다.");
            //}
            //else
            //{
            //    Console.WriteLine("잘못된 입력입니다. 1에서 12 사이의 숫자를 입력해주세요.");
            //}

            //11번 문제
            //int x = 15;

            //if (x > 10 && x < 20)
            //{
            //    Console.WriteLine("조건에 맞습니다.");
            //}

            //12번 문제

            //<< x = 0; y = 0; 일 때 >>
            //바깥쪽 if (x > 4) 조건에서 0 > 4는 거짓(False)이므로, 바로 else 블록으로 이동합니다.
            //실행 결과: Console.WriteLine(); 이 실행되어 화면에 빈 줄(줄바꿈)이 출력됩니다.

            //<< x = 10; y = 0; 일 때 >>
            //바깥쪽 if (x > 4) 조건에서 10 > 4는 참(True)이므로 안쪽 조건문으로 진입합니다.
            //안쪽 if (y > 2) 조건에서 0 > 2는 거짓(False)입니다.
            //안쪽 조건문에는 해당하는 else 블록이 없으므로 아무 작업도 수행하지 않고 종료됩니다.
            //실행 결과: 아무것도 출력되지 않음

            //<< x = 10; y = 10; 일 때 >>
            //바깥쪽 if (x > 4) 조건에서 10 > 4는 참(True)입니다
            //안쪽 if (y > 2) 조건에서 10 > 2도 참(True)입니다
            //Console.WriteLine(x * y); 가 실행되어 10 * 10의 연산 결과가 출력됩니다.
            //실행 결과: 100


            //13번
            // x가 미리 선언되어 있다고 가정할 때
            //Console.WriteLine((x % 2 == 0) ? "짝수" : "홀수");


            ////14번
            //Console.Write("학년을 입력하세요 : ");
            //int level = int.Parse(Console.ReadLine());

            //switch (level)
            //{

            //    case 1:
            //        Console.WriteLine("수강해야 하는 전공 학점: 12학점");
            //        break;
            //    case 2:
            //        Console.WriteLine("수강해야 하는 전공 학점: 18학점");
            //        break;
            //    case 3:
            //        Console.WriteLine("수강해야 하는 전공 학점: 10학점");
            //        break;
            //    case 4:
            //        Console.WriteLine("수강해야 하는 전공 학점: 18학점");
            //        break;
            //}
            

        }

    }
}
