using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Player : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public string Job { get; set; }

        public int CalculateAtk { get => Atk + CalculateItemStat(ItemType.Weapon); }
        public int Def { get; set; }
        public int CalculateDef { get => Def + CalculateItemStat(ItemType.Armor); }

        public int Gold { get; set; }
        public float MaxExpStorage { get; set; }
        public float NowExpStorage { get; set; }
        public Inventory Inventory { get; set; }
        private Random _random = new Random();

        public Player(string job, float maxExpStorage, float nowExpStorage)
        {
            Job = job;
            Level = 1;
            Atk = 10;
            Def = 5;
            Hp = 100;
            MaxHp = 100;
            Gold = 10000;
            MaxExpStorage = maxExpStorage;
            NowExpStorage = nowExpStorage;
            Inventory = new Inventory();
        }

        public void SetName()
        {
            Name = Console.ReadLine();
        }

        public string GetPlayerInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[내정보]");
            sb.AppendLine($"Lv.{Level} {Name} ({Job})");
            sb.AppendLine($"HP {Hp}/{MaxHp}");
            return sb.ToString();
        }
        public string GetStatus()
        {
            int calculateItemAtk = CalculateItemStat(ItemType.Weapon);
            int calculateItemDef = CalculateItemStat(ItemType.Armor);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {Job} )");
            sb.AppendLine($"EXP ({NowExpStorage} / {MaxExpStorage.ToString("N2")})");
            sb.Append($"공격력 : {CalculateAtk}");
            if (calculateItemAtk != 0)
            {
                sb.Append($" ({Atk} + {calculateItemAtk})");
            }
            sb.Append($"\n방어력 : {CalculateDef}");
            if (calculateItemDef != 0)
            {
                sb.Append($" ({Def} + {calculateItemDef})");
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
        public int Attack()
        {
            return _random.Next((int)Math.Round(Atk * 0.9f), (int)Math.Round(Atk * 1.1f));
        }
        public string Hit(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                return "Dead";
            }
            return Hp.ToString();
        }
        public string Rest()
        {
            StringBuilder sb = new StringBuilder();
            if (Gold >= 500 && Hp < MaxHp)
            {
                Gold -= 500;
                //휴식 하기
                Hp = MaxHp;
                sb.AppendLine($"휴식 완료");
            }
            else if (Hp == 100)
            {
                sb.AppendLine($"\n이미 최대체력입니다.");
            }
            else
            {
                //쫒아내기
                sb.AppendLine($"\n돈이 없습니다");
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
                case ItemType.Weapon:
                    stat = Inventory.EquipedItems.Sum(item => item.Atk);
                    break;
                case ItemType.Armor:
                    stat = Inventory.EquipedItems.Sum(item => item.Def);
                    break;
            }
            return stat;
        }
    }

}