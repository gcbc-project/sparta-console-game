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

        public void Buy(Player player, int index)
        {
            if (Items[index].Price <= player.Gold)
            {
                Items[index].IsPurchased = true;
                player.Inventory.Items.Add((InventoryItem)Items[index]);
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
        }
        public void Sell(Player player, int index)
        {
            Items[index].IsPurchased = false;
            player.Inventory.Items.Remove((InventoryItem)Items[index]);
            player.Gold += Convert.ToInt32(Math.Round(Items[index].Price * 0.85f));
            Console.WriteLine("판매를 완료했습니다.");
        }
    }
}