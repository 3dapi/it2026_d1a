using System.Xml.Linq;

namespace Loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //loop
            //for
            //while
            //do-while
            //foreach

            //초기 조건 식, 유효성, 스텝
            //for (int i = 0; i < 100; ++i)
            //{
            //    Console.WriteLine(i);
            //}

            //int i = 0;
            //do
            //{
            //    if (!(i < 100))
            //        break;
            //    Console.WriteLine(i);
            //    ++i;
            //} 
            //while (false);

            //List<int> myArray = new();
            //for (int j = 1; j < 100; ++j)
            //{
            //    myArray.Add(j);
            //}

            //int sum = 0;
            //int i = 0;
            //while (i < myArray.Count)
            //{
            //    sum += myArray[i];
            //    i++;
            //}
            //Console.WriteLine($"1~100 sum :{sum}");

            //한글 문자 출력
            for(var k = '가'; k<= '힣'; ++k)
                Console.Write(k);

            Console.WriteLine();


        }
    }
}
