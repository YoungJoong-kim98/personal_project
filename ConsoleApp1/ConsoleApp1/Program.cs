using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    internal class Program
    {
        // 부모 클래스
        public class Animal
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Eat()
            {
                Console.WriteLine("Animal is eating.");
            }

            public void Sleep()
            {
                Console.WriteLine("Animal is sleeping.");
            }
        }

        // 자식 클래스
        public class Dog
        {

        }

        public class Cat
        {

        }

               

        static void Main(string[] args)
        {

       
            
        }
    }
}
