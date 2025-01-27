namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("다음은 섭씨온도를 화씨온도로 변환하는 프로그램입니다. 섭씨온도를 입력해주세요.");
            float Temperature = float.Parse(Console.ReadLine());

            float F_Temperature = (float)(Temperature * 1.8) + 32;
            Console.WriteLine($"{Temperature}C를 화씨온도로 바꾸면 {F_Temperature}F입니다.");
        }
    }
}
