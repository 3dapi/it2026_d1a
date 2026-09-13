using System;

namespace _26311012_KimMinJun_Resume
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Size_Paper = 120;
            int Cursor_Position_y = 2;

            Console.SetCursorPosition(Size_Paper / 6, Cursor_Position_y - 1);
            Console.Write("이력서");

            Console.SetCursorPosition(0, Cursor_Position_y);
            Console.Write("----------------------------------------------");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 24, Cursor_Position_y + 1);
            Console.Write("이름 : 김민준");
            Console.SetCursorPosition(Size_Paper / 6, Cursor_Position_y + 1);
            Console.Write("생년월일 : 2007.05.19");
            Cursor_Position_y += 2;

            Console.SetCursorPosition(Size_Paper / 24, Cursor_Position_y);
            Console.Write("성별 : 남");
            Console.SetCursorPosition(Size_Paper / 6, Cursor_Position_y);
            Console.Write("나이 : 19");
            Cursor_Position_y += 1;

            Console.SetCursorPosition((Size_Paper / 24) - 4,Cursor_Position_y);
            Console.Write("전화번호 : 010-4077-7479");
            Cursor_Position_y += 2;

            Console.SetCursorPosition(0, Cursor_Position_y);
            Console.Write("----------------------------------------------");

            Cursor_Position_y += 4;

            Console.SetCursorPosition(Size_Paper / 30, Cursor_Position_y);
            Console.Write("학\n력\n사\n항");
            

            Cursor_Position_y -= 1;
            Console.SetCursorPosition(Size_Paper / 12 + 3, Cursor_Position_y);
            Console.WriteLine("학교명");
            Cursor_Position_y += 2;

            Console.SetCursorPosition(Size_Paper / 12, Cursor_Position_y - 1);
            Console.Write("흥도초등학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 12, Cursor_Position_y - 1);
            Console.Write("도래울중학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 12, Cursor_Position_y - 1);
            Console.Write("동패고등학교");
            Cursor_Position_y += 1;

            Console.SetCursorPosition(Size_Paper / 12, Cursor_Position_y - 1);
            Console.Write("한국IT직업전문학교");
        }
    }
}