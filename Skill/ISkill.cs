using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Skill
{
    public interface ISkill
    {
        string Name { get; }
        string Desc { get; }
        int MPCost { get; }
        int Use(ICharacter character);
        bool IsUse(ICharacter character);
        string GetSkillInfo();
    }
}