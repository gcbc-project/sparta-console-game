using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class Minion : BaseEnemy
    {
        public Minion() : base(new Stats(hp :15, atk : 5, eva : 1.10f))
        {
            Name = "미니언";
            Level = 2;
            Hp = Stats.Hp;
        }
    }
}