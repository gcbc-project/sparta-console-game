using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Enemy
{
    public class Stats
    {
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public float Crit { get; set; }
        public float Eva { get; set; }

        public Stats() { }
        public Stats(int hp = 0, int mp = 0, int atk = 0, int def = 0, float crit = 0, float eva = 0)
        {
            Hp = hp;
            Mp = mp;
            Atk = atk;
            Def = def;
            Crit = crit;
            Eva = eva;
        }
        public virtual Stats DeepCopy()
        {
            Stats newStats = (Stats)Activator.CreateInstance(GetType());

            // 현재 객체의 속성을 복사하여 새로운 객체에 할당
            newStats.Hp = this.Hp;
            newStats.Mp = this.Mp;
            newStats.Atk = this.Atk;
            newStats.Def = this.Def;
            newStats.Crit = this.Crit;
            newStats.Eva = this.Eva;

            return newStats;
        }

    }
}
