namespace abc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 09 ~ 15
            //// 09번
            //// [문제 : 사용자에게 태어난 연도를 입력받아 그 해의 띠를 출력하는 프로그램을 작성하시오.]
            //{
            //    Console.WriteLine("당신이 태어난 연도를 입력하시오 :");
            //    int year = Console.Read();
            //    Console.WriteLine("당신의 띠는");
            //    switch (year % 12)
            //    {
            //        case 0:
            //            Console.WriteLine("원숭이띠 입니다");
            //            break;
            //        case 1:
            //            Console.WriteLine("닭띠 입니다");
            //            break;
            //        case 2:
            //            Console.WriteLine("개띠 입니다");
            //            break;
            //        case 3:
            //            Console.WriteLine("돼지띠 입니다");
            //            break;
            //        case 4:
            //            Console.WriteLine("쥐띠 입니다");
            //            break;
            //        case 5:
            //            Console.WriteLine("소띠 입니다");
            //            break;
            //        case 6:
            //            Console.WriteLine("범띠 입니다");
            //            break;
            //        case 7:
            //            Console.WriteLine("토끼띠 입니다");
            //            break;
            //        case 8:
            //            Console.WriteLine("용띠 입니다");
            //            break;
            //        case 9:
            //            Console.WriteLine("뱀띠 입니다");
            //            break;
            //        case 10:
            //            Console.WriteLine("말띠 입니다");
            //            break;
            //        case 11:
            //            Console.WriteLine("양띠 입니다");
            //            break;
            //        default:
            //            Console.WriteLine("오류발생");
            //            break;
            //    }
            //}

            //// 10번
            //// [문제 : 사용자에게 현재 월을 입력받아 계절을 출력하는 프로그램을 작성하시오]
            //{
            //        Console.WriteLine("현재 월을 숫자로 입력하시오");
            //    var month = Int32.Parse(Console.ReadLine());
            //    if (month <= 0 || month >= 13)
            //    {
            //        Console.WriteLine("잘못된 입력입니다");
            //    }
            //    else
            //    {
            //        if (month >= 3 && month <= 5)
            //        {
            //            Console.WriteLine("현재는 봄입니다");
            //        }
            //        else if (month >= 6 && month <= 8)
            //        {
            //            Console.WriteLine("현재는 여름입니다");
            //        }
            //        else if (month >= 9 && month <= 11)
            //        {
            //            Console.WriteLine("현재는 가을입니다");
            //        }
            //        else
            //        {
            //            Console.WriteLine("현재는 겨울입니다");
            //        }
            //    }
            //}

            //// 11번
            //// [문제 : 논리 연산자를 사용하여 다음 중첩 조건문을 if가 한 개인 조건문으로 작성하시오]
            //// [원문]
            ////if ( x > 10)
            ////{
            ////    if (x < 20)
            ////    {
            ////        Console.WriteLine("조건에 맞습니다.");
            ////    }
            ////}
            //// [풀이]
            //{
            //    int x = 15;
            //    if (x > 10 && x < 20)
            //    {
            //        Console.WriteLine("조건에 맞습니다");
            //    }
            //}


            //// 12번
            //// [문제 :다음 코드의 주석 부분이 입력 값일 때의 실행 결과를 쓰시오]
            //// [원문]
            //static void Main(string[] args)
            //{
            //    int x, y;
            //    /*
            //     * x = 0; y = 0;
            //     * x = 10; y = 0;
            //     * x = 10; y = 10;
            //     */
            //    if (x > 4)
            //    {
            //        if (y > 2)
            //        {
            //            Console.WriteLine(x * y);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine();
            //    }
            //}
            //{
            //    //실행 결과 = [1번 = 0 | 2번 = 0 | 3번 = 100]
            //}

            //// 13번
            //// [문제 : 삼항 연산자를 활용하여 숫자 x가 짝수인지 홀수인지 출력하는 코드를 작성하시오.]
            //{
            //    Console.WriteLine("짝홀 판독기 시작. \n 아무 숫자를 입력해주세요 :");
            //    var input = int.Parse(Console.ReadLine());
            //    Console.WriteLine(input % 2 == 0 ? "짝수" : "홀수");
            //}

            //// 14번
            //// [문제 :다음 if 조건문으로 작성된 프로그램을 switch 조건문으로 변경하시오]
            //{
            //    Console.Write("학년을 입력하세요: ");
            //    int level = int.Parse(Console.ReadLine());

            //    switch (level - 1)
            //    {
            //        case 0:
            //            Console.WriteLine("수강해야 하는 전공 학점: 12학점");
            //            break;
            //        case 1:
            //            Console.WriteLine("수강해야 하는 전공 학점: 18학점");
            //            break;
            //        case 2:
            //            Console.WriteLine("수강해야 하는 전공 학점: 10학점");
            //            break;
            //        case 3:
            //            Console.WriteLine("수강해야 하는 전공 학점: 18학점");
            //            break;
            //        default:
            //            Console.WriteLine("잘못된 입력입니다.");
            //            break;
            //    }
            //}

            //// 15번
            //// [문제 :아내가 프로그래머 남편에게 "쇼핑하러 갈 때 우유 하나 사와. 아, 계란 있으면 6개 사와."라고 말했다. 남편은 잠시 후 우유를 6개 사왔다.
            //// 남편은 잠시 후 우유를 6개 사왔다. 남편이 머릿속에 그렸을 코드를 작성하시오.]
            //Console.WriteLine("문을 나설 때 받은 명령 : 우유 하나, 계란이 있을 경우 여섯개.\n");
            //int milkCount = 1;
            //Console.WriteLine("직원에게 질문 : 혹시 여기 계란 있나요?");
            //Console.WriteLine("직원의 입장에서 계란의 유무를 네/아니오로 적어주세요");
            //string employeeAnswer = Console.ReadLine();
            //bool hasEgg = employeeAnswer.Contains("네");
            //if (hasEgg == true)
            //{
            //    milkCount += 5;
            //    Console.WriteLine($"\n결과 : 계란의 존재가 {hasEgg}였어. 그래서 우유를 {milkCount}개 사왔어. 잘했지?");
            //}
            //else
            //{
            //    Console.WriteLine($"\n결과 : 계란의 존재가 {hasEgg}였어. 그래서 우유를 {milkCount}개 사왔어. 잘했지?");
            //}
        }
    }
}
