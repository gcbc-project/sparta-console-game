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
        public MenuItem(string label, Action action)
        {
            Label = label; Action = action;
        }
    }
    
    internal class Menu
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Func<string> Info { get; private set; }

        public Action RefreshMenu { get; private set; }

        private List<MenuItem> menuItems;


        public Menu()
        {
            menuItems = new List<MenuItem>();
        }

        public void SetRefreshMenu(Action refreshMenu) 
        {
            RefreshMenu = () => 
            { 
                menuItems.Clear(); 
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
            Info = info;
            
        }

        public void AddMenuItem(string option, Action action)
        {

            menuItems.Add(new MenuItem(option, action));
        }

        public void Display()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            if (Info != null)
            { 
                Console.WriteLine(Info());            
            }
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Label}");
            }
            
            Console.WriteLine("\n0. 나가기");
        }


        public int HandleChoice()
        {


            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int c) && c <= menuItems.Count)
            {
                return c;
            }
            else
            {
                Console.WriteLine("유효하지 않은 선택입니다. 다시 시도해주세요.");
                Thread.Sleep(1000);
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
                    menuItems[choice - 1].Action.Invoke();
                }
                else if (choice == 0)
                {
                    break;
                }
            }
        }

    }
}
