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
        public string Name { get; set; }
        public string Desc { get; set; }
        public Stats Stats { get; set; }
        public int Price { get; set; }
        public bool IsEquiped { get; set; }

        public ItemType Type { get; set; }

        public InventoryItem(string name, string desc, int price, ItemType type, Stats stats )
        {
            Name = name;
            Desc = desc;
            Price = price;
            Stats = stats;
            Type = type;
        }

        public string GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (IsEquiped)
            {
                sb.Append("[E]");
            }
            sb.Append($"{Name}\t|");
            if (Stats.Atk != 0)
            {
                sb.Append($"공격력 {Stats.Atk}\t|");
            }
            if (Stats.Def != 0)
            {
                sb.Append($"방어력 {Stats.Def}\t|");
            }
            sb.Append($"{Desc}");

            return sb.ToString();
        }

        public static explicit operator InventoryItem(ShopItem v)
        {
            return new InventoryItem(v.Name, v.Desc, v.Price, v.Type, v.Stats);
        }
    }
}