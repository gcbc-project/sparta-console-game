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
        public override int MPCost => 5;

        public override int Use(ICharacter character)
        {
            character.Stats.Mp -= MPCost;
            return (int)Math.Ceiling(character.Attack() * 1.5f);
        }
    }
}