using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _26031023_이진호_자기이력서작성
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine("               자기이력서");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[기본 정보]");
            Console.ResetColor();

            Console.WriteLine("이름          : 이진호");
            Console.WriteLine("생년월일      : 2004년 1월 3일");
            Console.WriteLine("MBTI          : ENTJ");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[학력 사항]");
            Console.ResetColor();

            Console.WriteLine("불당초등학교       (전학)");
            Console.WriteLine("서당초등학교       (졸업)");
            Console.WriteLine("불당중학교         (졸업)");
            Console.WriteLine("월봉고등학교       (졸업)");
            Console.WriteLine("백석대학교         (중퇴)");
            Console.WriteLine("한국IT직업전문학교 (재학 중)");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[사용 프로그램]");
            Console.ResetColor();

            Console.WriteLine("게임 엔진     : Unreal Engine, Unity");
            Console.WriteLine("그래픽 도구   : Photoshop, Illustrator, 3ds Max");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[취미]");
            Console.ResetColor();

            Console.WriteLine("독서, 게임, 클라이밍");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[자기소개]");
            Console.ResetColor();

            Console.WriteLine("저는 게임의 일관성을 가장 중요하게 생각하는 기획자 지망생 이진호입니다.");
            Console.WriteLine("전달하고자 하는 핵심 경험을 중심으로 모든 구조와 요소가 하나의 방향을 바라보고,");
            Console.WriteLine("특히 사소한 수치와 작은 연출까지 그 방향성을 놓치지 않는 일관된 설계를 보면 흥분됩니다.");
            Console.WriteLine("저 또한 디테일한 부분까지 일관되게 설계할 수 있는 기획자가 되고 싶습니다.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.ResetColor();
        }
    }
}
