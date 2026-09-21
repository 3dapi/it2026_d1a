using System;
using System.Collections.Generic;

namespace JeonSeunghyoResume
{
    public class SchoolRecord
    {
        public string Period { get; set; }
        public string SchoolName { get; set; }

        public SchoolRecord(string period, string schoolName)
        {
            Period = period;
            SchoolName = schoolName;
        }
    }

    public class GameWork
    {
        public string TeamName { get; set; }
        public string GameName { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }

        public GameWork(string teamName, string gameName, string period, string status)
        {
            TeamName = teamName;
            GameName = gameName;
            Period = period;
            Status = status;
        }
    }

    public class Portfolio
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Aspiration { get; set; }

        public List<SchoolRecord> SchoolHistory { get; set; } = new List<SchoolRecord>();
        public List<GameWork> WorkHistory { get; set; } = new List<GameWork>();

        public void Print()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("                    이 력 서                      ");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            Console.WriteLine($"  이름     : {Name,-10} 생년월일 : {BirthDate}");
            Console.WriteLine($"  성별     : {Gender,-10} 나이     : 만 {Age}세");
            Console.WriteLine($"  희망직무 : {Aspiration}");
            Console.WriteLine("--------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [ 학력 사항 ]");
            Console.ResetColor();
            foreach (var s in SchoolHistory)
            {
                Console.WriteLine($"    {s.Period,-18} {s.SchoolName}");
            }
            Console.WriteLine("--------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [ 프로젝트 경력 ]");
            Console.ResetColor();
            foreach (var w in WorkHistory)
            {
                Console.WriteLine($"    게임명  : {w.GameName}  ({w.Status})");
                Console.WriteLine($"    팀      : {w.TeamName}");
                Console.WriteLine($"    기간    : {w.Period}");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Portfolio myPortfolio = new Portfolio
            {
                Name = "전승효",
                BirthDate = "070113",
                Gender = "남",
                Age = 19,
                Aspiration = "게임 기획자"
            };

            myPortfolio.SchoolHistory.Add(new SchoolRecord("2014 ~ 2019", "응암초등학교"));
            myPortfolio.SchoolHistory.Add(new SchoolRecord("2020 ~ 2022", "충암중학교"));
            myPortfolio.SchoolHistory.Add(new SchoolRecord("2023 ~ 2025", "대성고등학교"));
            myPortfolio.SchoolHistory.Add(new SchoolRecord("2026 ~ 재학중", "한국IT전문학교"));

            myPortfolio.WorkHistory.Add(new GameWork("더단백팀", "폰더킹", "04/11 ~ 04/23", "완성"));
            myPortfolio.WorkHistory.Add(new GameWork("-", "뽀더", "04/24 ~", "잠정중단"));

            myPortfolio.Print();
        }
    }
}
