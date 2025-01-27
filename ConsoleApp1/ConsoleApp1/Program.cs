namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("첫 번째 숫자를 입력하세요.");
            int Num1 = int.Parse(Console.ReadLine());
            Console.Write("두 번째 숫자를 입력하세요.");
            int Num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("두 수의 사칙연산 결과입니다.");
            Console.WriteLine("더하기 " + (Num1 + Num2));
            Console.WriteLine("빼기 " + (Num1 - Num2));
            Console.WriteLine("곱하기 " + Num1 * Num2);
            Console.WriteLine("나누기 " + Num1 / Num2);
            Console.WriteLine("나머지 " + Num1 % Num2);
        }
    }
}
