using SpartaConsoleGame;
using System;
using System.Numerics;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        DataManager.Instance.InitGame();
        Menu.ChooseGame();
    }
}