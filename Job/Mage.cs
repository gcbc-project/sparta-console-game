using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;

namespace SpartaConsoleGame
{
    internal class Mage : Player
    {
        public Mage(string name)
        : base(name, new Stats(80, 150, 10, 5, 1.10f, 1.15f))
        {
            Skills = new List<ISkill> { new FireBall() };
            JobLabel = "마법사";
        }
    }
}
