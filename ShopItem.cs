﻿using SpartaConsoleGame.Enemy;
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

        public ConsoleBuilder GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{BaseItem.Name}\t|");
            if (BaseItem.Stats.Atk != 0)
            {
                sb.Append($" 공격력 +{BaseItem.Stats.Atk}\t|");
            }
            if (BaseItem.Stats.Def != 0)
            {
                sb.Append($" 방어력 +{BaseItem.Stats.Def}\t|");
            }
            sb.Append($" {BaseItem.Desc}\t|");
            sb.Append($" {BaseItem.Price} G");
            return new ConsoleBuilder(sb.ToString());
        }
    }
}