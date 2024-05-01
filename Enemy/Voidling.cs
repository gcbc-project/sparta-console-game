using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class Voidling : BaseEnemy
    {
        public Voidling()
        {
            Name = "공허충";
            Level = 3;
            MaxHp = 10;
            Hp = 10;
            Atk = 9;
        }
    }
}