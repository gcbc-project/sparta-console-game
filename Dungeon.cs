using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Dungeon
    {
        public int RecommendDef { get; set; }

        public int BasicReward { get; set; }

        public string Title { get; set; }


        // 경험치 리워드
        public float ExpReward { get; set; }
        

        public Dungeon(string title, int recommendDef, int basicReward, float expReward)
        {
            Title = title;
            RecommendDef = recommendDef;
            BasicReward = basicReward;
            ExpReward = expReward;
        }

        public string GetDungeonInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Title} 던전 \t| 방어력 : {RecommendDef} 이상 권장");

            return sb.ToString();
        }
    }  
}