using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;

namespace SpartaConsoleGame
{
    internal class Warrior : Player
    {
        public Warrior(string name)
        : base(name, new Stats(120, 50, 15, 10, 1.75f, 1.50f))
        {
            Skills = new List<ISkill> { new PowerStrike() };
            JobLabel = "전사";
        }
    }
}
