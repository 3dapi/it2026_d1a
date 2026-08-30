using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace 이동윤자기이력서
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 글자 색상 초록색
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("========== 자기 이력서 ==========");
            Console.WriteLine();

            Console.WriteLine("[ 인적 사항 ]");
            Console.WriteLine("이름 : 이동윤 ");
            Console.WriteLine("생년월일 : 070428  ");
            Console.WriteLine("성별 : 남");
            Console.WriteLine("나이 : 만 19세");
            Console.WriteLine("주소 : 군포시 산본 ");
            Console.WriteLine();

            Console.WriteLine("[ 학력 사항 ]");
            Console.WriteLine("학교 : 한국아이티전문학교");
            Console.WriteLine("학과 : 게임기획");
            Console.WriteLine("학년 : 1학년");
            Console.WriteLine();

            Console.WriteLine("[ 자격증 ]");
            Console.WriteLine("자격증 : 없음 ");
            Console.WriteLine();

            Console.WriteLine("[ 기타 사항 ]");
            Console.WriteLine("특기 : 열정열정열정");
            Console.WriteLine("희망 직무 : 게임기획자");

            Console.WriteLine();
            Console.WriteLine("===============================");

            Console.ReadLine();
        }
    }
}