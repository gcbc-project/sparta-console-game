using SpartaConsoleGame.Enemy;

namespace SpartaConsoleGame
{
    public class Warrior : IJob
    {
        public string Name => "전사";
        public Warrior()
        {
        }

        public Stats Stats => new Stats(120, 50, 15, 10, 1.15f, 1.05f);
    }
}
