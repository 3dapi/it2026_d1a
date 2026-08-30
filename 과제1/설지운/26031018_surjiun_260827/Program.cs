namespace _26031018_surjiun_260827
{
    internal class Program
    {
        private const int CategoryWidth = 14;
        private const int ContentWidth = 40;

        private const ConsoleColor CategoryBackgroundColor = ConsoleColor.DarkCyan;
        private const ConsoleColor CategoryTextColor = ConsoleColor.White;

        private const ConsoleColor ContentBackgroundColor = ConsoleColor.DarkGray;
        private const ConsoleColor ContentTextColor = ConsoleColor.White;

        static void Main(string[] args)
        {
            
            PrintRow("이름", "설지운");
            PrintRow("생년월일", "2003년 04월 04일");
            PrintRow("학과", "게임기획");
            PrintRow("취미", "게임, 커스텀 키보드 및 컴퓨터 조립");
             
        }

        static void PrintRow(string category, string content)
        {

            Console.ForegroundColor = CategoryTextColor;

            Console.BackgroundColor = CategoryBackgroundColor;
            WritePadded("  " + category, CategoryWidth);

            Console.ForegroundColor = ContentTextColor;
            Console.BackgroundColor = ContentBackgroundColor;
            
            WritePadded("  " + content, ContentWidth);
            
            Console.ResetColor();
            Console.WriteLine();
        }

        static void WritePadded(string text, int width)
        {
            Console.Write(text);

            int emptyWidth = width - GetDisplayWidth(text);

            if (emptyWidth > 0)
            {
                Console.Write(new string(' ', emptyWidth));
            }
        }

        static int GetDisplayWidth(string text)
        {

            
            int width = 0;
            
            foreach (char character in text)
            {
                if (character >= 0xAC00 && character <= 0xD7A3)
                {
                    width += 2;
                }
                else
                {
                    width++;
                }
            }
            return width;
        }
    }
}
