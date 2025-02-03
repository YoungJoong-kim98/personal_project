using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConsoleApp2
{
    class Program
    {
        class Item
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int EffectValue { get; set; }  // 예: 체력 회복, 공격력 증가
            public bool IsEquipped { get; set; }  // 아이템 장착 여부
            public string ItemType { get; set; }

            public Item(string name, string description, int effectValue ,string Type)
            {
                Name = name;
                Description = description;
                EffectValue = effectValue;
                ItemType = Type;
                IsEquipped = false;
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
            public string Job { get; set; } // 직업
            public float Attack { get; set; } // 공격력
            public float Defense { get; set; } // 방어력
            public float Health { get; set; } // 체력
            public float Gold { get; set; }
            public List<Item> Inventory { get; set; }
            public Item EquippedItem { get; set; } // 장착된 아이템

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

            public void EquipItem(Item item)
            {
                if (item != null && !item.IsEquipped)
                {
                    item.IsEquipped = true;
                    EquippedItem = item;
                    if (item.ItemType == "공격")
                    {
                        Attack += item.EffectValue;  // 장착 시 방어력 증가
                    }
                    else if(item.ItemType == "방어")
                    {
                        Defense += item.EffectValue;
                    }
                    Console.WriteLine($"{item.Name}을 장착했습니다.");
                }
                else
                {
                    Console.WriteLine("이 아이템은 이미 장착되었습니다.");
                }
            }

            public void UnequipItem()
            {
                if (EquippedItem != null)
                {
                    EquippedItem.IsEquipped = false;
                    Defense -= EquippedItem.EffectValue;  // 방어력 감소
                    Console.WriteLine($"{EquippedItem.Name}을(를) 해제했습니다.");
                    EquippedItem = null;
                }
                else
                {
                    Console.WriteLine("장착된 아이템이 없습니다.");
                }
            }

            static void Main()
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

                Character MyCharacter = new Character();
                Item armor = new Item("armor", "무쇠로 만들어져 튼튼한 갑옷입니다.", 5 , "방어");
                Item spear = new Item("spear", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7,"공격");
                Item sword = new Item("sword", "쉽게 볼 수 있는 낡은 검입니다.", 2,"공격");
                MyCharacter.Inventory.Add(armor);  // armor 아이템을 인벤토리에 추가
                MyCharacter.Inventory.Add(spear);
                MyCharacter.Inventory.Add(sword);

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
                            if (IsNum)
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
                                int index = 1;
                                foreach (var item in MyCharacter.Inventory)
                                {
                                    Console.WriteLine($" | {index} {(item.IsEquipped ? "[E]" : "")} {item.Name} | {item.EffectValue} | {item.Description} ");
                                    index++;
                                }

                                Console.WriteLine("장착할 아이템을 선택하세요.");
                                int itemChoice;
                                if (int.TryParse(Console.ReadLine(), out itemChoice) && itemChoice >= 1 && itemChoice <= MyCharacter.Inventory.Count)
                                {
                                    var selectedItem = MyCharacter.Inventory[itemChoice - 1];

                                    if (!selectedItem.IsEquipped)
                                    {
                                        MyCharacter.EquipItem(selectedItem);
                                    }
                                    else
                                    {
                                        Console.WriteLine("이 아이템은 이미 장착되어 있습니다.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다.");
                                }
                            }
                        }
                        else if (Num == 3)
                        {
                            // 상점 로직을 추가할 수 있습니다.
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
}
