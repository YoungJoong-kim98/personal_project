namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("안녕하세요 BMI 수치를 알려드리겠습니다.");
            Console.Write("체중을 Kg 단위로 입력하세요.");
            float Weight = float.Parse(Console.ReadLine());
            Console.Write("키를 Cm 단위로 입력하세요.");
            float Height = float.Parse(Console.ReadLine())/100;
            float BMI = Weight / (Height * Height); // BMI 계산 공식 체중 / 신장제곱
            Console.WriteLine(BMI);

        }
    }
}
