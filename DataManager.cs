
using SpartaConsoleGame.Enemy;
using System.Numerics;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SpartaConsoleGame.JsonConverts;

namespace SpartaConsoleGame
{
    internal class DataManager
    {
        private static DataManager _instance;
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataManager();
                }
                return _instance;
            }
        }

        [JsonProperty]
        public Player Player { get; private set; }
        [JsonProperty]
        public Shop Shop { get; private set; }
        [JsonProperty]
        public DungeonManager DungeonManager { get; private set; }

        public DataManager()
        {
            Shop = new Shop();
            DungeonManager = new DungeonManager();
        }
        public void CreatePlayer(Player player)
        {
            Player = player;
        }
        public void LoadGameData()
        {
            Player = SaveManager.LoadData<Player>("Player", new JsonSerializerSettings
            {
                Converters = { new IJobConverter() },
                TypeNameHandling = TypeNameHandling.Auto
            });
            Shop = SaveManager.LoadData<Shop>("Shop");
            DungeonManager = SaveManager.LoadData<DungeonManager>("DungeonManager", new JsonSerializerSettings
            {
                Converters = { new IEnemyConverter() },
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public void InitShopItem()
        {
            Shop = new Shop();
            Shop.Items.Add(new ShopItem(new Item("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, new Stats(def: 5))));
            Shop.Items.Add(new ShopItem(new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.Armor, new Stats(def: 9))));
            Shop.Items.Add(new ShopItem(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor, new Stats(def: 15))));
            Shop.Items.Add(new ShopItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 600, ItemType.Weapon, new Stats(atk: 2))));
            Shop.Items.Add(new ShopItem(new Item("청동 도끼", "어디선가 사용됐던 것 같은 도끼입니다.", 1500, ItemType.Weapon, new Stats(atk: 5))));
            Shop.Items.Add(new ShopItem(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.Weapon, new Stats(atk: 7))));
            Shop.Items.Add(new ShopItem(new Item("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.Weapon, new Stats(atk: 15))));
        }
        public void InitDungeon()
        {
            DungeonManager = new DungeonManager();
            DungeonManager.DungeonList.Add(new Dungeon("쉬운", 5, 1000, 50, new List<IEnemy> { new Minion() }));
            DungeonManager.DungeonList.Add(new Dungeon("일반", 11, 1700, 75, new List<IEnemy> { new Minion(), new Voidling() }));
            DungeonManager.DungeonList.Add(new Dungeon("어려운", 17, 2500, 100, new List<IEnemy> { new Minion(), new Voidling(), new CannonMinion() }));
        }
    }
}
