using System.Text.Json.Serialization;

namespace ConsoleApp2
{
    class Program
    {
        class Item
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int EffectValue { get; set; }  // 예: 체력 회복, 공격력 증가

            public Item(string name, string description, int effectValue)
            {
                Name = name;
                Description = description;
                EffectValue = effectValue;
            }

            public void Use(Character character)
            {
                // 아이템의 효과 적용 (예: 체력 회복)
                character.Health += EffectValue;
            }
        }

        class Character
        {
            public int Lv { get; set; } // 레벨
            public string Job { get; set; }
            public float Attack { get; set; } // 공격력
            public float Defense { get; set; } // 방어력
            public float Health { get; set; } // 체력
            public float Gold { get; set; }
            public List<Item> Inventory { get; set; }

            public Character()
            {
                Lv = 1;
                Job = "전사";
                Attack = 10;
                Defense = 5;
                Health = 100;
                Gold = 1500;
                Inventory = new List<Item>();
      
            }


        }


        static void Main()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Character MyCharacter = new Character();
            Item armor = new Item("armor", "무쇠로 만들어져 튼튼한 갑옷입니다.",5);
            MyCharacter.Inventory.Add(armor);  // armor 아이템을 인벤토리에 추가


            while (true)
            {
                int Num = 0;
                Console.WriteLine("1.상태 보기 \n2.인벤토리\n3.상점\n\n원하는 행동을 입력해주세요.");
                string select = Console.ReadLine();
                bool IsNum = int.TryParse(select, out Num);

                if (IsNum)
                {
                    if (Num == 1)
                    {
                        Console.WriteLine("Lv. " + MyCharacter.Lv.ToString("00"));
                        Console.WriteLine($"Chad ( {MyCharacter.Job} )");
                        Console.WriteLine("공격력 : " + MyCharacter.Attack);
                        Console.WriteLine("방어력 : " + MyCharacter.Defense);
                        Console.WriteLine("체력 : " + MyCharacter.Health);
                        Console.WriteLine("Gold : " + MyCharacter.Gold);
                        Console.WriteLine("\n\n0. 나가기");
                        select = Console.ReadLine();
                        IsNum = int.TryParse(select, out Num);
                        if(IsNum)
                        {
                            if (Num == 0) continue;
                            else Console.WriteLine("숫자를 입력하세요.");
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        
                    }
                    else if (Num == 2)
                    {
                        Console.WriteLine("[아이템 목록]");
                        if (MyCharacter.Inventory.Count == 0)
                        {
                            Console.WriteLine("인벤토리 아이템이 없습니다.");
                        }
                        else
                        {
                            foreach(var item in MyCharacter.Inventory)
                                {
                                    Console.WriteLine($"{item.Name}");
                                }
                        }
                                                 
                        

                    }
                    else if (Num == 3)
                    {

                    }
                    else
                    {
                        Console.WriteLine("숫자 1 ~ 3 을 입력해주세요.\n");
                    }

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다\n");
                }








            }
        }
    }
}
