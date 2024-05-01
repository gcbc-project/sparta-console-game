using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class ShopItem : IItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Price { get; set; }

        public bool IsPurchased { get; set; }
        public ItemType Type { get; set; }

        public ShopItem(string name, string desc, int price, ItemType type, int attack = 0, int defense = 0)
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
            sb.Append($"{Name}\t|");
            if (Attack != 0)
            {
                sb.Append($"공격력 +{Attack}\t|");
            }
            if (Defense != 0)
            {
                sb.Append($"방어력 +{Defense}\t|");
            }
            sb.Append($" {Desc}\t|");
            if (IsPurchased)
            {
                sb.Append(" 구매완료");
            }
            else
            {
                sb.Append($" {Price} G");
            }

            return sb.ToString();
        }
    }
}