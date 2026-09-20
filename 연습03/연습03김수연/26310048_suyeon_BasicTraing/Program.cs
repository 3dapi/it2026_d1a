/*
 * 이 프로그램은 식별자, 주석 연습입니다
 * 
 */

using System.Data;
using System.Runtime.InteropServices;

namespace _26310048_suyeon_BasicTraing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 키워드,식별자 연습
            //string name = "김수연";
            //string id = "26310048";
            //string subject = "객체지향C#";

            //Console.WriteLine($"이력서");
            //Console.WriteLine($"이름: {name}");
            //Console.WriteLine($"ID: {id}");
            //Console.WriteLine($"과목: {subject}");

            //Console.WriteLine(0);

            //Console.WriteLine($"이름: {name}, id: {id}, 과목: {subject}");

            // 연산 연습

            //Console.WriteLine(10 + 2000);
            //Console.WriteLine(10 + "2000");
            //Console.WriteLine(Int32.Parse("10") + 2000);
            //Console.WriteLine("10" + "2000");

            // 연산 연습
            //Console.WriteLine(12345 % 1);
            //Console.WriteLine((12345 % 10)/1);
            //Console.WriteLine((12345 % 100)/10);
            //Console.WriteLine((12345 % 1000)/ 100);
            //Console.WriteLine((12345 % 10000)/ 1000);
            //Console.WriteLine((12345 % 100000)/ 10000);

            // X % Y = (X/Y) * Y

            //Console.WriteLine(4 % 3);
            //Console.WriteLine(4 % -3);    //4 -(4/-3) *(-3)
            //Console.WriteLine(-4 % 3);    //4 -(-4/3) *(3)
            //Console.WriteLine(-4 % -3);   //4 -(-4/-3) *(-3)


            //Console.WriteLine(12345.0 / 1000.0);

            //Console.WriteLine(1 / 2);
            //Console.WriteLine(1.0 / 2.0);

            //Console.WriteLine(1.0 % 2.0);
            //Console.WriteLine(1.0 % 2.2);

            //Console.WriteLine(5.0 % 2.2);
            //Console.WriteLine(0.6 / 1.0);

            // 문자형
            //Console.WriteLine('A');
            //Console.WriteLine('쀏');
            //Console.WriteLine("안녕하세요");

            // 문자열
            //Console.WriteLine($"이력서 \n \t\\ 이름 : \"김수연\"");
            //Console.WriteLine($"이력서" + "\n \t\\ 이름 : \"김수연\"");

            //Console.WriteLine("안녕하세요"[0]);
            //Console.WriteLine("안녕하세요"[1]);
            //Console.WriteLine("안녕하세요"[2]);
            //Console.WriteLine("안녕하세요"[3]);
            //Console.WriteLine("안녕하세요"[4]);
            //Console.WriteLine("안녕하세요"[100]); // 안녕하세요 그 밖의 수로 넘어감 -> 컴파일 오류

            //Console.WriteLine("한"+"글");

            //Console.WriteLine('가' + '가'); // 유니코드
            // Bloon
            //Console.WriteLine(true);
            //Console.WriteLine(false);

            // 변수 선언과 저장 (초기화 한다)
            //int    IdNumber = 12345; //기본 숫자(정수)
            //double Score = 98.5; //소수점이 있다면 double
            //char   character = 'a'; //작은 따옴표 (홀따옴표)문자열 이라면 char
            //string Message = "안녕하세요"; //큰따옴표 문자열(겹따옴표) 이라면 string

            //변수 저장
            //int      IdNumber;          
            //long     gameMoney;        //int 보다 표현범위 넓음
            //double   Score;
            //char     character;
            //string   Message;

            //IdNumber = 15;       //변수니까 새로 지정가능
            //gameMoney = 200;

            //Console.WriteLine(IdNumber % gameMoney);

            //int a = 2147483640; 
            //int b = 52273;
            //Console.WriteLine(a+b);  //범위를 벗어나면 값이 오버플로됨, 오류로 감지되지않음


            Console.WriteLine(int.MinValue);
            Console.WriteLine(int.MaxValue);
            Console.WriteLine(uint.MinValue);
            Console.WriteLine(uint.MaxValue);
            Console.WriteLine(long.MinValue);
            Console.WriteLine(long.MaxValue);


        }
    }
}

// 기획문서의 스펙을 적는다
class GameScene
{
    //// 생성
    //Create();
    //// 초기화
    //Init();
    //// 갱신
    //Update();
    //// 랜더링
    //Render();
    //// 파괴
    //Distory():
}