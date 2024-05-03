using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public  Stats Stats { get; set; }
        public  int Hp { get; set; }
        public  int Attack();
        public  string Hit(int damage);
    }
}