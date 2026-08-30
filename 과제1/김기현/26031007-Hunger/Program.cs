namespace _26031007_Hunger
{
    internal class Program
    {
        static string[] titles =
        {
            "===[인적사항]===",
            "===[자기소개서]===",
            "===[자기이력서]==="
        };
        static ConsoleColor[] colors =
        {
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Green
        };
        static string[] texts =
        {
            "이름:김기현\n생년월일:2002.05.25\n학번:26031007\n학년:1학년\n학과:게임기획과",
            "성격소개:생각을 오래하는 경향이 있으나, 그만큼 신중하고 객관적으로 판단하여 대응합니다. 자주 대화를 통해 성격의 단점을 고쳐나가는 중입니다.\n" +
                "학교생활:학교에서는 늘 최선을 다하며 다양한 분야의 지식을 습득하여 본인만의 도메인을 넓히려한다\n" +
                "지원동기:이 수업을 통해 유니티에 필요한 언어의 이해도를 높여 체계적인 기획서를 설계하고싶다는 생각에 지원하게 되었습니다.\n" +
                "포부:설계방식에 대한 효율적이고 창의적인 아이디어를 떠올려 프로젝트에서 사용해보겠습니다.",
            "학력사항:포항제철고등학교(졸업) | 한국IT전문학교(재학중)\n자격증:운전면허(2종수동)"
        };
        static string[] lines =
       {
            "==================",
        };
        static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("#===[이력서 및 자기소개서]===#");
            Console.SetCursorPosition(0, 5);
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = colors[i];
                Console.WriteLine(titles[i] + "\n");
                Console.WriteLine(texts[i] + "\n");
                Console.WriteLine(lines[0] + "\n\n\n");
            }
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
