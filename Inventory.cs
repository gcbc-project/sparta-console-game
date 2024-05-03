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

        public string GetItemsInfo()
        {
            StringBuilder sb = new StringBuilder();
            Items.ForEach(item => { sb.AppendLine(item.GetItemInfo()); });
            return sb.ToString();
        }

        public void EquipedItem(int itemIndex)
        {
            InventoryItem? findItem = Items.Find(item => item.IsEquiped && Items[itemIndex].BaseItem.Type == item.BaseItem.Type);
            if (findItem != null)
            {
                findItem.IsEquiped = false;
            }
            Items[itemIndex].IsEquiped = !Items[itemIndex].IsEquiped;
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