using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            InventoryItem? findItem = Items.Find(item => item.IsEquiped && Items[itemIndex].Type == item.Type);
            if (findItem != null)
            {
                findItem.IsEquiped = false;
            }
            Items[itemIndex].IsEquiped = !Items[itemIndex].IsEquiped;
        }
    }
}