using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class InventoryItem : IItem
    {
        public Item BaseItem { get; set; }
        public bool IsEquiped { get; set; }

        public InventoryItem(Item item)
        {
            BaseItem = item;
        }

        public ConsoleBuilder GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (IsEquiped)
            {
                sb.Append("[E]");
            }
            sb.Append($"{BaseItem.Name}\t|");
            if (BaseItem.Stats.Atk != 0)
            {
                sb.Append($" 공격력 {BaseItem.Stats.Atk}\t|");
            }
            if (BaseItem.Stats.Def != 0)
            {
                sb.Append($" 방어력 {BaseItem.Stats.Def}\t|");
            }
            sb.Append($" {BaseItem.Desc}\t|");
            if (BaseItem.Count > 0)
            {
                sb.Append($" {BaseItem.Count} 개");
            }
            return new ConsoleBuilder(sb.ToString(), () => IsEquiped, ConsoleColor.Green);
        }

        public static explicit operator InventoryItem(ShopItem v)
        {
            return new InventoryItem(v.BaseItem.DeepCopy());
        }
    }
}