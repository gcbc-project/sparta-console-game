﻿using System.Text;

namespace SpartaConsoleGame
{
    internal class Inventory
    {
        public List<InventoryItem> Items { get; set; }
        public List<InventoryItem> EquipedItems { get { return Items.Where(item => item.IsEquiped).ToList(); } }


        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        public string GetItemsInfo(params ItemType[] itemTypes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var itemType in itemTypes)
            {
                // ItemType에 따라 필터링하여 해당 아이템들만 처리
                var itemsOfType = Items.Where(item => item.BaseItem.Type == itemType);

                foreach (var item in itemsOfType)
                {
                    sb.AppendLine(item.GetItemInfo());
                }
            }

            return sb.ToString();
        }

        public void EquipedItem(int itemIndex)
        {
            InventoryItem? findItem = Items.Find(item => item.IsEquiped && Items[itemIndex].BaseItem.Type == item.BaseItem.Type);
            Items[itemIndex].IsEquiped = true;
            if (findItem != null)
            {
                findItem.IsEquiped = false;
            }  
        }

        public void UsingItem(int itemIndex)
        {
            if (Items[itemIndex].BaseItem.Name == "HP 초급 포션")
            {
                if(DataManager.Instance.Player.Hp >= DataManager.Instance.Player.Stats.Hp)
                {
                    Console.WriteLine("체력이 이미 최대치에 도달했습니다.");
                }
                else
                {
                    int healAmount = 30;
                    int maxHp = DataManager.Instance.Player.Stats.Hp;

                    int newHp = DataManager.Instance.Player.Hp + healAmount;
                    DataManager.Instance.Player.Hp = Math.Min(newHp, maxHp);

                    Console.WriteLine($"체력을 {healAmount} 회복하였습니다. 현재 체력: {DataManager.Instance.Player.Hp}");
                }
            }
            else
            {
                if (DataManager.Instance.Player.Mp >= DataManager.Instance.Player.Stats.Mp)
                {
                    Console.WriteLine("마나가 이미 최대치에 도달했습니다.");
                }
                else
                {
                    int ManaAmount = 30;
                    int maxMp = DataManager.Instance.Player.Stats.Mp;

                    int newMp = DataManager.Instance.Player.Hp + ManaAmount;
                    DataManager.Instance.Player.Hp = Math.Min(newMp, maxMp);

                    Console.WriteLine($"마나를 {ManaAmount} 회복하였습니다. 현재 마나: {DataManager.Instance.Player.Mp}");
                }
            }
            Thread.Sleep(600);
        }

        public void AddItem(Item item)
        {
            // 파라미터로 받은 shopItem과 같은 Id의 아이템이 존재하는지 확인
            // if 존재한다면 아이템의 카운트 증가
            InventoryItem? findItem = Items.Find(inventoryItem => inventoryItem.BaseItem.Id == item.Id);
            if (findItem != null)
            {
                findItem.BaseItem.Count++;
            }
            else // else 존재하지 않는다면 해당 아이템을 추가
            {
                Items.Add(new InventoryItem(item.DeepCopy()));
            }

        }

        public void RemoveItem(Item item)
        {
            InventoryItem? findItem = Items.Find(inventoryItem => inventoryItem.BaseItem.Id == item.Id);
            // if 해당 아이템이 여러개인 경우 카운트 감소
            if (findItem?.BaseItem.Count > 1)
            {
                findItem.BaseItem.Count--;
            }
            else // else 해당 아이템이 한개인 경우 리스트에서 제거
            {
                Items.Remove(findItem);
            }
        }
    }
}