using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class Voidling : BaseEnemy
    {
        public Voidling() : base(new Stats(hp: 10, atk: 9, eva: 1.10f))
        {
            Name = "공허충";
            Level = 3;
            Hp = Stats.Hp;
        }
    }
}