using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Player
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public int Level { get; set; }

        public int Atk { get; set; }
        public int CalculateAtk { get => Atk + CalculateItemStat(ItemType.WEAPON); }

        public int Def { get; set; }
        public int CalculateDef { get => Def + CalculateItemStat(ItemType.AMOR); } 
        public int Hp { get; set; }

        public int Gold { get; set; }

        public Inventory Inventory { get; set; }

        public Player (string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 10000;

            Inventory = new Inventory ();
        }

        public string GetStatus()
        {
            int CItemAtk = CalculateItemStat(ItemType.WEAPON);
            int CItemDef = CalculateItemStat(ItemType.AMOR);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {Job} )");
            sb.Append($"공격력 : {Atk}");
            if(CItemAtk != 0)
            {
                sb.Append($"(+{CItemAtk})");
            }
            sb.Append($"\n방어력 : {Def}");
            if (CItemDef != 0)
            {
                sb.Append($"(+{CItemDef})");
            }
            sb.AppendLine($"\n체  력 : {Hp}");
            sb.AppendLine($"Gold : {Gold} G");

            return sb.ToString();
        }

        private int CalculateItemStat(ItemType itemType)
        {
            int stat = 0;
            switch (itemType) 
            {
                case ItemType.WEAPON:
                    stat = Inventory.EquipedItems.Sum(item => item.Atk);
                    break;
                case ItemType.AMOR:
                    stat = Inventory.EquipedItems.Sum(item => item.Def);
                    break;
            }
            return stat;
        }
    }   

}
