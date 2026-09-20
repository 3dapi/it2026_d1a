//9

Console.Write("태어난 연도를 입력하세요: ");
int year = int.Parse(Console.ReadLine());

switch (year % 12)
{
    case 0: Console.WriteLine("원숭이띠"); break;
    case 1: Console.WriteLine("닭띠"); break;
    case 2: Console.WriteLine("개띠"); break;
    case 3: Console.WriteLine("돼지띠"); break;
    case 4: Console.WriteLine("쥐띠"); break;
    case 5: Console.WriteLine("소띠"); break;
    case 6: Console.WriteLine("범띠"); break;
    case 7: Console.WriteLine("토끼띠"); break;
    case 8: Console.WriteLine("용띠"); break;
    case 9: Console.WriteLine("뱀띠"); break;
    case 10: Console.WriteLine("말띠"); break;
    case 11: Console.WriteLine("양띠"); break;
}

//10

Console.Write("현재 월을 입력하세요: ");
int month = int.Parse(Console.ReadLine());

if (month < 1 || month > 12)
{
    Console.WriteLine("1~12 사이의 월을 입력하세요.");
}
else if (month >= 3 && month <= 5)
{
    Console.WriteLine("봄");
}
else if (month >= 6 && month <= 8)
{
    Console.WriteLine("여름");
}
else if (month >= 9 && month <= 11)
{
    Console.WriteLine("가을");
}
else
{
    Console.WriteLine("겨울");
}

//11if (x > 10 && x < 20)
{
    Console.WriteLine("조건에 맞습니다.");
}

//12

//1
//빈 줄 하나 줄력

//2
//아무것도 출력되지 않는다

//3 
//100 출력


//13 

Console.Write("x는? ");
string? input13 = Console.ReadLine();
int x = int.Parse(input13);
string ans = (x % 2 == 0) ? "짝수" : "홀수";
Console.WriteLine("x는 " + ans + "입니다.");

//14번

Console.Write("학년은? ");
string? input14 = Console.ReadLine();
int level = int.Parse(input14);

switch (level)
{
    case 1:
        Console.WriteLine("수강해야 하는 전공 학점: 12학점");
        break;
    case 2:
        Console.WriteLine("수강해야 하는 전공 학점: 18학점");
        break;
    case 3:
        Console.WriteLine("수강해야 하는 전공 학점: 10학점");
        break;
    case 4:
        Console.WriteLine("수강해야 하는 전공 학점: 18학점");
        break;
    default:
        Console.WriteLine("1~4학년을 입력해주세요.");
        break;
}

//15

bool hasEggs = true; 
int milkCount;

if (hasEggs)
{
    milkCount = 6;
}
else
{
    milkCount = 1;
}

Console.WriteLine("우유를 " + milkCount + "개 샀다.");

