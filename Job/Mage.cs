using SpartaConsoleGame.Enemy;

namespace SpartaConsoleGame
{
    public class Mage : IJob
    {
        public string Name => "¸¶¹ı»ç";
        public Stats Stats => new Stats(80, 150, 10, 5, 1.10f, 1.15f);
    }
}
