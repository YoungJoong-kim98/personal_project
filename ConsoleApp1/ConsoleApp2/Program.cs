﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {

        public class Item
        {
            public string Name { get; set; } // 아이템 이름
            public string Description { get; set; } // 아이템 설명
            public int EffectValue { get; set; }  // 아이템 효과
            public bool IsEquipped { get; set; }  // 아이템 장착 여부
            public string ItemType { get; set; } // 아이템 타입
            public float ItemGold { get; set; } // 아이템 금액


            public Item(string name, string description, int effectValue, string Type, float Gold)
            {
                //아이템 객체 생성 때 정보 등록
                Name = name;
                Description = description;
                EffectValue = effectValue;
                ItemType = Type;
                IsEquipped = false;
                ItemGold = Gold;
            }

            //public void Use(Character character)
            //{
            //    // 아이템의 효과 적용 
            //    character.Health += EffectValue;
            //}
        }
        class Store
        {
            public List<Item> ItemsForSale { get; set; } // 상점에 등록된 아이템 리스트
            public List<Item> ItemBuy {  get; set; }

            public Store()
            {
                ItemsForSale = new List<Item>(); // 아이템 리스트 초기화
                ItemBuy = new List<Item>(); //구매한 아이템 리스트 초기화
            }

            // 아이템을 상점에 등록하는 메서드
            public void AddItemToStore(Item item)
            {
                ItemsForSale.Add(item);
            }


            // 상점에서 아이템을 구매하는 메서드
            public void BuyItem(Character character, int itemIndex) //캐릭터 객체와 , 아이템 인덱스 값을 매개변수로 받음
            {
                if (itemIndex >= 0 && itemIndex < ItemsForSale.Count) // 아이템 인덱스 번호가 0 이상이고 아이템의 길이 보다 작은 값이 들어오면
                {
                    Item selectedItem = ItemsForSale[itemIndex];

                    if (character.Gold >= selectedItem.ItemGold) // 보유 골드 체크
                    {
                        character.Gold -= selectedItem.ItemGold;
                        character.Inventory.Add(selectedItem);
                        ItemBuy.Add(selectedItem); //구매한 아이템 리스트 추가
                        Console.WriteLine($"{selectedItem.Name}을(를) 구매하였습니다!");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다!");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                }
            }

            // 상점 아이템 출력 메서드
            public void ShowStoreItems()
            {
                Console.WriteLine("[상점 아이템 목록]");
                if (ItemsForSale.Count == 0)
                {
                    Console.WriteLine("상점에 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < ItemsForSale.Count; i++)
                    {
                        var item = ItemsForSale[i];
                        Console.WriteLine($"{i + 1}. {item.Name} | 가격: {item.ItemGold} 골드 | {item.Description} ");
                    }
                }
            }
        }

        class Character
        {
            public string Name { get; set; }
            public int Lv { get; set; } // 레벨
            public string Job { get; set; } // 직업
            public float Attack { get; set; } // 공격력
            public float Defense { get; set; } // 방어력
            public float Health { get; set; } // 체력
            public float Gold { get; set; } // 돈
            public List<Item> Inventory { get; set; } // 아이템 리스트
            public Item EquippedItem { get; set; } // 장착된 아이템

            public Character(string nickname)
            {
                //캐릭터 객체 생성 이름 빼고 기본값 저장
                Name = nickname;
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
                if (item != null && !item.IsEquipped) //아이템이 null이 아니고 장착여부가 false 이면 실행
                {
                    item.IsEquipped = true;
                    EquippedItem = item;
                    if (item.ItemType == "공격")
                    {
                        Attack += item.EffectValue;  // 장착 시 공격력 증가
                    }
                    else if (item.ItemType == "방어")
                    {
                        Defense += item.EffectValue; // 장착 시 방어력 증가
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
            public void ShowStatus()
            {
                int totalAttackBonus = 0; //전체 공격 보너스
                int totalDefenseBonus = 0; // 전체 방어 보너스
                foreach (var item in Inventory) // 내 인벤토리에서 아이템을 꺼내옴 item에 저장
                {
                    if (item.ItemType == "공격" && item.IsEquipped) totalAttackBonus += item.EffectValue; // 아이템이 공격이고 장착하였으면 전체 보너스 공격에 더해줌
                    if (item.ItemType == "방어" && item.IsEquipped) totalDefenseBonus += item.EffectValue; // 아이템이 방어이고 장착하였으면 전체 보너스 방어에 더해줌
                }
                //캐릭터 정보
                Console.WriteLine("\n상태 보기"); 
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine($"Name :{Name}"); 
                Console.WriteLine($"Lv. {Lv:00}");
                Console.WriteLine($"Chad ( {Job} )");
                Console.WriteLine($"공격력 : {Attack} {(totalAttackBonus > 0 ? $"(+{totalAttackBonus})" : "")}");
                Console.WriteLine($"방어력 : {Defense} {(totalDefenseBonus > 0 ? $"(+{totalDefenseBonus})" : "")}");
                Console.WriteLine($"체력 : {Health}");
                Console.WriteLine($"Gold : {Gold} G");
                Console.WriteLine("\n0. 나가기");
            }
        }
        static void Main()
        {
            string nickname;

            while (true)
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 입력해주세요.");
                nickname = Console.ReadLine();
                Console.WriteLine($"입력하신 이름은 {nickname}입니다.\n1.저장 \n2.아니오");
                int save = int.Parse(Console.ReadLine());
                if (save == 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("취소되었습니다.");
                    continue;
                }

            }


            Character MyCharacter = new Character(nickname); // 캐릭터 객체 생성
            Store myStore = new Store(); // 상점 객체 생성

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            //아이템 객체 생성
            Item Armor = new Item("Armor", "수련에 도움을 주는 갑옷입니다.", 5, "방어", 1000); 
            Item IronArmor = new Item("IronArmor", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, "방어", 2000);
            Item SpartaArmor = new Item("SpartaArmor", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, "방어", 3000);

            Item Sword = new Item("Sword", "쉽게 볼 수 있는 낡은 검입니다.", 2, "공격", 600);
            Item BronzeAx = new Item("BronzeAx", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, "공격", 1500);
            Item SpartSpear = new Item("SpartSpear", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, "공격", 2000);
            myStore.AddItemToStore(Armor);
            myStore.AddItemToStore(IronArmor);
            myStore.AddItemToStore(SpartaArmor);
            myStore.AddItemToStore(Sword);
            myStore.AddItemToStore(BronzeAx);
            myStore.AddItemToStore(SpartSpear);


            MyCharacter.Inventory.Add(IronArmor);  // IronArmor 아이템을 인벤토리에 추가
            MyCharacter.Inventory.Add(SpartSpear);
            MyCharacter.Inventory.Add(Sword);

            while (true)
            {
                int Num = 0;
                Console.WriteLine("1.상태 보기 \n2.인벤토리\n3.상점\n4.던전\n5.휴식\n원하는 행동을 입력해주세요.");
                string select = Console.ReadLine();
                bool IsNum = int.TryParse(select, out Num);

                if (IsNum)
                {
                    if (Num == 1)
                    {
                        MyCharacter.ShowStatus();
                        
                        while(true)
                        {
                            select = Console.ReadLine();
                            IsNum = int.TryParse(select, out Num);
                            if (IsNum)
                            {
                                if(Num == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("0을 입력하세요.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
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
                            {   //아이템 번호 및 삼항 연산자를 사용하여 아이템 장착 여부에 따라 다른 값이 실행 , 아이템 이름 , 아이템 효과 , 설명
                                Console.WriteLine($" | {index} {(item.IsEquipped ? "[E]" : "")} {item.Name} | {item.EffectValue} | {item.Description} ");
                                index++;
                            }

                            Console.WriteLine("장착할 아이템을 선택하세요. 0.나가기");
                            int itemChoice;
                            //아이템 값을 숫자로 입력 받고 그 숫자 값이 1보다 크고 아이템의 갯수(길이) 보다 작으면
                            if (int.TryParse(Console.ReadLine(), out itemChoice) && itemChoice >= 1 && itemChoice <= MyCharacter.Inventory.Count)
                            {
                                var selectedItem = MyCharacter.Inventory[itemChoice - 1];
                                //내 캐릭터 인벤토리에 있는 아이템 값 -1 을 한 아이템을 저장 
                                if (!selectedItem.IsEquipped) //이 아이템이 장착여부가 false 이면 
                                {
                                    MyCharacter.EquipItem(selectedItem); //아이템 장착 매서드 실행
                                }
                                else
                                {
                                    Console.WriteLine("이 아이템은 이미 장착되어 있습니다."); //아니면 장착되어있으니 다음과 같은 메세지 출력
                                }
                            }
                            else if(itemChoice == 0)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                    }
                    else if (Num == 3)
                    {
                        Console.WriteLine($"[보유 골드]\n{MyCharacter.Gold}");

                        myStore.ShowStoreItems();
                        Console.WriteLine("\n구매할 아이템 번호를 입력하세요 (0: 나가기).");
                        int buyChoice;
                        if (int.TryParse(Console.ReadLine(), out buyChoice) && buyChoice >= 1 && buyChoice <= myStore.ItemsForSale.Count)
                        {
                            myStore.BuyItem(MyCharacter, buyChoice - 1); //아이템 구매 메서드 실행
                        }
                        else if (buyChoice != 0)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }

                    }
                    else if(Num == 4)
                    {

                    }
                    else if(Num == 5)
                    {
                        Console.WriteLine($"500G 를 내면 체력을 회복할 수 있습니다. (보유 골드{MyCharacter.Gold} G)");
                        Console.WriteLine("1.휴식하기\n0.나가기");
                        Num = int.Parse(Console.ReadLine());
                        while(true)
                        {
                            if(Num == 1)
                            {
                                Console.WriteLine("우와 맛있는 음식이다.!");
                                Thread.Sleep(1000);
                                Console.WriteLine("우와 따뜻한 목욕탕이야!");
                                Thread.Sleep(1000);
                                Console.WriteLine("푹신 푹신한 침대~~~");
                                Thread.Sleep(1000);
                                Console.WriteLine("쿨쿨쿨~~~");
                                Console.WriteLine("체력이 50 회복되었습니다.");
                                MyCharacter.Health += 50;
                                if(MyCharacter.Health>=100)
                                {
                                    MyCharacter.Health = 100;
                                }
                                Thread.Sleep(1000);
                                Console.WriteLine($"현재체력 : {MyCharacter.Health}");
                                break;
                            }
                            else if(Num == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("0 또는 1을 입력해주세요.");
                            }
                        
                                    
                         }

                    }
                    else
                    {
                        Console.WriteLine("숫자 1 ~ 5 을 입력해주세요.\n");
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
