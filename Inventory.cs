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

        public void EquipedItem(int index)
        {
            InventoryItem? findItem = Items.Find(item => item.IsEquiped && Items[index].Type == item.Type);
            if(findItem != null)
            {
                findItem.IsEquiped = false;
            }
            Items[index].IsEquiped = !Items[index].IsEquiped;
        }
    }
}