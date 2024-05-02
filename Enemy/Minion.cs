using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class Minion : BaseEnemy
    {
        public Minion()
        {
            Name = "미니언";
            Level = 2;
            MaxHp = 15;
            Hp = 15;
            Atk = 5;
        }
    }
}