using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] com = new int[3] {0,0,0}; // 컴퓨터 랜덤 값을 저장 할 배열
            int[] user = new int[3]; // 사용자가 입력할 값을 저장 할 배열
            int count = 0; // 횟수를 저장 할 변수
            int z = 0; // while 문에 쓰일 변수
            while (true) // 중복 숫자가 들어가지 못하도록 방지
            {
                
                com[z] = new Random().Next(1,10);
                z++;
                if(z>=3)
                {
                    if (com[0] == com[1] || com[0] == com[2] || com[1] == com[2])
                    {
                        z = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(com[i]); // 컴퓨터 랜덤 값 확인
            }



            while(true)
            {
                count++; // 횟수 
                int correct = 0; //맞춘 숫자 초기화
                Console.WriteLine("컴퓨터의 1~9까지 3개의 숫자를 맞춰보세요");
                for (int i = 0; i<3;i++)
                {
                    user[i] = int.Parse(Console.ReadLine());
                }

                for (int i = 0;i<3;i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (com[i] == user[j])
                        {
                            correct++;
                            break;
                        }
                    }
                }

                Console.WriteLine("시도" + count + " : " + correct + "개의 숫자를 맞추셨습니다.");

                if(correct == 3)
                {
                    Console.WriteLine("축하합니다.! 모든 숫자를 맞추셨습니다.!");
                    break;
                }
            }
            
        }
    }
}
