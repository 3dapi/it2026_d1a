using System;
using System.Text;

namespace SelfIntroduction
{
    internal class Program
    {
        static void Main()
        {
            // 한글 깨짐 방지 (Windows 콘솔)
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================");
            Console.WriteLine("           자 기 소 개 서           ");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();

            // 이름
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 이름 ]");
            Console.ResetColor();
            Console.WriteLine("정우진");
            Console.WriteLine();

            // 생년월일
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 생년월일 ]");
            Console.ResetColor();
            Console.WriteLine("2008-01-02");
            Console.WriteLine();

            // MBTI
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[ MBTI ]");
            Console.ResetColor();
            Console.WriteLine("ISTJ");
            Console.WriteLine();

            // 장점
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[ 나의 장점 ]");
            Console.ResetColor();
            Console.WriteLine("- 책임감");
            Console.WriteLine("- 자기객관화");
            Console.WriteLine();

            // 단점
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ 나의 단점 ]");
            Console.ResetColor();
            Console.WriteLine("- 불필요한 걱정");
            Console.WriteLine("- 과제 미루기");
            Console.WriteLine();

            // 취미 및 관심사
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[ 취미 및 관심사 ]");
            Console.ResetColor();
            Console.WriteLine("- 게임 플레이");
            Console.WriteLine("- 독서");
            Console.WriteLine();

            // 좌우명
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[ 좌우명 ]");
            Console.ResetColor();
            Console.WriteLine("\"해보지 않으면 모른다\"");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================");
            Console.ResetColor();
        }
    }
}