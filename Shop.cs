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

        public void Buy(Player player, int shopItemindex)
        {
            if (Items[shopItemindex].BaseItem.Price <= player.Gold)
            {
                Items[shopItemindex].IsPurchased = true;
                player.Inventory.AddItem(Items[shopItemindex].BaseItem);
                player.Gold -= Items[shopItemindex].BaseItem.Price;
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Console.ReadKey();
        }

        public void Sell(Player player, int inventoryItemindex)
        {
            int sellItemPrice = Convert.ToInt32(Math.Round(player.Inventory.Items[inventoryItemindex].BaseItem.Price * 0.85f));
            ShopItem? findShopItem = Items.Find(item => item.BaseItem.Id == player.Inventory.Items[inventoryItemindex].BaseItem.Id);
            if (findShopItem != null)
            {
                findShopItem.IsPurchased = false;
            }
            player.Inventory.RemoveItem(player.Inventory.Items[inventoryItemindex].BaseItem);
            // player.Inventory.Items.RemoveAt(inventoryItemindex);
            player.Gold += sellItemPrice;
            Console.WriteLine($"\n{sellItemPrice}G에 판매를 완료했습니다.");
            Thread.Sleep(500);
        }
    }
}