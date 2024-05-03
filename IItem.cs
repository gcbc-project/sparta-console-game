using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public enum ItemType
    {
        Weapon,
        Armor
    }

    internal interface IItem
    {
        public Item BaseItem { get; set; }
        public string GetItemInfo();
    }
}