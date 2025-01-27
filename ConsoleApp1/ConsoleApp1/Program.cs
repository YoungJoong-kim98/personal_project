namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("이름을 입력해주세요.");
            string Name = Console.ReadLine();
            Console.WriteLine("나이를 입력해주세요.");
            string Age = Console.ReadLine();

            Console.WriteLine($"나의 이름은 {Name}이고 나이는{Age}입니다.");
        }
    }
}
