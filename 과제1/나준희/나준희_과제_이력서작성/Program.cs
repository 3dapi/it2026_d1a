using System;
using System.Collections.Generic;

namespace MyResumeApp
{
    // 학력 정보를 담는 클래스
    public class Education
    {
        public string Period { get; set; }
        public string SchoolName { get; set; }

        public Education(string period, string schoolName)
        {
            Period = period;
            SchoolName = schoolName;
        }
    }

    // 프로젝트 경험을 담는 클래스
    public class Project
    {
        public string TeamName { get; set; }
        public string Role { get; set; }
        public string GameName { get; set; }
        public string Period { get; set; }

        public Project(string teamName, string role, string gameName, string period)
        {
            TeamName = teamName;
            Role = role;
            GameName = gameName;
            Period = period;
        }
    }

    // 이력서 본체 클래스
    public class Resume
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public List<Education> EducationHistory { get; set; } = new List<Education>();
        public List<Project> ProjectHistory { get; set; } = new List<Project>();

        // 이력서 출력 메서드
        public void PrintResume()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                    이 력 서                      ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"이름     : {Name} \t\t생년월일 : {BirthDate}");
            Console.WriteLine($"성별     : {Gender} \t\t\t나이     : 만 {Age}세");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("[학력]");
            foreach (var edu in EducationHistory)
            {
                Console.WriteLine($"{edu.Period,-15} {edu.SchoolName}");
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("[프로젝트 경력]");
            foreach (var proj in ProjectHistory)
            {
                Console.WriteLine($"팀명   : {proj.TeamName}");
                Console.WriteLine($"게임명 : {proj.GameName}");
                Console.WriteLine($"업무   : {proj.Role}");
                Console.WriteLine($"기간   : {proj.Period}");
                Console.WriteLine();
            }
            Console.WriteLine("==================================================");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 이력서 객체 생성 및 기본 정보 입력
            Resume myResume = new Resume
            {
                Name = "나준희",
                BirthDate = "070608",
                Gender = "남",
                Age = 19
            };

            // 학력 데이터 추가
            myResume.EducationHistory.Add(new Education("2014 ~ 2019", "안산초등학교"));
            myResume.EducationHistory.Add(new Education("2020 ~ 2022", "임학중학교"));
            myResume.EducationHistory.Add(new Education("2023 ~ 2025", "영선고등학교"));
            myResume.EducationHistory.Add(new Education("2026 ~ 재학중", "한국it전문학교"));

            // 프로젝트 데이터 추가 (오타 '레벨디장인' -> '레벨디자인' 수정)
            myResume.ProjectHistory.Add(new Project(
                "아무개",
                "서브 기획, 일정조율, 레벨디자인",
                "hallway",
                "03/20 ~ 07/11"
            ));
            myResume.ProjectHistory.Add(new Project(
                "신데렐라",
                "서브 기획",
                "여우창",
                "05/28 ~ 진행중"
            ));

            // 출력 실행
            myResume.PrintResume();
        }
    }
}