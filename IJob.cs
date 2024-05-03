using SpartaConsoleGame;
using SpartaConsoleGame.Enemy;

namespace SpartaConsoleGame
{
    public interface IJob
    {
        string Name { get; }
        Stats Stats { get; }
    }
}
