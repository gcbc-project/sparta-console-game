using SpartaConsoleGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Stats Stats { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public ItemType Type { get; set; }

        public Item() { }
        public Item(string name, string desc, int price, ItemType type, Stats stats)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Desc = desc;
            Price = price;
            Type = type;
            Stats = stats.DeepCopy();
            Count = 1;
        }

        public virtual Item DeepCopy()
        {
            Item newItem = (Item)Activator.CreateInstance(GetType());

            newItem.Id = this.Id;
            newItem.Name = this.Name;
            newItem.Desc = this.Desc;
            newItem.Price = this.Price;
            newItem.Count = this.Count;
            newItem.Type = this.Type;
            newItem.Stats = this.Stats.DeepCopy();

            return newItem;
        }
    }
}