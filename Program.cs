using SpartaConsoleGame;
using System;
using System.Reflection;

internal class Program
{
    static Player player = new Player("르탄", "전사");
    static Shop shop = new Shop();

    private static void Main(string[] args)
    {
        InitGame();
        Menu mainMenu = new Menu();

        mainMenu.SetDesc("스파르타 마을에 오신 것을 환영합니다! \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

        mainMenu.AddMenuItem("상태보기",StatusMenu);
        mainMenu.AddMenuItem("인벤토리", InvenMenu);
        mainMenu.AddMenuItem("상점", ShopMenu);
        mainMenu.Run();
    }

    static void InitGame()
    {
        shop = new Shop();
        shop.Items.Add(new ShopItem("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.AMOR, def: 5));
        shop.Items.Add(new ShopItem("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.AMOR, def: 9));
        shop.Items.Add(new ShopItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.AMOR, def: 15));
        shop.Items.Add(new ShopItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.WEAPON, atk: 2));
        shop.Items.Add(new ShopItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.WEAPON, atk: 5));
        shop.Items.Add(new ShopItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.WEAPON, atk: 7));
        shop.Items.Add(new ShopItem("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.WEAPON, atk: 15));
    }

    public static void StatusMenu()
    {
        Console.Clear();
        Menu statusMenu = new Menu();


        statusMenu.SetTitle("[스테이터스]");
        statusMenu.SetInfo(player.GetStatus);

        statusMenu.Run();
    }

    public static void InvenMenu()
    {
        Console.Clear();

        Menu invenMenu = new Menu();

        invenMenu.SetTitle("[인벤토리]");
        invenMenu.SetInfo(player.Inventory.GetItemsInfo);
        invenMenu.SetInfo(() => {

            if (player.Inventory.Items.Count == 0)
            {
                return "인벤토리가 비었습니다";
            }
            else
            {
                return player.Inventory.GetItemsInfo();
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
            for (int i = 0; i < player.Inventory.Items.Count; i++)
            {
                int index = i;
                equipMenu.AddMenuItem(player.Inventory.Items[index].GetItemInfo(), () => { player.Inventory.EquipedItem(index); });

            }
        });
        equipMenu.SetInfo(() => {
            if (player.Inventory.Items.Count == 0)
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

        shopMenu.SetTitle("[상점]");
        shopMenu.SetDesc($"필요한 아이템을 얻을 수 있는 상점입니다.\n[보유 골드]: {player.Gold} G\n");
        shopMenu.SetInfo(shop.GetItemsInfo);

        shopMenu.AddMenuItem("아이템 구매", BuyMenu);

        shopMenu.Run();
    }

    public static void BuyMenu()
    {
        Console.Clear();
        Menu buyMenu = new Menu();
        buyMenu.SetRefreshMenu(() =>
        {
            for (int i = 0; i < shop.Items.Count; i++)
            {
                int index = i;
                buyMenu.AddMenuItem(shop.Items[index].GetItemInfo(), () => { shop.Buy(player, index); });

            }
        });
        buyMenu.SetTitle("[상점 - 아이템 구매]");
        buyMenu.SetDesc($"필요한 아이템을 얻을 수 있는 상점입니다.\n[보유 골드]: {player.Gold} G\n");

        buyMenu.Run();
    }
}