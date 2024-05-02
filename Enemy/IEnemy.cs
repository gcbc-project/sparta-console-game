using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public interface IEnemy : ICharacter
    {
        bool IsDead { get; set; }

        string GetEnemyInfo();

        void Die();
        BaseEnemy DeepCopy();
    }

}