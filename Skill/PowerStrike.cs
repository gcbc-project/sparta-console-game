using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Skill
{
    public class PowerStrike : BaseSkill
    {
        public override string Name => "파워 스트라이크";
        public override string Desc => "적에게 200%로 1회 일격을 가한다.";
        public override int MPCost => 20;

        public override int Use(ICharacter character)
        {
            character.Mp -= MPCost;
            int baseDamage = character.Attack(out bool isCritical);
            return (int)Math.Ceiling(baseDamage * 2.0f);
        }
    }
}