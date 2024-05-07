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

        public abstract int Use(ICharacter character);
        public virtual bool IsUse(ICharacter character)
        {
            if (character.Stats.Mp < MPCost)
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                Thread.Sleep(500);
            }
            return character.Stats.Mp >= MPCost;
        }
        public virtual string GetSkillInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} - MP {MPCost}");
            sb.AppendLine($"{Desc}");

            return sb.ToString();
        }

    }
}