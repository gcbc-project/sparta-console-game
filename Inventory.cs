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
<<<<<<< Updated upstream
        public List<InventoryItem> EquipedItems { get { return Items.Where(item=>item.IsEquiped).ToList(); } }
=======
        public List<InventoryItem> EquipedItems { get { return Items.Where(item => item.IsEquiped).ToList(); } }
>>>>>>> Stashed changes


        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        public void EquipedItem(int index)
        {
            Items[index].IsEquiped = !Items[index].IsEquiped;
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
