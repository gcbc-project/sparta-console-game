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

        public int Atk { get; set; }
        public int CalculateAtk { get => Atk + CalculateItemStat(ItemType.WEAPON); }

        public int Def { get; set; }
        public int CalculateDef { get => Def + CalculateItemStat(ItemType.AMOR); }
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
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 10000;
            MaxExpStorage = maxExpStorage;
            NowExpStorage = nowExpStorage;
            Inventory = new Inventory();
        }

        public string GetStatus()
        {
            int CItemAtk = CalculateItemStat(ItemType.WEAPON);
            int CItemDef = CalculateItemStat(ItemType.AMOR);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {Job} )");
            sb.AppendLine($"EXP ({NowExpStorage} / {MaxExpStorage.ToString("N2")})");
            sb.Append($"공격력 : {Atk}");
            if (CItemAtk != 0)
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

        public string GetGold()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[보유 골드] : {Gold} G\n");

            return sb.ToString();
        }
        public string Rest()
        {
            StringBuilder sb = new StringBuilder();
            if (Gold > 500 && Hp < 100)
            {
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
            if (MaxExpStorage <=NowExpStorage)
            {
                LevelUp();
            }
        }
        private void LevelUp()
        {
            float remainExp = NowExpStorage - MaxExpStorage;
            Level++;
            Def += 1;
            Atk += 2;
            MaxExpStorage = (float)Math.Round((double)(MaxExpStorage * 1.05f));
            NowExpStorage = remainExp;
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