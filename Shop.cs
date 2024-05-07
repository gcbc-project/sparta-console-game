using System;
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

        public ConsoleBuilder GetItemsInfo()
        {
            ConsoleBuilder cb = new ConsoleBuilder();
            Items.ForEach(item => { cb.Combine(item.GetItemInfo()); });
            return cb;
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
            player.Inventory.RemoveItem(player.Inventory.Items[inventoryItemindex].BaseItem);
            player.Gold += sellItemPrice;
            Console.WriteLine($"\n{sellItemPrice}G에 판매를 완료했습니다.");
            Thread.Sleep(500);
        }
    }
}