using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Random _random = new Random();
        public Dungeon(string title, int recommendDefense, int basicReward, float expReward, List<IEnemy> enemyList)
        {
            Title = title;
            RecommendDefense = recommendDefense;
            BasicReward = basicReward;
            ExpReward = expReward;
            EnemyList = enemyList;
        }

        public string GetDungeonInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Title} 던전 \t| 방어력 : {RecommendDefense} 이상 권장");

            return sb.ToString();
        }

        private string GetEnemiesInfo()
        {
            StringBuilder sb = new StringBuilder();
            SelectEnemyList.ForEach(enemy => { sb.AppendLine(enemy.GetEnemyInfo()); });
            return sb.ToString();
        }

        private void Result(Menu menu, Player player, int prevHp, bool isClear)
        {
            menu.SetTitle("[Battle!!] - Result");
            if (isClear)
            {
                menu.SetInfo(() => $"Victory\n", true);
                menu.SetInfo(() => $"던전에서 몬스터 {SelectEnemyList.Count}마리를 잡았습니다.\n");
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
                    menu.SetTitle("[Battle!!]");
                    menu.SetInfo(player.GetPlayerInfo, true);
                    menu.SetInfo(GetEnemiesInfo);

                    menu.AddMenuItem("공격", () => { BattlePhase(player); });
                    menu.SetExit(isExitHidden: true);
                }
            });

            menu.SetExit(true);
            menu.Run();
        }

        public void BattlePhase(Player player)
        {
            Menu menu = new Menu();
            menu.SetTitle("[Battle!! - Phase]");
            menu.SetInfo(player.GetPlayerInfo);
            menu.SetRefreshMenu(() =>
            {
                bool allDead = SelectEnemyList.All(enemy => enemy.IsDead);
                if (player.IsDead)
                {
                    menu.SetInfo(() => "플레이어가 쓰러졌습니다.");
                    menu.SetExit(exitLabel: "결과 페이지 이동");
                }
                else if (allDead)
                {
                    menu.SetInfo(() => "모든 적을 물리쳤습니다.");
                    menu.SetExit(exitLabel: "결과 페이지 이동");
                }
                else
                {
                    for (int i = 0; i < SelectEnemyList.Count; i++)
                    {
                        IEnemy enemy = SelectEnemyList[i];
                        menu.AddMenuItem(enemy.GetEnemyInfo(), () =>
                        {
                            AttackTurn(player, enemy);
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
                }
            });
            menu.SetExit(exitLabel: "취소");

            menu.Run();
        }
        public void AttackTurn(ICharacter offense, ICharacter defense)
        {
            int atk = offense.Attack();
            int prevHp = defense.Hp;
            string hp = defense.Hit(atk);
            Menu menu = new Menu();
            menu.SetTitle("[Battle!!] - Turn");
            menu.SetInfo(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{offense.Name} 의 공격!");
                sb.AppendLine($"Lv.{defense.Level} {defense.Name} 을(를) 맞췄습니다. [데미지 : {atk}]\n");
                sb.AppendLine($"Lv.{defense.Level} {defense.Name}");
                sb.AppendLine($"HP {prevHp} => {hp}");
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
    }
}