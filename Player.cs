﻿using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;
using System.Text;

namespace SpartaConsoleGame
{
    internal class Player : ICharacter, IJob
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public Stats Stats { get; set; }
        public bool IsDead { get => Hp == 0; }
        public List<ISkill> Skills { get; set; }
        public int CalculateAtk { get => Stats.Atk + CalculateItemStat(ItemType.Weapon); }
        public int CalculateDef { get => Stats.Def + CalculateItemStat(ItemType.Armor); }

        public int Gold { get; set; }
        public float MaxExpStorage { get; set; }
        public float NowExpStorage { get; set; }
        public Inventory Inventory { get; set; }
        public string JobLabel { get; set; }

        private Random _random = new Random();

        public Player(string name, Stats stats)
        {
            Name = name;
            Level = 1;
            Stats = stats;
            Hp = stats.Hp;
            Mp = stats.Mp;
            Gold = 10000;
            MaxExpStorage = 100;
            NowExpStorage = 0;
            Inventory = new Inventory();
        }
        public string GetPlayerInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[내정보]");
            sb.AppendLine($"Lv.{Level} {Name} ({JobLabel})");
            sb.AppendLine($"HP {Hp}/{Stats.Hp}");
            sb.AppendLine($"MP : {Mp} / {Stats.Mp}");
            return sb.ToString();
        }
        public string GetStatus()
        {
            int calculateItemAtk = CalculateItemStat(ItemType.Weapon);
            int calculateItemDef = CalculateItemStat(ItemType.Armor);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {JobLabel} )");
            sb.AppendLine($"EXP ({NowExpStorage} / {MaxExpStorage.ToString("N2")})");
            sb.AppendLine($"체  력 : {Hp} / {Stats.Hp}");
            sb.AppendLine($"마  나 : {Mp} / {Stats.Mp}");
            sb.Append($"공격력 : {CalculateAtk}");
            if (calculateItemAtk != 0)
            {
                sb.Append($" ({Stats.Atk} + {calculateItemAtk})");
            }
            sb.Append($"\n방어력 : {CalculateDef}");
            if (calculateItemDef != 0)
            {
                sb.Append($" ({Stats.Def} + {calculateItemDef})");
            }
            sb.AppendLine($"\n치명타 : {((Stats.Crit - 1) * 100).ToString("N2")} %");
            sb.AppendLine($"회피율 : {((Stats.Eva - 1) * 100).ToString("N2")} %");
            sb.AppendLine($"\nGold   : {Gold} G");

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
            return _random.Next((int)Math.Round(CalculateAtk * 0.9f), (int)Math.Round(CalculateAtk * 1.1f));
        }
        public string Hit(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Die();
                return "Dead";
            }
            return Hp.ToString();
        }

        public void Die()
        {
            Hp = 0;
        }
        public string Rest()
        {
            StringBuilder sb = new StringBuilder();
            if (Gold >= 500 && Hp < Stats.Hp)
            {
                Gold -= 500;
                //휴식 하기
                Hp = Stats.Hp;
                Mp = Stats.Mp;
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
        public void ExpUp(float expReward, Menu menu, Player player, float prevExp)
        {
            prevExp = NowExpStorage;
            NowExpStorage += expReward;
            if (MaxExpStorage <= NowExpStorage)
            {
                LevelUp(expReward, menu, player, prevExp);
            }
            else
            {
                menu.SetInfo(() => $" Lv. {player.Level} EXP {prevExp} ->   Lv. {player.Level} EXP {NowExpStorage}\n");
            }
        }
        private void LevelUp(float expReward, Menu menu, Player player, float prevExp)
        {
            float remainExp = NowExpStorage - MaxExpStorage;
            int prevLevel = player.Level;
            Level++;    
            Stats.Def += 1;
            Stats.Atk += 2;
            MaxExpStorage = (float)Math.Round((double)(MaxExpStorage * 1.05f));
            NowExpStorage = remainExp;
            menu.SetInfo(() => $"레벨업!  Lv. {prevLevel} EXP {prevExp} -> Lv. {player.Level} EXP {remainExp}\n");
        }

        private int CalculateItemStat(ItemType itemType)
        {
            int stat = 0;
            switch (itemType)
            {
                case ItemType.Weapon:
                    stat = Inventory.EquipedItems.Sum(item => item.BaseItem.Stats.Atk);
                    break;
                case ItemType.Armor:
                    stat = Inventory.EquipedItems.Sum(item => item.BaseItem.Stats.Def);
                    break;
            }
            return stat;
        }
    }

}