using System;

namespace ProblemSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================
            // 09번: 태어난 연도로 띠 출력하기
            // =========================================

            //Console.Write("태어난 연도를 입력하세요: ");
            //int year = int.Parse(Console.ReadLine());

            //switch (year % 12)
            //{
            //    case 0: Console.WriteLine("원숭이"); break;
            //    case 1: Console.WriteLine("닭"); break;
            //    case 2: Console.WriteLine("개"); break;
            //    case 3: Console.WriteLine("돼지"); break;
            //    case 4: Console.WriteLine("쥐"); break;
            //    case 5: Console.WriteLine("소"); break;
            //    case 6: Console.WriteLine("호랑이"); break;
            //    case 7: Console.WriteLine("토끼"); break;
            //    case 8: Console.WriteLine("용"); break;
            //    case 9: Console.WriteLine("뱀"); break;
            //    case 10: Console.WriteLine("말"); break;
            //    case 11: Console.WriteLine("양"); break;
            //}
            //Console.WriteLine();


            // =========================================
            // 10번: 현재 월을 입력받아 계절 출력하기
            // =========================================

            //Console.Write("월을 입력하세요 (1~12): ");
            //int month = int.Parse(Console.ReadLine());

            //if (month >= 3 && month <= 5)
            //{
            //    Console.WriteLine("봄");
            //}
            //else if (month >= 6 && month <= 8)
            //{
            //    Console.WriteLine("여름");
            //}
            //else if (month >= 9 && month <= 11)
            //{
            //    Console.WriteLine("가을");
            //}
            //else
            //{
            //    Console.WriteLine("겨울");
            //}
            //Console.WriteLine();


            // =========================================
            // 11번: 논리 연산자를 사용한 조건문
            // =========================================

            //Console.Write("x 값을 입력하세요: ");
            //int x11 = int.Parse(Console.ReadLine());

            //if (x11 > 10 && x11 < 20)
            //{
            //    Console.WriteLine("조건에 맞습니다.");
            //}
            //Console.WriteLine();


            // =========================================
            // 12번: 중첩 조건문 검증
            // =========================================

            //Console.Write("x 값을 입력하세요: ");
            //int x12 = int.Parse(Console.ReadLine());
            //Console.Write("y 값을 입력하세요: ");
            //int y12 = int.Parse(Console.ReadLine());

            //if (x12 > 4)
            //{
            //    if (y12 > 2)
            //    {
            //        Console.WriteLine(x12 * y12);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine();
            //}
            //Console.WriteLine();


            // =========================================
            // 13번: 삼항 연산자를 사용한 짝수/홀수 판별
            // =========================================

            //Console.Write("숫자 x를 입력하세요: ");
            //int x13 = int.Parse(Console.ReadLine());

            //string result13 = (x13 % 2 == 0) ? "짝수" : "홀수";
            //Console.WriteLine(result13);
            //Console.WriteLine();


            // =========================================
            // 14번: switch문으로 작성한 학년별 학점 안내
            // =========================================
            
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
            }
        }
    }
}