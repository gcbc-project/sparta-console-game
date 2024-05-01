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
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Price { get; set; }
        public bool IsEquiped { get; set; }

        public ItemType Type { get; set; }

        public InventoryItem(string name, string desc, int price, ItemType type, int attack = 0, int defense = 0)
        {
            Name = name;
            Desc = desc;
            Price = price;
            Attack = attack;
            Defense = defense;
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
            if (Attack != 0)
            {
                sb.Append($"공격력 {Attack}\t|");
            }
            if (Defense != 0)
            {
                sb.Append($"방어력 {Defense}\t|");
            }
            sb.Append($"{Desc}");

            return sb.ToString();
        }

        public static explicit operator InventoryItem(ShopItem v)
        {
            return new InventoryItem(v.Name, v.Desc, v.Price, v.Type, v.Attack, v.Defense);
        }
    }
}