using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame.Skill
{
    public class PowerStrike : ISkill
    {
        public string Name => "파워 스트라이크";
        public string Desc => "적에게 200%로 1회 일격을 가한다.";
        public int MPCost => 3;

        public void Use(ICharacter character)
        {
            if (character.Stats.Mp >= MPCost)
            {
                character.Stats.Mp -= MPCost;
                int damage = (int)Math.Cosh(character.Stats.Atk * 2.0f);
            }
            else
            {
                Console.WriteLine("Not enough MP to use this skill.");
            }
        }
    }
}