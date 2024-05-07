using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;
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
        public Stats Stats { get; set; }
        public List<ISkill> Skills { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Attack(out bool isCritical);
        string Hit(int damage, out bool isDodged, bool allowDodge = true);
    }
}