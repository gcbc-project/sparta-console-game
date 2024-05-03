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
        public Item BaseItem { get; set; }
        public bool IsPurchased { get; set; }
        public ShopItem(Item item)
        {
            BaseItem = item;
        }

        public string GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{BaseItem.Name}\t|");
            if (BaseItem.Stats.Atk != 0)
            {
                sb.Append($"공격력 +{BaseItem.Stats.Atk}\t|");
            }
            if (BaseItem.Stats.Def != 0)
            {
                sb.Append($"방어력 +{BaseItem.Stats.Def}\t|");
            }
            sb.Append($" {BaseItem.Desc}\t|");
            if (IsPurchased)
            {
                sb.Append(" 구매완료");
            }
            else
            {
                sb.Append($" {BaseItem.Price} G");
            }

            return sb.ToString();
        }
    }
}