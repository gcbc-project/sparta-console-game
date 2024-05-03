using SpartaConsoleGame.Enemy;
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
        public Stats Stats { get; set; }
        public int Price { get; set; }

        public bool IsPurchased { get; set; }
        public ItemType Type { get; set; }

        public ShopItem(string name, string desc, int price, ItemType type, Stats stats)
        {
            Name = name;
            Desc = desc;
            Price = price;
            Type = type;
            Stats = stats;
        }

        public string GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name}\t|");
            if (Stats.Atk != 0)
            {
                sb.Append($"공격력 +{Stats.Atk}\t|");
            }
            if (Stats.Def != 0)
            {
                sb.Append($"방어력 +{Stats.Def}\t|");
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