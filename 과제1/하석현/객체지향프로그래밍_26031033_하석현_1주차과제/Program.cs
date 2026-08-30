namespace 객체지향프로그래밍_26031033_하석현_1주차과제
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //이력서 기입사항
            string studentNumber = "26031033";
            string studentName = "하석현";
            string studentAge = "2001.02.02";
            string goal = "본인이 머리속에 그리고 있는 게임을 C#이라는 도화지에 덮어쓰거나 C#이라는 특별한 도화지를 활용해 재미있는 게임을 그리고 싶습니다.";
            string Attitude = "2학기에는 그래픽, 객체지향프로그래밍, 게임창작, 게임분석, 게임기획까지 게임을 만드는 능력에 관한 많은 분야의 수업을 듣는 만큼 게임기획자로서의 역량을 키우는 시간이 될 것 같습니다. 나라는 캐릭터의 레벨을 개성있는 특성을 찍어가며 키워가고 싶습니다.";

            //객체지향프로그래밍 이력서
            Console.WriteLine("==================객체지향프로그래밍 이력서==================");
            Console.WriteLine("");

            Console.WriteLine("(1) 생년월일과 학번, 이름");
            Console.WriteLine($" 생년월일 : {studentAge}");
            Console.WriteLine($" 학번 : {studentNumber}");
            Console.WriteLine($" 이름 : {studentName}");


            Console.WriteLine("");

            Console.WriteLine("(2) 이 수업에 참여하면서 얻고 싶은 것");
            Console.WriteLine(goal);

            Console.WriteLine("");

            Console.WriteLine("(3) 2학기에 임하는 자세와 다짐");
            Console.WriteLine(Attitude);
        }
    }
}
