namespace _26031025_임주호_과제_자기이력서작성
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int Size_Paper = 60;
            int Cursor_Position_y = 2;

            // 인적 사항
            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("이름 : 임주호 ");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("생년 월일 : 071108 ");
            Cursor_Position_y += 2;

            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("성별 : 남 ");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("나이 : (만)18세 ");
            Cursor_Position_y += 2;

            // 학력 사항 출력
            Cursor_Position_y += 4;

            //좌측 항목명 출력
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y);
            Console.Write("학");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 1);// 제목행 출력을 위해 변수 삾을 건드리지 않고 직접 출력
            Console.Write("력");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 2);
            Console.Write("사");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 3);
            Console.Write("항");

            //상단 제목행 출력
            Cursor_Position_y -= 1;
            Console.SetCursorPosition(Size_Paper / 10 * 3, Cursor_Position_y);
            Console.Write("기간");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 10 * 4, Cursor_Position_y);
            Console.WriteLine("학교명");
            Cursor_Position_y += 2;

            // 세부 내용 출력
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("2014~2019");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("예원초등학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("2020~2022");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("예당중학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("2023~2025");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("동탄중앙고등학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("2026~");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("한국IT전문학교");
            Cursor_Position_y += 1;


            //프로젝트 경력 출력
            Cursor_Position_y += 4;

            //좌측 항목명 출력
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y);
            Console.Write("프");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 1);
            Console.Write("로");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 2);
            Console.Write("젝");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 3);
            Console.Write("트");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 4);
            Console.Write("경");
            Console.SetCursorPosition(Size_Paper / 10, Cursor_Position_y + 5);
            Console.Write("력");


            //상단 제목행 출력
            Cursor_Position_y -= 1;
            Console.SetCursorPosition(Size_Paper / 10 * 2, Cursor_Position_y);
            Console.Write("기간");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 10 * 6, Cursor_Position_y);
            Console.Write("프로젝트명");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 10 * 3, Cursor_Position_y);
            Console.Write("현황");
            Cursor_Position_y += 2;

            // 세부 내용 출력
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("03/20 ~ 05/25");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("몽환");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 10 * 3, Cursor_Position_y);
            Console.Write("중단");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 5, Cursor_Position_y);
            Console.Write("05/28 ~ 06/16");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 2, Cursor_Position_y);
            Console.Write("뽀더");
            Console.SetCursorPosition(Size_Paper - Size_Paper / 10 * 3, Cursor_Position_y);
            Console.Write("중단");
            Cursor_Position_y += 1;


            // 이력서 전체 틀 그리기
            Console.SetCursorPosition(0, 0);

            Console.WriteLine(new string('_',Size_Paper));
            for (int i = 1; i < Size_Paper / 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(Size_Paper - 1, i);
                Console.Write("|");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', Size_Paper));
        }
    }
}
