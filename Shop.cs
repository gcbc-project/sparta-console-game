﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class Shop
    {
        public List<ShopItem> Items { get; set; }


        public Shop()
        {
            Items = new List<ShopItem>();
        }

        public string GetItemsInfo()
        {
            StringBuilder sb = new StringBuilder();
            Items.ForEach(item => { sb.AppendLine(item.GetItemInfo()); });
            return sb.ToString();
        }

        public void Buy(Player player, int index)
        {
            if (Items[index].Price <= player.Gold)
            {
                Items[index].IsPurchased = true;
                player.Inventory.Items.Add((InventoryItem)Items[index]);
                player.Gold -= Items[index].Price;
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Console.ReadKey();
        }
        public void Sell(Player player, int index)
        {
            int sellItemPrice = Convert.ToInt32(Math.Round(Items[index].Price * 0.85f));
            Items[index].IsPurchased = false;
            player.Inventory.Items.RemoveAt(index);
            player.Gold += sellItemPrice;
            Console.WriteLine($"{sellItemPrice}G에 판매를 완료했습니다.");
            Thread.Sleep(500);
        }
    }
}