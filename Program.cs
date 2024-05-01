﻿using SpartaConsoleGame;
using System;
using System.Numerics;
using System.Reflection;

internal class Program
{
    static Player _player = new Player("르탄", "전사", 100, 0);
    static Shop _shop = new Shop();
    static DungeonManager _dungeonManager = new DungeonManager();


    private static void Main(string[] args)
    {
        InitGame();
        Menu mainMenu = new Menu();

        mainMenu.SetDesc("스파르타 마을에 오신 것을 환영합니다! \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

        mainMenu.AddMenuItem("상태보기", StatusMenu);
        mainMenu.AddMenuItem("인벤토리", InvenMenu);
        mainMenu.AddMenuItem("상점", ShopMenu);
        mainMenu.AddMenuItem("던전 입장", DungeonMenu);
        mainMenu.AddMenuItem("휴식하기", RestMenu);

        mainMenu.Run();
    }

    static void InitGame()
    {
        _shop = new Shop();
        _shop.Items.Add(new ShopItem("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, defense: 5));
        _shop.Items.Add(new ShopItem("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.Armor, defense: 9));
        _shop.Items.Add(new ShopItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor, defense: 15));
        _shop.Items.Add(new ShopItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon, attack: 2));
        _shop.Items.Add(new ShopItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon, attack: 5));
        _shop.Items.Add(new ShopItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.Weapon, attack: 7));
        _shop.Items.Add(new ShopItem("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.Weapon, attack: 15));

        _dungeonManager = new DungeonManager();
        _dungeonManager.DungeonList.Add(new Dungeon("쉬운", 5, 1000, 50));
        _dungeonManager.DungeonList.Add(new Dungeon("일반", 11, 1700, 75));
        _dungeonManager.DungeonList.Add(new Dungeon("어려운", 17, 2500, 100));
    }

    public static void StatusMenu()
    {
        Console.Clear();
        Menu statusMenu = new Menu();


        statusMenu.SetTitle("[스테이터스]");
        statusMenu.SetInfo(_player.GetStatus);

        statusMenu.Run();
    }

    public static void InvenMenu()
    {
        Console.Clear();

        Menu invenMenu = new Menu();

        invenMenu.SetTitle("[인벤토리]");
        invenMenu.SetInfo(() =>
        {

            if (_player.Inventory.Items.Count == 0)
            {
                return "인벤토리가 비었습니다\n";
            }
            else
            {
                return _player.Inventory.GetItemsInfo();
            }
        });
        invenMenu.AddMenuItem("장착관리", EquipMenu);

        invenMenu.Run();
    }

    public static void EquipMenu()
    {
        Console.Clear();

        Menu equipMenu = new Menu();

        equipMenu.SetTitle("[인벤토리]");
        equipMenu.SetRefreshMenu(() =>
        {
            for (int i = 0; i < _player.Inventory.Items.Count; i++)
            {
                int index = i;
                equipMenu.AddMenuItem(_player.Inventory.Items[index].GetItemInfo(), () => { _player.Inventory.EquipedItem(index); });

            }
        });
        equipMenu.SetInfo(() =>
        {
            if (_player.Inventory.Items.Count == 0)
            {
                return "인벤토리이 비었습니다";
            }
            return null;


        });
        equipMenu.Run();
    }

    public static void ShopMenu()
    {
        Console.Clear();
        Menu shopMenu = new Menu();

        shopMenu.SetTitle("[상점]\n");
        shopMenu.SetDesc($"필요한 아이템을 얻을 수 있는 상점입니다.\n");
        shopMenu.SetInfo(_player.GetGold);
        shopMenu.SetInfo(_shop.GetItemsInfo);
        shopMenu.AddMenuItem("아이템 구매", BuyMenu);
        shopMenu.AddMenuItem("아이템 판매", SellMenu);

        shopMenu.Run();
    }

    public static void BuyMenu()
    {
        Console.Clear();
        Menu buyMenu = new Menu();
        buyMenu.SetTitle("[상점 - 아이템 구매]\n");
        buyMenu.SetDesc($"필요한 아이템을 얻을 수 있는 상점입니다.");
        buyMenu.SetInfo(_player.GetGold);
        buyMenu.SetRefreshMenu(() =>
        {
            for (int i = 0; i < _shop.Items.Count; i++)
            {
                int index = i;
                buyMenu.AddMenuItem(_shop.Items[index].GetItemInfo(), () => { _shop.Buy(_player, index); });

            }
        });

        buyMenu.Run();
    }

    public static void SellMenu()
    {
        Console.Clear();
        Menu sellMenu = new Menu();
        sellMenu.SetTitle("[상점 - 아이템 판매]\n");
        sellMenu.SetDesc($"보유한 아이템을 팔 수 있는 상점입니다.\n");
        sellMenu.SetInfo(_player.GetGold);
        sellMenu.SetRefreshMenu(() =>
        {
            for (int i = 0; i < _player.Inventory.Items.Count; i++)
            {
                int index = i;
                sellMenu.AddMenuItem(_player.Inventory.Items[index].GetItemInfo(), () => { _shop.Sell(_player, index); });
            }
        });
        sellMenu.Run();
    }

    public static void DungeonMenu()
    {
        Console.Clear();
        Menu dungeonMenu = new Menu();
        dungeonMenu.SetTitle("[던전 입장]\n");
        dungeonMenu.SetDesc("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
        for (int i = 0; i < _dungeonManager.DungeonList.Count; i++)
        {
            int index = i;
            dungeonMenu.AddMenuItem(_dungeonManager.DungeonList[index].GetDungeonInfo(), () => DungeonResultMenu(index));
        }

        dungeonMenu.Run();
    }

    public static void DungeonResultMenu(int index)
    {
        Console.Clear();
        Menu dungeonClearMenu = new Menu();
        dungeonClearMenu.SetTitle("[던전 클리어]\n");

        dungeonClearMenu.SetInfo(() => _dungeonManager.Enter(_player, index));
        dungeonClearMenu.Run();
    }

    public static void RestMenu()
    {
        Console.Clear();
        Menu restMenu = new Menu();
        restMenu.SetTitle("[휴식하기]");
        restMenu.SetDesc("500 G 를 내면 체력을 회복할 수 있습니다.");
        restMenu.SetInfo(_player.GetGold);

        restMenu.AddMenuItem("휴식하기", () => _player.Rest());

        restMenu.Run();
    }
}