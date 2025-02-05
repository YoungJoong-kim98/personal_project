using System;
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
            public bool WeaponItem {  get; set; } //공격 아이템 장착 여부
            public bool TopItem { get; set; } // 상의 아이템 장착여부
            public string ItemType { get; set; } // 아이템 타입
            public float ItemGold { get; set; } // 아이템 금액
            public bool ItemBuy; // 아이템 구매 여부

            public Item(string name, string description, int effectValue, string Type, float Gold)
            {
                //아이템 객체 생성 때 정보 등록
                Name = name;
                Description = description;
                EffectValue = effectValue;
                ItemType = Type;
                IsEquipped = false;
                TopItem = false;
                WeaponItem = false;
                
                ItemGold = Gold;

                ItemBuy = false; // 상점 아이템 구매 여부
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

                        
            public Store()
            {
                ItemsForSale = new List<Item>(); // 아이템 리스트 초기화
                               
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
                    Item selectedItem = ItemsForSale[itemIndex]; // 상점 리스트에 있는 내가 선택한 인덱스번호(이미 매개변수 전달 시 -1을 해줌)를 값을 저장

                    if (character.Gold >= selectedItem.ItemGold) // 보유 골드 체크
                    {
                        character.Gold -= selectedItem.ItemGold; // 선택한 아이템 골드에서 내 골드를 뺌
                        character.Inventory.Add(selectedItem); //내 인벤토리 리스트에 추가
                        Console.WriteLine($"{selectedItem.Name}을(를) 구매하였습니다!");
                        selectedItem.ItemBuy = true; // 상점에 아이템 구매 여부를 true 바꿔줌
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
                        var item = ItemsForSale[i]; // 0번째 인덱스부터 상점리스트에 있는 갯수만큼 상점에 출력
                        Console.WriteLine($"{i + 1}. {item.Name} | {item.ItemType}+{item.EffectValue} | {item.Description} | {(item.ItemBuy ? "구매완료" : item.ItemGold+" G")}");
                    }
                }
            }
        }

        static void Dungeon(Character MyCharacter, ref int ClearCount, string DungeonLevel, int RecommendedDefense)
        {
            if (DungeonLevel == "Easy") // 쉬운 던전
            {
                Random random = new Random(); // 랜덤 값 지정
                if (MyCharacter.Defense < RecommendedDefense)
                {

                    if (random.Next(0, 10) < 5) // 랜덤값으로 40% 확률로 클리어 가능하게 함.
                    {
                        Console.WriteLine("던전 클리어!");
                        ClearCount += 1; // 클리어 횟수 나중에 레벨업에 영향을 미침
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health -= (random.Next(20, 36) + (RecommendedDefense - MyCharacter.Defense)); // 권장 방어력 보다 낮으니 권장 방어력에서 내 방어력 빼기 한 값을 빼줌
                        Console.WriteLine($"{MyCharacter.Health}");
                        Console.Write($"Gold {MyCharacter.Gold} G =>");
                        MyCharacter.Gold += 1000 + MyCharacter.Attack * random.Next(10, 20); ; // 쉬운던전 클리어 보상 1000+공격력의 랜덤값 10~20%
                        Console.WriteLine($"{MyCharacter.Gold} G");
                        Thread.Sleep(1000);
                        

                    }
                    else
                    {
                        Console.WriteLine("던전클리어를 실패하였습니다.");
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health /= 2; //체력 감소
                        Console.WriteLine($"{MyCharacter.Health}");
                        Thread.Sleep(1000);

                    }
                }
                else
                {
                    Console.WriteLine("던전 클리어!");
                    ClearCount += 1; //클리어 횟수 증가
                    Thread.Sleep(1000);
                    Console.Write($"체력 {MyCharacter.Health} =>");
                    MyCharacter.Health -= (random.Next(20, 36) - (MyCharacter.Defense - RecommendedDefense));  //체력 감소 내 방어력이 더 높으니 권장 방어력 - 한 값을 더해줌
                    Console.WriteLine($"{MyCharacter.Health}");
                    Console.Write($"Gold {MyCharacter.Gold} G =>");
                    MyCharacter.Gold += 1000 + MyCharacter.Attack * random.Next(10, 20);  // 쉬운던전 클리어 보상 1000+공격력의 랜덤값 10~20%
                    Console.WriteLine($"{MyCharacter.Gold} G");
                    Thread.Sleep(1000);
                }

            }
            else if (DungeonLevel == "Normal")
            {
                Random random = new Random();
                if (MyCharacter.Defense < RecommendedDefense)
                {

                    if (random.Next(0, 10) < 5)
                    {
                        Console.WriteLine("던전 클리어!");
                        ClearCount += 1;
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health -= (random.Next(20, 36) + (RecommendedDefense - MyCharacter.Defense)); // 권장 방어력 보다 낮으니 권장 방어력에서 내 방어력 빼기 한 값을 빼줌
                        Console.WriteLine($"{MyCharacter.Health}");
                        Console.Write($"Gold {MyCharacter.Gold} G =>");
                        MyCharacter.Gold += 1700 + MyCharacter.Attack * random.Next(10, 20); ;
                        Console.WriteLine($"{MyCharacter.Gold} G");
                        Thread.Sleep(1000);

                    }
                    else
                    {
                        Console.WriteLine("던전클리어를 실패하였습니다.");
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health /= 2; //체력 감소
                        Console.WriteLine($"{MyCharacter.Health}");
                        Thread.Sleep(1000);

                    }
                }
                else
                {
                    Console.WriteLine("던전 클리어!");
                    ClearCount += 1; //클리어 횟수 증가
                    Thread.Sleep(1000);
                    Console.Write($"체력 {MyCharacter.Health} =>");
                    MyCharacter.Health -= (random.Next(20, 36) - (MyCharacter.Defense - RecommendedDefense));  //체력 감소 내 방어력이 더 높으니 권장 방어력 - 한 값을 더해줌
                    Console.WriteLine($"{MyCharacter.Health}");
                    Console.Write($"Gold {MyCharacter.Gold} G =>");
                    MyCharacter.Gold += 1700 + MyCharacter.Attack * random.Next(10, 20);  // 일반던전 클리어 보상 1700+공격력의 랜덤값 10~20%
                    Console.WriteLine($"{MyCharacter.Gold} G");
                    Thread.Sleep(1000);
                }
            }
            else if (DungeonLevel == "Hard")
            {
                Random random = new Random();
                if (MyCharacter.Defense < RecommendedDefense)
                {

                    if (random.Next(0, 10) < 5)
                    {
                        Console.WriteLine("던전 클리어!");
                        ClearCount += 1;
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health -= (random.Next(20, 36) + (RecommendedDefense - MyCharacter.Defense)); // 권장 방어력 보다 낮으니 권장 방어력에서 내 방어력 빼기 한 값을 빼줌
                        Console.WriteLine($"{MyCharacter.Health}");
                        Console.Write($"Gold {MyCharacter.Gold} G =>");
                        MyCharacter.Gold += 2500 + MyCharacter.Attack * random.Next(10, 20); ;
                        Console.WriteLine($"{MyCharacter.Gold} G");
                        Thread.Sleep(1000);

                    }
                    else
                    {
                        Console.WriteLine("던전클리어를 실패하였습니다.");
                        Thread.Sleep(1000);
                        Console.Write($"체력 {MyCharacter.Health} =>");
                        MyCharacter.Health /= 2; //체력 감소
                        Console.WriteLine($"{MyCharacter.Health}");
                        Thread.Sleep(1000);

                    }
                }
                else
                {
                    Console.WriteLine("던전 클리어!");
                    ClearCount += 1; //클리어 횟수 증가
                    Thread.Sleep(1000);
                    Console.Write($"체력 {MyCharacter.Health} =>");
                    MyCharacter.Health -= (random.Next(20, 36) - (MyCharacter.Defense - RecommendedDefense));  //체력 감소 내 방어력이 더 높으니 권장 방어력 - 한 값을 더해줌
                    Console.WriteLine($"{MyCharacter.Health}");
                    Console.Write($"Gold {MyCharacter.Gold} G =>");
                    MyCharacter.Gold += 2500 + MyCharacter.Attack * random.Next(10, 20);  // 어려운 던전 클리어 보상 2500+공격력의 랜덤값 10~20%
                    Console.WriteLine($"{MyCharacter.Gold} G");
                    Thread.Sleep(1000);
                }
            }
            if (MyCharacter.Health > 0) // 플레이어 체력이 0 보다 크면 클리어횟수에 따른 레벨업과 공격력 , 방어력 증가
            {
                MyCharacter.Lv = ClearCount;
                MyCharacter.Attack += 0.5f;
                MyCharacter.Defense += 1;
                Console.WriteLine($"Level UP! 현재 레벨{MyCharacter.Lv + 1}");
                Console.WriteLine("능력치 공격력 0.5 방어력 1 증가");
                Console.WriteLine($"현재 공격력:{MyCharacter.Attack} , 현재 방어력:{MyCharacter.Defense}");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("플레이어가 죽었습니다."); // 플레이어 사망
            }


        }

        class Character
        {
            public string Name { get; set; } // 캐릭터 이름
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


                    if (item.ItemType == "공격력") // 아이템 타입이 공격력
                    {
                        // 인벤토리를 전부 뒤져서 장착여부와 공격아이템이 true인지를 확인 중복 장착을 방지하도록하기위해서 있을시 능력치 감소 및 해제
                        foreach (Item item2 in Inventory) 
                        {
                            if (item2.IsEquipped == true && item2.WeaponItem == true)
                            {
                                Attack -= item2.EffectValue;
                                item2.IsEquipped = false;
                                item2.TopItem = false;
                            }
                        }

                        item.WeaponItem = true; // 공격 아이템 장착
                        Attack += item.EffectValue;  // 장착 시 공격력 증가
                    }
                    else if (item.ItemType == "방어력")
                    {
                        foreach (Item item2 in Inventory)
                        {
                            if (item2.IsEquipped == true && item2.TopItem == true)
                            {
                                Defense -= item2.EffectValue;
                                item2.IsEquipped = false;
                                item2.TopItem = false;
                            }
                        }
                        item.TopItem = true; //방어 아이템 장착
                        Defense += item.EffectValue; // 장착 시 방어력 증가
                    }
                    item.IsEquipped = true;
                    EquippedItem = item;
                    Console.WriteLine($"{item.Name}을 장착했습니다.");
                }
                else
                {
                    Console.WriteLine("이 아이템은 이미 장착되었습니다.");
                }
            }

            //public void UnequipItem()
            //{
            //    if (EquippedItem != null)
            //    {
            //        EquippedItem.IsEquipped = false;
            //        Defense -= EquippedItem.EffectValue;  // 방어력 감소
            //        Console.WriteLine($"{EquippedItem.Name}을(를) 해제했습니다.");
            //        EquippedItem = null;
            //    }
            //    else
            //    {
            //        Console.WriteLine("장착된 아이템이 없습니다.");
            //    }
            //}

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
            int ClearCount = 0;
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
            Item Armor = new Item("Armor", "수련에 도움을 주는 갑옷입니다.", 5, "방어력", 1000);
            Item IronArmor = new Item("IronArmor", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, "방어력", 2000);
            Item SpartaArmor = new Item("SpartaArmor", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, "방어력", 3000);

            Item Sword = new Item("Sword", "쉽게 볼 수 있는 낡은 검입니다.", 2, "공격력", 600);
            Item BronzeAx = new Item("BronzeAx", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, "공격력", 1500);
            Item SpartSpear = new Item("SpartSpear", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, "공격력", 2000);
            //상점에 등록
            myStore.AddItemToStore(Armor);
            myStore.AddItemToStore(IronArmor);
            myStore.AddItemToStore(SpartaArmor);
            myStore.AddItemToStore(Sword);
            myStore.AddItemToStore(BronzeAx);
            myStore.AddItemToStore(SpartSpear);


            //MyCharacter.Inventory.Add(IronArmor);  // IronArmor 아이템을 인벤토리에 추가
            //MyCharacter.Inventory.Add(SpartSpear);
            //MyCharacter.Inventory.Add(Sword);

            while (true)
            {
                if (MyCharacter.Health <= 0) //플레이어가 죽으면 바로 빠져나오도록 하는 코드
                {
                    break;
                }                
                int Num = 0;
                Console.WriteLine("1.상태 보기 \n2.인벤토리\n3.상점\n4.던전\n5.휴식\n원하는 행동을 입력해주세요.");
                string select = Console.ReadLine();
                bool IsNum = int.TryParse(select, out Num);

                if (IsNum)
                {
                    if (Num == 1)
                    {
                        MyCharacter.ShowStatus();

                        while (true)
                        {
                            select = Console.ReadLine();
                            IsNum = int.TryParse(select, out Num);
                            if (IsNum)
                            {
                                if (Num == 0)
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
                            else if (itemChoice == 0)
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


                        Console.WriteLine("\n1.아이템 구매\n2.아이템 판매\n0.나가기");
                        while (true)
                        {
                            select = Console.ReadLine();
                            IsNum = int.TryParse(select, out Num);
                            if (IsNum)
                            {
                                if (Num == 1)
                                {
                                    Console.WriteLine("아이템 구매를 선택하셨군요. 보기에서 원하는 아이템 번호를 입력하세요.");
                                    Console.WriteLine($"[보유 골드]\n{MyCharacter.Gold}");
                                    myStore.ShowStoreItems(); // 아이템 목록 보여주는 매서드 실행
                                    Console.WriteLine("\n0.나가기");
                                    int buyChoice;
                                    if (int.TryParse(Console.ReadLine(), out buyChoice) && buyChoice >= 1 && buyChoice <= myStore.ItemsForSale.Count)
                                    {
                                        myStore.BuyItem(MyCharacter, buyChoice - 1); //아이템 구매 메서드 실행
                                        break;
                                    }
                                    else if (buyChoice != 0)
                                    {
                                        Console.WriteLine("잘못된 입력입니다.");
                                    }
                                    else if (buyChoice == 0)
                                    {
                                        Console.WriteLine("상점을 나갑니다.");
                                        break;
                                    }

                                }
                                else if (Num == 2)
                                {
                                    Console.WriteLine("[내 아이템 목록]");
                                    if (MyCharacter.Inventory.Count == 0)
                                    {
                                        Console.WriteLine("인벤토리 아이템이 없습니다.");
                                    }
                                    else
                                    {
                                        int index = 1;
                                        foreach (var item in MyCharacter.Inventory)
                                        {
                                            //아이템 번호 및 이름 효과 설명 , 그리고 판매금액 원래 상점가 보다 15% 할인 판매

                                            Console.WriteLine($"  {index} | {(item.IsEquipped ? "[E]" : "")} | {item.Name} | {item.EffectValue} | {item.Description} | 판매금액 {item.ItemGold * 0.85}");
                                            index++;


                                        }

                                        Console.WriteLine("판매할 아이템을 번호를 입력하세요.");
                                        int itemChoice;
                                        if (int.TryParse(Console.ReadLine(), out itemChoice) && itemChoice >= 1 && itemChoice <= MyCharacter.Inventory.Count)
                                        {
                                            
                                            var selectedItem = MyCharacter.Inventory[itemChoice - 1];
                                            Console.WriteLine($"{selectedItem.Name}을(를) {selectedItem.ItemGold * 0.85} 골드에 판매하시겠습니까? (Y/N)");

                                            string confirm = Console.ReadLine();

                                            if (confirm.ToUpper() == "Y") //입력을  받아서 대문자로변경 Y이면 판매
                                            {
                                                if(selectedItem.ItemType == "공격력" && selectedItem.IsEquipped == true) 
                                                {
                                                    MyCharacter.Attack -= selectedItem.EffectValue; //아이템 장착 여부와 타입에 따른 능력치 감소
                                                }
                                                else if(selectedItem.ItemType == "방어력" && selectedItem.IsEquipped == true)
                                                {
                                                    MyCharacter.Defense -= selectedItem.EffectValue;
                                                }
                                                MyCharacter.Gold += (int)(selectedItem.ItemGold * 0.85); // 골드 추가
                                                selectedItem.ItemBuy = false; // 상점에서 아이템 구매 여부를 다시 false로 주면서 상점을 다시 입장하면 구매완료가 안뜨도록 로직설계
                                                MyCharacter.Inventory.RemoveAt(itemChoice - 1); // 아이템 삭제


                                                Console.WriteLine($"{selectedItem.Name}을(를) 판매했습니다! 현재 골드: {MyCharacter.Gold}");
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("판매를 취소했습니다.");
                                                break;
                                            }
                                        }


                                    }
                                }
                                else if (Num == 0)
                                {
                                    Console.WriteLine("상점을 나갑니다.");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("0 ~ 2의 숫자를 선택하세요.");
                            }
                        }




                    }
                    else if (Num == 4)
                    {
                        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                        Console.WriteLine("1.쉬운던전   | 방어력 5이상 권장");
                        Console.WriteLine("2.일반던전   | 방어력 11이상 권장");
                        Console.WriteLine("3.어려운던전   | 방어력 17이상 권장");
                        Console.WriteLine("0.나가기\n원하시는 행동을 입력해주세요.");
                        while (true)
                        {
                            select = Console.ReadLine();
                            IsNum = int.TryParse(select, out Num);
                            if (IsNum)
                            {
                                if (Num == 0)
                                {
                                    break;
                                }
                                else if (Num == 1)
                                {
                                    Dungeon(MyCharacter, ref ClearCount, "Easy", 5); //던전 캐릭터 객체 , 참조형태로 count값 전달 , 난이도 , 권장방어력
                                    break;
                                }

                                else if (Num == 2)
                                {
                                    Dungeon(MyCharacter, ref ClearCount, "Normal", 11);
                                    break;
                                }
                                else if (Num == 3)
                                {
                                    Dungeon(MyCharacter, ref ClearCount, "Hard", 17);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("0 ~ 3 숫자를 입력해주세요");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                            }
                        }



                    }
                    else if (Num == 5)
                    {
                        Console.WriteLine($"500G 를 내면 체력을 회복할 수 있습니다. (보유 골드{MyCharacter.Gold} G)");
                        Console.WriteLine("1.휴식하기\n0.나가기");
                        Num = int.Parse(Console.ReadLine());
                        while (true)
                        {
                            if (Num == 1) //비교적 간단한 로직 Sleep 함수를 추가하여 좀 더 생동감있게 로직을 구현
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
                                if (MyCharacter.Health >= 100)
                                {
                                    MyCharacter.Health = 100;
                                }
                                Thread.Sleep(1000);
                                Console.WriteLine($"현재체력 : {MyCharacter.Health}");
                                break;
                            }
                            else if (Num == 0)
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
            Console.WriteLine("프로그램을 다시 시작하십시오.");
        }
    }
}
