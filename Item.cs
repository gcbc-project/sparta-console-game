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
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Price { get; set; }

        public ItemType Type { get; set; }

        public string GetItemInfo();
    }
}