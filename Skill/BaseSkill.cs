using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Skill
{
    public abstract class BaseSkill : ISkill
    {
        public abstract string Name { get; }
        public abstract string Desc { get; }
        public abstract int MPCost { get; }

        public abstract void Use(ICharacter character);
        public virtual string GetSkillInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} - MP {MPCost})");
            sb.AppendLine($"{Desc})");

            return sb.ToString();
        }

    }
}