using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        ////띠 구하기(9번 문제)
        //Console.Write("당신이 태어난 년도는? ");
        //string? input = Console.ReadLine();
        //var age = int.Parse(input);
        //int month = age % 12;
        //string your="";

        ////배열과 반복문 활용
        //string[] animal = ["원숭이", "닭","개","돼지","쥐", "소", "호랑이", "토끼","용","뱀","말","양"];
        //for (int i = 0; i < animal.Length; i++)
        //{
        //    if(month == i)
        //    {
        //        your = animal[i];
        //        Console.WriteLine("당신은 " + your + "띠입니다.");
        //        break;
        //    }
        //}

        ////if,elseif로 구현
        //if (month == 0)
        //{
        //    your = "원숭이";
        //}
        //else if (month == 1)
        //{
        //    your = "닭";
        //}
        //else if (month == 2)
        //{
        //    your = "개";
        //}
        //else if (month == 3)
        //{
        //    your = "돼지";
        //}
        //else if (month == 4)
        //{
        //    your = "쥐";
        //}
        //else if (month == 5)
        //{
        //    your = "소";
        //}
        //else if (month == 6)
        //{
        //    your = "호랑이";
        //}
        //else if (month == 7)
        //{
        //    your = "토끼";
        //}
        //else if (month == 8)
        //{
        //    your = "용";
        //}
        //else if (month == 9)
        //{
        //    your = "뱀";
        //}
        //else if (month == 10)
        //{
        //    your = "말";
        //}
        //else
        //{
        //    your = "양";
        //}
        //Console.WriteLine("당신은 " + your + "띠입니다.");


        ////계절(10번 문제)
        //Console.Write("몇 월인가요? ");
        //string? input = Console.ReadLine();
        //var month = int.Parse(input);
        //int ans = month / 3;
        //string[] season = ["겨울","봄","여름","가을","겨울"];
        //for(int i = 0; i < season.Length; i++)
        //{
        //    if (ans == i)
        //    {
        //        Console.Write("현재 계절은 " + season[i] + "입니다.\n");
        //        break;
        //    } 
        //}


        ////11번 문제(동시 만족)
        //Console.Write("x는? ");
        //string? input = Console.ReadLine();
        //var x = int.Parse(input);
        //if (x>10 && x < 20)
        //{
        //    Console.WriteLine("조건에 맞습니다.");
        //}


        ////12번(주석 해제시 결과)
        //int x, y;
        ////x = 0; y = 0;//1번
        ////x = 10; y = 0;//2번
        ////x = 10; y = 10;//3번
        //if (x > 4)
        //{
        //    if (y > 2)
        //    {
        //        Console.WriteLine(x*y);
        //    }
        //    else
        //    {
        //        Console.WriteLine();
        //    }
        //}
        ////1,2번일때 null
        ////3번일때 100


        ////13번
        //Console.Write("x는? ");
        //string? input = Console.ReadLine();
        //var x = int.Parse(input);
        //string ans = (x % 2 == 0)? "짝수" : "홀수";
        //Console.WriteLine("x는 "+ans+"입니다.");


        ////14번(switch변환)
        //Console.Write("학년을 입력하세요: ");
        //string? input = Console.ReadLine();
        //int level = int.Parse(input);
        //int[] needscore = [12, 18, 10, 18];
        ////switch로 구현
        //switch (level) 
        //{
        //    case 1:
        //        Console.WriteLine("수강해야하는 전공 학점: 12학점");
        //        break;
        //    case 2:
        //        Console.WriteLine("수강해야하는 전공 학점: 18학점");
        //        break;
        //    case 3:
        //        Console.WriteLine("수강해야하는 전공 학점: 10학점");
        //        break;
        //    case 4:
        //        Console.WriteLine("수강해야하는 전공 학점: 18학점");
        //        break;
        //    default: 
        //        Console.WriteLine("정확한 학년을 입력해주세요");
        //        break;
        //}

        ////배열 활용
        //for(int i=0;  i<needscore.Length; i++)
        //{
        //    if(level == i+1)
        //    {
        //        Console.WriteLine("수강해야하는 전공 학점: " + needscore[i] + "학점");
        //    }
        //}
    }
}
