static void Main(string[] args)
{
    //9번 띠 분류
    var input = Console.ReadLine();
    var birthYear = Int32.Parse(input);

    if (birthYear % 12 == 0 )
    {
        Console.WriteLine("원숭이띠");
    }
    else if (birthYear % 12 == 1)
    {
        Console.WriteLine("닭띠");
    }
    else if (birthYear % 12 == 2)
    {
        Console.WriteLine("개띠");
    }
    else if (birthYear % 12 == 3)
    {
        Console.WriteLine("돼지띠");
    }
    else if (birthYear % 12 == 4)
    {
        Console.WriteLine("쥐띠");
    }
    else if (birthYear % 12 == 5)
    {
        Console.WriteLine("소띠");
    }
    else if (birthYear % 12 == 6)
    {
        Console.WriteLine("호랑이띠");
    }
    else if (birthYear % 12 == 7)
    {
        Console.WriteLine("토끼띠");
    }
    else if (birthYear % 12 == 8)
    {
        Console.WriteLine("용띠");
    }
    else if (birthYear % 12 == 9)
    {
        Console.WriteLine("뱀띠");
    }
    else if (birthYear % 12 == 10)
    {
        Console.WriteLine("말띠");
    }
    else
    {
        Console.WriteLine("양띠");
    }



    //10번 계절 출력
    var input1 = Console.ReadLine();
    var month = Int32.Parse(input1);

    if (month >= 3 && month < 6)
    {
        Console.WriteLine("봄");
    }
    else if (month >= 6 && month < 9)
    {
        Console.WriteLine("여름");
    }
    else if (month >= 9 && month < 12)
    {
        Console.WriteLine("가을");
    }
    else if (month >= 12 && month < 3)
    {
        Console.WriteLine("겨울");
    }



    //11번 if정리
    int x = 0;
    if (x > 10 && x < 20)
    {
        Console.WriteLine("조건에 맞습니다.");
    }



    //12번 실행 결과 예상
    // x, y 각각 값이 10이므로 x는 4보다 크고, y는 2보다 크기 때문에 x*y의 값인 100이 출력될 것이다.



    //13번 삼항연산자
    int x2 = 3;
    string result = (x2 % 2 == 0) ? "짝수" : "홀수";
    Console.WriteLine(result);



    //14번 if를 switch로 변환
    static void Main(string[] args)
    {
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