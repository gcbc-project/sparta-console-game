using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Skill
{
    public class FireBall : BaseSkill
    {
        public override string Name => "파이어 볼";
        public override string Desc => "적을 150%로 1회 불태우는 화염구를 날린다";
        public override int MPCost => 15;

        public override int Use(ICharacter character)
        {
            character.Stats.Mp -= MPCost;
            int baseDamage = character.Attack(out bool isCritical);
            return (int)Math.Ceiling(baseDamage * 1.5f);
        }
    }
}