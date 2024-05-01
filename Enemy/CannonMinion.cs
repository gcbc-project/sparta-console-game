using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class CannonMinion : BaseEnemy
    {
        public CannonMinion()
        {
            Name = "대포 미니언";
            Level = 5;
            MaxHp = 25;
            Hp = 25;
            Atk = 8;
        }
    }
}