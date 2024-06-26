﻿using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaConsoleGame
{
    internal class Dungeon
    {
        public string Title { get; set; }
        public int RecommendDefense { get; set; }
        public int BasicReward { get; set; }
        public float ExpReward { get; set; }
        public List<IEnemy> EnemyList { get; set; }
        private List<IEnemy> SelectEnemyList { get; set; }
        public List<DropItem> DropItems { get; set; }

        public Item RelicItem { get; private set; }

        private Random _random = new Random();
        public Dungeon(string title, int recommendDefense, int basicReward, float expReward, List<IEnemy> enemyList, List<DropItem> dropItems)
        {
            Title = title;
            RecommendDefense = recommendDefense;
            BasicReward = basicReward;
            ExpReward = expReward;
            EnemyList = enemyList;
            DropItems = dropItems;

        }

        public string GetDungeonInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Title} 던전 \t| 방어력 : {RecommendDefense} 이상 권장");

            return sb.ToString();
        }

        private ConsoleBuilder GetEnemiesInfo()
        {
            ConsoleBuilder cb = new ConsoleBuilder();
            SelectEnemyList.ForEach(enemy => { cb.Combine(enemy.GetEnemyInfo()); });
            return cb;
        }

        private void Result(Menu menu, Player player, int prevHp, bool isClear)
        {
            menu.SetTitle(new ConsoleBuilder("[Battle!!] - Result", () => true, ConsoleColor.DarkYellow));
            if (isClear)
            {
                int prevLevel = player.Level - 1;
                menu.SetInfo(() => $"Victory\n", true);
                menu.SetInfo(() => $"던전에서 몬스터 {SelectEnemyList.Count}마리를 잡았습니다.\n");
                menu.SetInfo(() => $"[보상 목록]\n");
                SetRewardItems( menu, player);
                player.ExpUp(ExpReward, menu, player, prevHp);

            }
            else
            {
                menu.SetInfo(() => $"You Lose\n", true);
            }
            menu.SetInfo(player.GetPlayerInfo);
            menu.SetInfo(() => $"HP {prevHp} -> {player.Hp}");
            menu.SetExit(exitLabel: "던전 나가기");
        }
        public void Battle(Player player)
        {
            SetEnemies();
            int playerHp = player.Hp;
            Menu menu = new Menu();
            menu.SetRefreshMenu(() =>
            {
                if (player.IsDead)
                {
                    Result(menu, player, playerHp, false);
                }
                else if (SelectEnemyList.All(enemy => enemy.IsDead))
                {
                    Result(menu, player, playerHp, true);
                }
                else
                {
                    menu.SetTitle(new ConsoleBuilder("[Battle!!]", () => true, ConsoleColor.DarkYellow));
                    menu.SetInfo(player.GetPlayerInfo, true);
                    menu.SetInfo(GetEnemiesInfo);

                    menu.AddMenuItem("공격", () => { BattlePhase(player); });
                    menu.AddMenuItem("스킬", () => { BattleChoiceSkill(player); });
                    menu.SetExit(isExitHidden: true);
                }
            });

            menu.SetExit(true);
            menu.Run();
        }

        public void BattleChoiceSkill(Player player)
        {
            bool isSkip = false;
            Menu menu = new Menu();
            menu.SetTitle(new ConsoleBuilder("[Battle!!]", () => true, ConsoleColor.DarkYellow));
            menu.SetInfo(player.GetPlayerInfo, true);
            menu.SetInfo(GetEnemiesInfo);

            for (int i = 0; i < player.Skills.Count; i++)
            {
                ISkill skill = player.Skills[i];
                menu.AddMenuItem(skill.GetSkillInfo(),
                                () => { isSkip = true; BattlePhase(player, skill); },
                                () => skill.IsUse(player));
            }
            menu.SetExit(exitLabel: "공격 방법 다시 선택");
            menu.SetIsSkip(() => isSkip);
            menu.Run();
        }

        public void BattlePhase(Player player, ISkill skill = null)
        {
            bool isSkip = false;
            Menu menu = new Menu();
            menu.SetTitle(new ConsoleBuilder("[Battle!! - Phase]", () => true, ConsoleColor.DarkYellow));
            menu.SetInfo(player.GetPlayerInfo);
            menu.SetRefreshMenu(() =>
            {
                for (int i = 0; i < SelectEnemyList.Count; i++)
                {
                    IEnemy enemy = SelectEnemyList[i];
                    menu.AddMenuItem(enemy.GetEnemyInfo(), () =>
                    {
                        isSkip = true;
                        AttackTurn(player, enemy, skill);
                        SelectEnemyList.Where(e => !e.IsDead).ToList().ForEach((e) =>
                        {
                            if (!player.IsDead)
                            {
                                AttackTurn(e, player);
                            }
                        });
                    }, () =>
                    {
                        if (enemy.IsDead)
                        {
                            Console.WriteLine("이미 처치된 적입니다.");
                            Thread.Sleep(500);
                        }
                        return !enemy.IsDead;
                    });
                }
            });
            menu.SetExit(exitLabel: "취소");
            menu.SetIsSkip(() => isSkip);

            menu.Run();
        }
        public void AttackTurn(ICharacter offense, ICharacter defense, ISkill skill = null)
        {
            int atk;
            bool isCritical = false;
            bool isDodged = false;
            string hp;

            if (skill != null)
            {
                atk = skill.Use(offense);
                isCritical = _random.NextDouble() < (offense.Stats.Crit - 1);
                hp = defense.Hit(atk, out isDodged, allowDodge: false);  // 스킬로 공격 시 회피는 고려하지 않음
            }
            else
            {
                atk = offense.Attack(out isCritical);
                hp = defense.Hit(atk, out isDodged);
            }

            int prevHp = defense.Hp;

            Menu menu = new Menu();
            menu.SetTitle(new ConsoleBuilder($"[Battle!!] - Turn({offense.Name})", () => true, ConsoleColor.DarkYellow));
            menu.SetInfo(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{offense.Name}의 {(skill != null ? $"{skill.Name}" : "공격")}!");

                if (isDodged && skill == null)
                {
                    sb.AppendLine($"Lv.{defense.Level} {defense.Name}을(를) 공격했지만 아무 일도 일어나지 않았습니다.");
                }
                else
                {
                    sb.Append($"Lv.{defense.Level} {defense.Name}을(를) 맞췄습니다. [데미지 : {atk}]");
                    if (isCritical)
                    {
                        sb.Append(" - 치명타 공격!!");
                    }
                    sb.AppendLine();
                    sb.AppendLine($"Lv.{defense.Level} {defense.Name}");
                    sb.AppendLine($"HP {prevHp} => {hp}");
                }
                return sb.ToString();
            });
            menu.SetExit(exitLabel: "다음");

            menu.Run();
        }

        private void SetEnemies()
        {
            SelectEnemyList = new List<IEnemy>();
            int enemyNum = _random.Next(4);

            for (int i = 0; i <= enemyNum; i++)
            {
                int index = _random.Next(EnemyList.Count);
                SelectEnemyList.Add(EnemyList[index].DeepCopy());
            }
        }



        public void SetRewardItems(Menu menu, Player player)
        {
            List<DropItem> dropItems = DropItems;

            foreach (var item in dropItems)
            {
                int itemRandom = _random.Next(0, 100);
                if (itemRandom <= item.DropRate * 100)
                {
                    player.Inventory.AddItem(item.BaseItem);
                    menu.SetInfo(item.GetItemInfo);
                }
            }
        }


    }
}