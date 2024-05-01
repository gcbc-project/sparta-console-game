using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class DungeonManager
    {
        public List<Dungeon> DungeonList { get; set; }

        public DungeonManager()
        {
            DungeonList = new List<Dungeon>();
        }


        public string Enter(Player player, int index)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            // Hp와 관련된 정의

            if (player.CalculateDefense < DungeonList[index].RecommendDefense && random.Next(0, 10) < 4)
            {
                sb.AppendLine("아쉽습니다..");
                sb.AppendLine($"{DungeonList[index].Title}던전 을 클리어하지 못했습니다.");
                sb.AppendLine("\n[탐험 결과]");
                sb.AppendLine($"체력 {player.Hp} -> {player.Hp / 2}");
                player.Hp /= 2;
            }
            else
            {
                int defValve = DungeonList[index].RecommendDefense - player.CalculateDefense;
                int baseHpLoss = random.Next(20 + defValve, 36 + defValve);
                int extraRewardPer = random.Next(player.CalculateAttack, player.CalculateAttack * 2);
                int extraReward = (int)(DungeonList[index].BasicReward * (extraRewardPer / 100.0f));
                int totalReward = DungeonList[index].BasicReward + extraReward;
                sb.AppendLine("축하드립니다!");
                sb.AppendLine($"{DungeonList[index].Title}던전 을 클리어하였습니다.");
                sb.AppendLine("\n[탐험 결과]");
                sb.AppendLine($"체력 {player.Hp} -> {player.Hp - baseHpLoss}");
                sb.AppendLine($"Gold {player.Gold} -> {player.Gold + totalReward}");
                player.Hp -= baseHpLoss;
                player.Gold += totalReward;
                player.ExpUp(DungeonList[index].ExpReward);
            }
            return sb.ToString();
        }


    }
}
