using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class DropItem : IItem
    {
        public Item BaseItem { get; set; }
        public float DropRate { get; set; }

        public DropItem(Item item, float dropRate)
        {
            BaseItem = item;
            DropRate = dropRate;
        }

        public ConsoleBuilder GetItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            // TODO : 리워드시 표시될 Info 작성
            sb.Append($"{BaseItem.Name}");
            return new ConsoleBuilder(sb.ToString());
        }

    }
}