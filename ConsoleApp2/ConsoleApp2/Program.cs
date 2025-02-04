namespace ConsoleApp2
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            List<int> numbers = new List<int> { 5, 2, 8, 3, 9 };

            var sortedNumbers = numbers.OrderBy(n => n); // 오름차순 정렬

            Console.WriteLine(string.Join(", ", sortedNumbers)); // 출력: 2, 3, 5, 8, 9
        }
    }



}



