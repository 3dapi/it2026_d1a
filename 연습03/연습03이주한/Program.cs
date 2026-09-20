using System.Threading.Channels;

class Program
{
    static void Main(string[] args)
    {
        //09번
        Console.WriteLine("\n09번 띠 출력");
        int inputYear = int.Parse(Console.ReadLine());
        int animalYear = inputYear % 12;
        string animal = "";

        if (animalYear == 0)
        {
            animal = "원숭이";
        }
        else if (animalYear == 1)
        {
            animal = "닭";
        }
        else if (animalYear == 2)
        {
            animal = "개";
        }
        else if (animalYear == 3)
        {
            animal = "돼지";
        }
        else if (animalYear == 4)
        {
            animal = "쥐";
        }
        else if (animalYear == 5)
        {
            animal = "소";
        }
        else if (animalYear == 6)
        {
            animal = "호랑이";
        }
        else if (animalYear == 7)
        {
            animal = "토끼";
        }
        else if (animalYear == 8)
        {
            animal = "용";
        }
        else if (animalYear == 9)
        {
            animal = "뱀";
        }
        else if (animalYear == 10)
        {
            animal = "말";
        }
        else if (animalYear == 11)
        {
            animal = "양";
        }

        Console.WriteLine($"{inputYear}년생은 {animal}띠입니다.");




        //10번
        Console.WriteLine("\n10번 계절 출력");
        int inputMonth = int.Parse(Console.ReadLine());
        string season = "";

        if (inputMonth == 3 || inputMonth == 4 || inputMonth == 5)
        {
            season = "봄";
        }
        else if (inputMonth == 6 || inputMonth == 7 || inputMonth == 8)
        {
            season = "여름";
        }
        else if (inputMonth == 9 || inputMonth == 10 || inputMonth == 11)
        {
            season = "가을";
        }
        else if (inputMonth == 12 || inputMonth == 1 || inputMonth == 2)
        {
            season = "겨울";
        }

        Console.WriteLine($"{inputMonth}월은 {season}입니다.");



        //11번
        Console.WriteLine("\n11번 중첩조건문");
        int x = 15;
        if (x > 10 && x < 20)
        {
            Console.WriteLine("조건에 맞습니다.");
        }



        //12번
        //1. 빈 한줄 출력
        //2. 아무것도 출력하지 않음
        //3. 100 출력



        //13번
        Console.WriteLine("\n13번 삼항연산자");
        int inputX = int.Parse(Console.ReadLine());
        string answer = "";

        answer = inputX % 2 == 0 ? "X는 짝수입니다." : "X는 홀수입니다.";
        Console.WriteLine(answer);



        //14번
        Console.WriteLine("\n14번 switch문\n학년을 입력하세요: ");
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
