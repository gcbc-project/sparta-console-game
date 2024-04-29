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
    }
}
