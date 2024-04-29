using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Inventory
    {
        List<InventoryItem> Items { get; set; }

        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        public void EquipedItem(int index)
        {
            Items[index].IsEquiped = !Items[index].IsEquiped;
        }
    }
}
