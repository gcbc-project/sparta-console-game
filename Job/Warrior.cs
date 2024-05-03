using SpartaConsoleGame.Enemy;

namespace SpartaConsoleGame
{
    internal class Warrior : Player
    {
        public Warrior(string name)
        : base(name, new Stats(120, 50, 15, 10, 1.15f, 1.05f), "전사") { }
    }
}
