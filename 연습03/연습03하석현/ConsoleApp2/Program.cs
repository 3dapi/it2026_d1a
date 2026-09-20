class Program
{
    static void Main(string[] args)
    {
        //키워드, 식별자 연습
        //string name = "하석현";
        //string id100000 = "648565";
        //string _subject = "객체지향c#";

        //system.Console.WriteLine($"이름 : {name}");
        //system.Console.WriteLine($"이름 : {name}");
        //system.Console.WriteLine($"이름 : {name}, id: {id100000}, 과목 : {_subject}");

        //연산 연습
        //Console.WriteLine(10 + 2000);
        Console.WriteLine(10 + "2000");
        Console.WriteLine("10" + "2000");
        Console.writeLine(Int32.Parse("10") + 2000);

        Console.writeLine(12345%1);
        Console.writeLine(12345 % 10/ 1);
        Console.writeLine(12345 % 100 / 10);
        Console.writeLine(12345 % 1000 / 100);
        Console.writeLine(12345 % 10000 / 1000);
        Console.writeLine(12345 % 100000 / 10000);

        // x % y = x - (x / y) * y
        Console.WriteLine(4 % 3);
        Console.WriteLine(4 % -3);      //4 - (4/-3) * (-3)
        Console.WriteLine(-4 % 3);      //4 - (4/3) * (3)  
        Console.WriteLine(-4 % -3);     //4 - (-4/-3) * (-3)

        Console.WriteLine(12345.0 / 1000.0);

        Console.WriteLine(1.0 / 2.0);
        Console.WriteLine(1 / 2);

        Console.WriteLine(1.0 % 2.2);
        Console.WriteLine(5.0 % 2.2);
        Console.Writeline(0.6 % 3);

        //문자형
        Console.WriteLine('똚');

        //문자열
        Console.WriteLine("얀녕하세요.\t\t\t반가워요");
        Console.WriteLine("\t\t이름: 하석현");
        Console.WriteLine("이력서\n\t\\이름 : \"하석현\"");

        //인덱스는 0부터
        Console.WriteLine("안녕하세요"[0]);
        Console.WriteLine("안녕하세요"[1]);
        Console.WriteLine("안녕하세요"[2]);
        Console.WriteLine("안녕하세요"[4]);
        Console.WriteLine("안녕하세요"[100]);

        Console.WriteLine("가" + "나");

        Console.WriteLine('한' + '글'); //한글의 유니코드 테이블
        Console.WriteLine('가' + '가');
        Console.WriteLine("난" + '가');
        Console.WriteLine('난' + "가"); //코드로 나오나?

        Console.WriteLine("난" + 0XAC00);

        //boolean
        Console.WriteLine(true);
        Console.WriteLine(false);

        //변수
        int idNumber 1235; //선언과 동시에 초기화한다 이렇게 표현?
        long gameMoney = 10000000000000000; //용량 큰 숫자를 표현하기 위함? 8byte 
        double score =//8.5; //변수값을 지워도 된다? 왜..?
        char character = 'a'; //등호(asignment)는 뭐라고? 못 들음ㅋㅋ..
        string Greetings = "안녕하세요"; //type은 자료형

        idNumber = 15;
        gameMoney = 200;

        Console.WriteLine(idNumber % gameMoney);

        int a = 2147483640;
        int b = 52273;
        Console.WriteLine(a + b);
        Console.WriteLine(int.MinValue);
        Console.WriteLine(int.MaxValue);
        Console.WriteLine(uint.MinValue);
        Console.WriteLine(uint.MaxValue);
        //양의 영역을 벗어나면 음수의 영역으로 넘어간다
        //-> 오버플로(컴이 오류발생을 감지하지 못함)
    }
}

//기획문서의 스펙을 적는다.
class Gamescene
{
/*    //생성
    Create();

    //초기화
    Init();

    //갱신
    Update();

    //렌더링
    Render();

    //파괴
    Destroy();*/
}