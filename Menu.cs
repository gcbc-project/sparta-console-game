﻿namespace SpartaConsoleGame
{
    public struct MenuItem
    {
        public ConsoleBuilder Label { get; private set; }
        public Action Action { get; private set; }
        public Func<bool> IsAction { get; private set; }
        public MenuItem(ConsoleBuilder label, Action action, Func<bool> isAction)
        {
            Label = label;
            Action = action;
            IsAction = isAction;

        }
    }

    internal class Menu
    {
        public ConsoleBuilder Title { get; private set; }
        public ConsoleBuilder Description { get; private set; }
        public Action Info { get; private set; }
        public Action RefreshMenu { get; private set; }
        public bool IsExitHidden { get; private set; }
        public ConsoleBuilder ExitLabel { get; private set; } = new ConsoleBuilder("나가기", () => true, ConsoleColor.Red);
        public Func<bool> IsSkip { get; set; }
        private List<MenuItem> _menuItems;



        public Menu()
        {
            _menuItems = new List<MenuItem>();
        }

        public void SetExit(bool isExitHidden = false, string exitLabel = null, bool isNewLine = true)
        {
            if (exitLabel != null)
            {
                ExitLabel = new ConsoleBuilder(exitLabel);
            }
            IsExitHidden = isExitHidden;
        }

        public void SetExit(ConsoleBuilder exitLabel, bool isExitHidden = false)
        {
            if (exitLabel != null)
            {
                ExitLabel = exitLabel;
            }
            IsExitHidden = isExitHidden;
        }

        public void SetIsSkip(Func<bool> isSkip)
        {
            IsSkip = isSkip;
        }


        public void SetRefreshMenu(Action refreshMenu)
        {
            RefreshMenu = () =>
            {
                _menuItems.Clear();
                refreshMenu();
            };
        }
        public void SetTitle(string title, bool isNewLine = true)
        {
            Title = new ConsoleBuilder(title, isNewLine: isNewLine);
        }

        public void SetTitle(ConsoleBuilder title)
        {
            Title = title;
        }

        public void SetDesc(string desc, bool isNewLine = true)
        {
            Description = new ConsoleBuilder(desc, isNewLine: isNewLine);
        }
        public void SetDesc(ConsoleBuilder desc)
        {
            Description = desc;
        }

        public void SetInfo(Func<string> info, bool isRefresh = false, bool isNewLine = true)
        {
            if (isRefresh)
            {
                Info = () => { new ConsoleBuilder(info(), isNewLine: isNewLine).Display(); };
            }
            else
            {
                Info += () => { new ConsoleBuilder(info(), isNewLine: isNewLine).Display(); };
            }
        }
        public void SetInfo(Func<ConsoleBuilder> info, bool isRefresh = false)
        {
            if (isRefresh)
            {
                Info = () => { info().Display(); };
            }
            else
            {
                Info += () => { info().Display(); };
            }
        }
        public void AddMenuItem(string option, Action action, Func<bool> isAction = null, bool isNewLine = true)
        {
            _menuItems.Add(new MenuItem(new ConsoleBuilder(option, isNewLine: isNewLine), action, isAction));
        }

        public void AddMenuItem(ConsoleBuilder option, Action action, Func<bool> isAction = null)
        {
            _menuItems.Add(new MenuItem(option, action, isAction));
        }

        public void Display()
        {
            Title?.Display();
            Description?.Display();
            if (Info != null)
            {
                Console.WriteLine();
                Info?.Invoke();
            }
            if (_menuItems.Count != 0)
            {
                Console.WriteLine();
                for (int i = 0; i < _menuItems.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    _menuItems[i].Label?.Display();
                }
                Console.WriteLine();
            }

            if (IsExitHidden == false)
            {
                Console.Write($"0. ");
                ExitLabel?.Display();
            }
        }

        public int HandleChoice()
        {
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요. >> ");
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
                if (IsSkip != null && IsSkip.Invoke())
                {
                    break;
                }
                Console.Clear();
                if (RefreshMenu != null)
                {
                    RefreshMenu();
                }
                Display();

                int choice = HandleChoice();
                if (choice > 0)
                {
                    if (_menuItems[choice - 1].IsAction == null ||
                        _menuItems[choice - 1].IsAction.Invoke())

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

        public void StartMenuDesc()
        {
            Title.Display();
            Description.Display();
        }
        public static void ChooseGame()
        {
            Menu chooseGameMenu = new Menu();
            chooseGameMenu.AddMenuItem("새 게임", StartMenu);
            chooseGameMenu.AddMenuItem("불러오기", () =>
            {
                DataManager.Instance.LoadGameData();
                if (DataManager.Instance.Player == null)
                {
                    Console.WriteLine("저장된 데이터가 없습니다.");
                    Thread.Sleep(500);
                }
                else
                {
                    MainMenu();
                }
            });
            chooseGameMenu.SetExit(isExitHidden: true);
            chooseGameMenu.Run();
        }

        public static void StartMenu()
        {
            Console.Clear();
            Menu mainMenu = new Menu();

            mainMenu.SetTitle("스파르타 RPG에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.\n");
            mainMenu.SetDesc("이름을 입력해주세요 : ", isNewLine: false);
            mainMenu.StartMenuDesc();
            string playerName = Console.ReadLine();

            JobChoiceMenu(playerName);
        }

        public static void JobChoiceMenu(string playerName)
        {
            Menu jobMenu = new Menu();
            jobMenu.SetTitle("[직업 선택]");
            jobMenu.SetDesc("직업을 선택해주세요");

            jobMenu.AddMenuItem("전사", () =>
            {
                DataManager.Instance.CreatePlayer(new Warrior(playerName));
                DataManager.Instance.InitGame();
                MainMenu();  // 직업 선택 후 메인 메뉴 호출
            });
            jobMenu.AddMenuItem("마법사", () =>
            {
                DataManager.Instance.CreatePlayer(new Mage(playerName));
                DataManager.Instance.InitGame();
                MainMenu();  // 직업 선택 후 메인 메뉴 호출
            });
            jobMenu.SetExit(true);
            jobMenu.Run();
        }

        public static void MainMenu()
        {
            Menu mainMenu = new Menu();
            mainMenu.SetTitle("스파르타 마을에 오신 것을 환영합니다!");
            mainMenu.SetDesc("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            mainMenu.AddMenuItem("상태보기", StatusMenu);
            mainMenu.AddMenuItem("인벤토리", InvenMenu);
            mainMenu.AddMenuItem("상점", ShopMenu);
            mainMenu.AddMenuItem("던전 입장", DungeonMenu, () => !DataManager.Instance.Player.IsDead);
            mainMenu.AddMenuItem("휴식하기", RestMenu);
            mainMenu.AddMenuItem("저장하기", SaveMenu);
            mainMenu.AddMenuItem("게임종료", ExitGame);
            mainMenu.SetExit(true);

            mainMenu.Run();

        }

        public static void SaveMenu()
        {
            Console.Clear();
            Menu saveMenu = new Menu();
            saveMenu.SetTitle("[게임 저장]");
            saveMenu.SetDesc("게임을 저장 하시겠습니까?\n");
            saveMenu.AddMenuItem("네", () =>
            {
                SaveManager.SaveData("Player", DataManager.Instance.Player);
                SaveManager.SaveData("Shop", DataManager.Instance.Shop);
                SaveManager.SaveData("DungeonManager", DataManager.Instance.DungeonManager);
            });
            saveMenu.SetExit(false, "나가기");

            saveMenu.Run();
        }
        public static void ExitGame()
        {
            Console.Clear();
            Menu exitMenu = new Menu();
            exitMenu.SetDesc("정말 게임을 종료하시겠습니까?\n");
            exitMenu.AddMenuItem("네", () => Environment.Exit(0));
            exitMenu.SetExit(false, "아니요");

            exitMenu.Run();
        }

        public static void StatusMenu()
        {
            Console.Clear();
            Menu statusMenu = new Menu();


            statusMenu.SetTitle("[스테이터스]");
            statusMenu.SetInfo(DataManager.Instance.Player.GetStatus);

            statusMenu.Run();
        }

        public static void InvenMenu()
        {
            Console.Clear();

            Menu invenMenu = new Menu();

            invenMenu.SetTitle("[인벤토리]");
            invenMenu.SetInfo(() =>
            {

                if (DataManager.Instance.Player.Inventory.Items.Count == 0)
                {
                    return new ConsoleBuilder("인벤토리가 비었습니다\n");
                }
                else
                {
                    return DataManager.Instance.Player.Inventory.GetItemsInfo();
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
                for (int i = 0; i < DataManager.Instance.Player.Inventory.Items.Count; i++)
                {
                    int index = i;
                    equipMenu.AddMenuItem(DataManager.Instance.Player.Inventory.Items[index].GetItemInfo(), () => { DataManager.Instance.Player.Inventory.EquipedItem(index); });

                }
            });
            equipMenu.SetInfo(() =>
            {
                if (DataManager.Instance.Player.Inventory.Items.Count == 0)
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
            shopMenu.SetInfo(DataManager.Instance.Player.GetGold);
            shopMenu.SetInfo(DataManager.Instance.Shop.GetItemsInfo);
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
            buyMenu.SetInfo(DataManager.Instance.Player.GetGold);
            buyMenu.SetRefreshMenu(() =>
            {
                for (int i = 0; i < DataManager.Instance.Shop.Items.Count; i++)
                {
                    int index = i;
                    buyMenu.AddMenuItem(DataManager.Instance.Shop.Items[index].GetItemInfo(), () => { DataManager.Instance.Shop.Buy(DataManager.Instance.Player, index); });

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
            sellMenu.SetInfo(DataManager.Instance.Player.GetGold);
            sellMenu.SetRefreshMenu(() =>
            {
                for (int i = 0; i < DataManager.Instance.Player.Inventory.Items.Count; i++)
                {
                    int index = i;
                    sellMenu.AddMenuItem(DataManager.Instance.Player.Inventory.Items[index].GetItemInfo(), () => { DataManager.Instance.Shop.Sell(DataManager.Instance.Player, index); });
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

            for (int i = 0; i < DataManager.Instance.DungeonManager.DungeonList.Count; i++)
            {
                int index = i;
                dungeonMenu.AddMenuItem(DataManager.Instance.DungeonManager.DungeonList[index].GetDungeonInfo(), () => DungeonResultMenu(index));
            }

            dungeonMenu.Run();
        }

        public static void DungeonResultMenu(int index)
        {

            DataManager.Instance.DungeonManager.Enter(DataManager.Instance.Player, index);

        }

        public static void RestMenu()
        {
            Console.Clear();
            Menu restMenu = new Menu();
            restMenu.SetTitle("[휴식하기]");
            restMenu.SetDesc("500 G 를 내면 체력을 회복할 수 있습니다.");
            restMenu.SetInfo(DataManager.Instance.Player.GetGold);

            restMenu.AddMenuItem("휴식하기", () =>
            {
                string result = DataManager.Instance.Player.Rest();
                Console.WriteLine(result);
                Thread.Sleep(500);
            });
            restMenu.Run();
        }


    }
}