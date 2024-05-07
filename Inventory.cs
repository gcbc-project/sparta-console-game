using System.Text;

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

        public ConsoleBuilder GetItemsInfo()
        {
            ConsoleBuilder cb = new ConsoleBuilder();
            Items.ForEach(item => { cb.Combine(item.GetItemInfo()); });
            return cb;
        }

        public ConsoleBuilder GetItemsInfo(params ItemType[] itemTypes)
        {
            ConsoleBuilder cb = new ConsoleBuilder();

            foreach (var itemType in itemTypes)
            {
                // ItemType에 따라 필터링하여 해당 아이템들만 처리
                var itemsOfType = Items.Where(item => item.BaseItem.Type == itemType);

                foreach (var item in itemsOfType)
                {
                    cb.Combine(item.GetItemInfo());
                }
            }

            return cb;
        }

        public List<InventoryItem> GetItems(params ItemType[] itemTypes)
        {
            List<InventoryItem> items = new List<InventoryItem>();
            foreach (var itemType in itemTypes)
            {
                var itemsOfType = Items.Where(item => item.BaseItem.Type == itemType);

                foreach (var item in itemsOfType)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public void EquipedItem(InventoryItem inventoryItem)
        {
            InventoryItem? findItem = Items.Find(item => item.IsEquiped && inventoryItem.BaseItem.Type == item.BaseItem.Type);
            inventoryItem.IsEquiped = !inventoryItem.IsEquiped;
            if (findItem != null)
            {
                findItem.IsEquiped = false;
            }
        }

        public void UsingItem(Player player, InventoryItem inventoryItem)
        {
            if (inventoryItem.BaseItem.Name == "HP 초급 포션")
            {
                if (player.Hp >= player.Stats.Hp)
                {
                    Console.WriteLine("체력이 이미 최대치에 도달했습니다.");
                }
                else
                {
                    int healAmount = 30;
                    int maxHp = player.Stats.Hp;

                    int newHp = player.Hp + healAmount;
                    player.Hp = Math.Min(newHp, maxHp);

                    Console.WriteLine($"체력을 {healAmount} 회복하였습니다. 현재 체력: {player.Hp}");
                    player.Inventory.RemoveItem(inventoryItem.BaseItem);
                }
            }
            else
            {
                if (player.Mp >= player.Stats.Mp)
                {
                    Console.WriteLine("마나가 이미 최대치에 도달했습니다.");
                }
                else
                {
                    int ManaAmount = 30;
                    int maxMp = player.Stats.Mp;

                    int newMp = player.Hp + ManaAmount;
                    player.Hp = Math.Min(newMp, maxMp);

                    Console.WriteLine($"마나를 {ManaAmount} 회복하였습니다. 현재 마나: {player.Mp}");
                    player.Inventory.RemoveItem(inventoryItem.BaseItem);
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