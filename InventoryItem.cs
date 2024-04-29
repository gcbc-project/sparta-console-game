using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class InventoryItem : Item
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Price { get; set; }
        public bool IsEquiped { get; set; }

        public ItemType Type { get; set; }

        public InventoryItem(string name, string desc, int price, ItemType type, int atk = 0, int def = 0)
        {
            Name = name;
            Desc = desc;
            Price = price;
            Atk = atk;
            Def = def;
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
            if (Atk != 0)
            {
                sb.Append($"공격력 {Atk}\t|");
            }
            if (Def != 0)
            {
                sb.Append($"방어력 {Def}\t|");
            }
            sb.Append($"{Desc}");

            return sb.ToString();
        }

        public static explicit operator InventoryItem(ShopItem v)
        {
            return new InventoryItem(v.Name, v.Desc, v.Price, v.Type, v.Atk, v.Def);
        }
    }
}