namespace _26310048_김수연_자소서
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("나의 이력서");

            Console.SetCursorPosition(1, 2);

            Console.ResetColor();

            Console.WriteLine();
            Console.Write("\x1b[38;2;230;140;50m");

            Console.WriteLine("이름");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 김수연");

            Console.SetCursorPosition(1, 5);

            Console.WriteLine();
            Console.Write("\x1b[38;2;230;140;50m");

            Console.WriteLine("나이");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 23살");

            Console.SetCursorPosition(3, 8);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("2004년생");

            Console.SetCursorPosition(1, 9);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MBTI");
            Console.WriteLine(">> INTP");

            Console.SetCursorPosition(6, 12);

            Console.WriteLine();

            Console.Write("\x1b[38;2;230;140;50m");

            Console.WriteLine("특이사항");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 편입생, 아트전공");

            Console.SetCursorPosition(1, 15);
            Console.WriteLine();

            Console.Write("\x1b[38;2;230;140;50m");

            Console.WriteLine("가능한 게임엔진");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 유니티, 언리얼(블루프린트)");

            Console.SetCursorPosition(1, 18);
            Console.WriteLine();

            Console.Write("\x1b[38;2;230;140;50m");

            Console.WriteLine("좋아하는 게임");

            Console.SetCursorPosition(1, 20);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("(갑툭튀 심한)");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> 공포게임 제외한 모든 장르 좋아합니다");
        }
    }
}
