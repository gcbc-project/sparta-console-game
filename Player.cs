using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Player
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public int Level { get; set; }

        public int Attack { get; set; }
        public int CalculateAttack { get => Attack + CalculateItemStat(ItemType.Weapon); }

        public int Defense { get; set; }
        public int CalculateDefense { get => Defense + CalculateItemStat(ItemType.Armor); }
        public int Hp { get; set; }

        public int Gold { get; set; }

        //맥스 경험치 통
        public float MaxExpStorage { get; set; }
        //현재 경험치 통
        public float NowExpStorage { get; set; }


        public Inventory Inventory { get; set; }

        public Player(string name, string job, float maxExpStorage, float nowExpStorage)
        {
            Name = name;
            Job = job;
            Level = 1;
            Attack = 10;
            Defense = 5;
            Hp = 100;
            Gold = 10000;
            MaxExpStorage = maxExpStorage;
            NowExpStorage = nowExpStorage;
            Inventory = new Inventory();
        }

        public string GetStatus()
        {
            int calculateItemAtk = CalculateItemStat(ItemType.Weapon);
            int calculateItemDef = CalculateItemStat(ItemType.Armor);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {Job} )");
            sb.AppendLine($"EXP ({NowExpStorage} / {MaxExpStorage.ToString("N2")})");
            sb.Append($"공격력 : {Attack}");
            if (calculateItemAtk != 0)
            {
                sb.Append($"(+{calculateItemAtk})");
            }
            sb.Append($"\n방어력 : {Defense}");
            if (calculateItemDef != 0)
            {
                sb.Append($"(+{calculateItemDef})");
            }
            sb.AppendLine($"\n체  력 : {Hp}");
            sb.AppendLine($"Gold : {Gold} G");

            return sb.ToString();
        }

        public string GetGold()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[보유 골드] : {Gold} G\n");

            return sb.ToString();
        }
        public string Rest()
        {
            StringBuilder sb = new StringBuilder();
            if (Gold >= 500 && Hp < 100)
            {
                Gold -= 500;
                //휴식 하기
                Hp = 100;
                sb.AppendLine($"휴식 완료");
            }
            else
            {
                //쫒아내기
                sb.AppendLine($"돈이 없습니다");
            }
            return sb.ToString();
        }
        public void ExpUp(float expReward)
        {
            NowExpStorage += expReward;
            if (MaxExpStorage <= NowExpStorage)
            {
                LevelUp();
            }
        }
        private void LevelUp()
        {
            float remainExp = NowExpStorage - MaxExpStorage;
            Level++;
            Defense += 1;
            Attack += 2;
            MaxExpStorage = (float)Math.Round((double)(MaxExpStorage * 1.05f));
            NowExpStorage = remainExp;
        }

        private int CalculateItemStat(ItemType itemType)
        {
            int stat = 0;
            switch (itemType)
            {
                case ItemType.Weapon:
                    stat = Inventory.EquipedItems.Sum(item => item.Attack);
                    break;
                case ItemType.Armor:
                    stat = Inventory.EquipedItems.Sum(item => item.Defense);
                    break;
            }
            return stat;
        }
    }

}