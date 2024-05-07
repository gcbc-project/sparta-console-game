using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class CannonMinion : BaseEnemy
    {
        public CannonMinion() : base(new Stats(hp: 25, atk: 8, eva: 1.10f))
        {
            Name = "대포 미니언";
            Level = 5;
            Hp = Stats.Hp;
        }
    }
}