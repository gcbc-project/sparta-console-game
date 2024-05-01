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


        public Dungeon(string title, int recommendDefense, int basicReward, float expReward)
        {
            Title = title;
            RecommendDefense = recommendDefense;
            BasicReward = basicReward;
            ExpReward = expReward;
        }

        public string GetDungeonInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Title} 던전 \t| 방어력 : {RecommendDefense} 이상 권장");

            return sb.ToString();
        }
    }
}