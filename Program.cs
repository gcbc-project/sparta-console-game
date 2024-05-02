using SpartaConsoleGame;
using System;
using System.Numerics;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        DataManager.Instance.InitShopItem();
        DataManager.Instance.InitDungeon();
        Menu.StartMenu();
    }
}