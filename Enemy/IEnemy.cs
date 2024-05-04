using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public interface IEnemy : ICharacter
    {
        bool IsDead { get; set; }

        ConsoleBuilder GetEnemyInfo();

        void Die();
        BaseEnemy DeepCopy();
    }

}