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

        public override void Use(ICharacter character)
        {
            if (character.Stats.Mp >= MPCost)
            {
                character.Stats.Mp -= MPCost;
                int damage = (int)Math.Cosh(character.Stats.Atk * 1.5f);
            }
            else
            {
                Console.WriteLine("Not enough MP to use this skill.");
            }
        }
    }
}