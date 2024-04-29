using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Player
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public int Level { get; set; }

        public int Atk { get; set; }

        public int Def { get; set; }

        public int Hp { get; set; }

        public int Gold { get; set; }

        public Player (string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 1500;
        }

        public string GetStatus()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"{Name} ( {Job} )");
            sb.AppendLine($"공격력 : {Atk}");
            sb.AppendLine($"방어력 : {Def}");
            sb.AppendLine($"체  력 : {Hp}");
            sb.AppendLine($"Gold : {Gold} G");

            return sb.ToString();
        }
    }   

}
