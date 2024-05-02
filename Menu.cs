using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SpartaConsoleGame;

namespace SpartaConsoleGame
{
    public struct MenuItem
    {
        public string Label { get; private set; }
        public Action Action { get; private set; }
        public Func<bool> IsAction { get; private set; }
        public MenuItem(string label, Action action, Func<bool> isAction)
        {
            Label = label; 
            Action = action; 
            IsAction = isAction;
        }
    }

    internal class Menu
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Action Info { get; private set; }
        public Action RefreshMenu { get; private set; }
        private List<MenuItem> _menuItems;
        public string ExitLabel { get; private set; } = "나가기";
        public bool IsExitHidden { get; private set; }

        public Menu()
        {
            _menuItems = new List<MenuItem>();
        }

        public void SetRefreshMenu(Action refreshMenu)
        {
            RefreshMenu = () =>
            {
                _menuItems.Clear();
                refreshMenu();
            };
        }
        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetDesc(string desc)
        {
            Description = desc;
        }

        public void SetInfo(Func<string> info)
        {
            Info += () => { Console.WriteLine(info()); };
        }


        public void AddMenuItem(string option, Action action, Func<bool> isAction = null)
        {
            _menuItems.Add(new MenuItem(option, action, isAction));
        }

        public void SetExit(bool isExitHidden = false, string exitLabel = "나가기")
        {
            IsExitHidden = isExitHidden;
            ExitLabel = exitLabel;
        }

        public void Display()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Info?.Invoke();
            for (int i = 0; i < _menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuItems[i].Label}");
            }
            if (IsExitHidden == false)
            {
                Console.WriteLine($"\n0. {ExitLabel}");
            }
        }

        public int HandleChoice()
        {


            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int c) && c <= _menuItems.Count)
            {
                return c;
            }
            else
            {
                Console.WriteLine("유효하지 않은 선택입니다. 다시 시도해주세요.");
                Thread.Sleep(500);
                return -1;
            }

        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                if (RefreshMenu != null)
                {
                    RefreshMenu();
                }
                Display();

                int choice = HandleChoice();
                if (choice > 0)
                {
                    if (_menuItems[choice -1].IsAction == null || _menuItems[choice -1].IsAction())
                    {
                        _menuItems[choice - 1].Action.Invoke();
                    }
                }
                else if (choice == 0 && IsExitHidden == false)
                {
                    break;
                }
            }
        }

        public static void MainMenu()
        {
            Menu mainMenu = new Menu();

            mainMenu.SetDesc("스파르타 마을에 오신 것을 환영합니다! \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            mainMenu.AddMenuItem("상태보기", StatusMenu);
            mainMenu.AddMenuItem("인벤토리", InvenMenu);
            mainMenu.AddMenuItem("상점", ShopMenu);
            mainMenu.AddMenuItem("던전 입장", DungeonMenu);
            mainMenu.AddMenuItem("휴식하기", RestMenu);

            mainMenu.Run();

        }

        public static void StatusMenu()
        {
            Console.Clear();
            Menu statusMenu = new Menu();


            statusMenu.SetTitle("[스테이터스]");
            statusMenu.SetInfo(DataManager._player.GetStatus);

            statusMenu.Run();
        }

        public static void InvenMenu()
        {
            Console.Clear();

            Menu invenMenu = new Menu();

            invenMenu.SetTitle("[인벤토리]");
            invenMenu.SetInfo(() =>
            {

                if (DataManager._player.Inventory.Items.Count == 0)
                {
                    return "인벤토리가 비었습니다\n";
                }
                else
                {
                    return DataManager._player.Inventory.GetItemsInfo();
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
                for (int i = 0; i < DataManager._player.Inventory.Items.Count; i++)
                {
                    int index = i;
                    equipMenu.AddMenuItem(DataManager._player.Inventory.Items[index].GetItemInfo(), () => { DataManager._player.Inventory.EquipedItem(index); });

                }
            });
            equipMenu.SetInfo(() =>
            {
                if (DataManager._player.Inventory.Items.Count == 0)
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
            shopMenu.SetInfo(DataManager._player.GetGold);
            shopMenu.SetInfo(DataManager._shop.GetItemsInfo);
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
            buyMenu.SetInfo(DataManager._player.GetGold);
            buyMenu.SetRefreshMenu(() =>
            {
                for (int i = 0; i < DataManager._shop.Items.Count; i++)
                {
                    int index = i;
                    buyMenu.AddMenuItem(DataManager._shop.Items[index].GetItemInfo(), () => { DataManager._shop.Buy(DataManager._player, index); });

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
            sellMenu.SetInfo(DataManager._player.GetGold);
            sellMenu.SetRefreshMenu(() =>
            {
                for (int i = 0; i < DataManager._player.Inventory.Items.Count; i++)
                {
                    int index = i;
                    sellMenu.AddMenuItem(DataManager._player.Inventory.Items[index].GetItemInfo(), () => { DataManager._shop.Sell(DataManager._player, index); });
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
            for (int i = 0; i < DataManager._dungeonManager.DungeonList.Count; i++)
            {
                int index = i;
                dungeonMenu.AddMenuItem(DataManager._dungeonManager.DungeonList[index].GetDungeonInfo(), () => DungeonResultMenu(index));
            }

            dungeonMenu.Run();
        }

        public static void DungeonResultMenu(int index)
        {
            // Console.Clear();
            // Menu dungeonClearMenu = new Menu();
            // dungeonClearMenu.SetTitle("[던전 클리어]\n");

            // dungeonClearMenu.SetInfo(() => DataManager._dungeonManager.Enter(DataManager._player, index));
            // dungeonClearMenu.Run();
            DataManager._dungeonManager.Enter(DataManager._player, index);
        }

        public static void RestMenu()
        {
            Console.Clear();
            Menu restMenu = new Menu();
            restMenu.SetTitle("[휴식하기]");
            restMenu.SetDesc("500 G 를 내면 체력을 회복할 수 있습니다.");
            restMenu.SetInfo(DataManager._player.GetGold);

            restMenu.AddMenuItem("휴식하기", () =>
            {
                string result = DataManager._player.Rest();
                Console.WriteLine(result);
                Thread.Sleep(500);
            });
            restMenu.Run();
        }


    }
}
