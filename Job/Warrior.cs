using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;

namespace SpartaConsoleGame
{
    internal class Warrior : Player
    {
        public Warrior(string name)
        : base(name, new Stats(120, 80, 15, 10, 1.15f, 1.10f))
        {
            Skills = new List<ISkill> { new PowerStrike() };
            JobLabel = "전사";
        }
    }
}
