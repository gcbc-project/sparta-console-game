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
        public override int MPCost => 3;

        public override int Use(ICharacter character)
        {
            character.Stats.Mp -= MPCost;
            return (int)Math.Ceiling(character.Attack() * 2.0f);
        }
    }
}