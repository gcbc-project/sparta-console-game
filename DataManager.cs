
using SpartaConsoleGame.Enemy;
using Newtonsoft.Json;
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
                Converters = { new PlayerConverter() },
                TypeNameHandling = TypeNameHandling.Auto
            });
            Shop = SaveManager.LoadData<Shop>("Shop");
            DungeonManager = SaveManager.LoadData<DungeonManager>("DungeonManager", new JsonSerializerSettings
            {
                Converters = { new IEnemyConverter() },
                TypeNameHandling = TypeNameHandling.Auto
            });
        }


        public void InitGame()
        {
            List<Item> items = new List<Item>()
            {
                new Item("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, new Stats(def: 5)),
                new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.Armor, new Stats(def: 9)),
                new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor, new Stats(def: 15)),
                new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 600, ItemType.Weapon, new Stats(atk: 2)),
                new Item("청동 도끼", "어디선가 사용됐던 것 같은 도끼입니다.", 1500, ItemType.Weapon, new Stats(atk: 5)),
                new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.Weapon, new Stats(atk: 7)),
                new Item("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.Weapon, new Stats(atk: 15)),
                new Item("HP 초급 포션", "HP를 30회복시켜주는 초급 포션입니다.", 100, ItemType.ConsumableItem, new Stats()),
                new Item("MP 초급 포션", "MP를 30회복시켜주는 초급 포션입니다.", 100, ItemType.ConsumableItem, new Stats())
            };
            Shop = new Shop();

            for (int i = 0; i < items.Count; i++)
            {
                Shop.Items.Add(new ShopItem(items[i].DeepCopy()));
            }

            DungeonManager = new DungeonManager();
            DungeonManager.DungeonList.Add(new Dungeon("쉬운", 5, 1000, 50, new List<IEnemy> { new Minion() }, new List<DropItem> { new DropItem(items[0].DeepCopy(), 0.25f), new DropItem(items[3].DeepCopy(), 0.25f), new DropItem(new Item("작은 결정석", "쉬운 던전 보상입니다.", 1000, ItemType.Misc, new Stats()), 100) }));
            DungeonManager.DungeonList.Add(new Dungeon("일반", 11, 1700, 75, new List<IEnemy> { new Minion(), new Voidling() }, new List<DropItem> { new DropItem(items[1].DeepCopy(), 0.2f), new DropItem(items[4].DeepCopy(), 0.2f), new DropItem(new Item("보통 결정석", "일반 던전 보상입니다.", 1500, ItemType.Misc, new Stats()), 100) }));
            DungeonManager.DungeonList.Add(new Dungeon("어려운", 17, 2500, 100, new List<IEnemy> { new Minion(), new Voidling(), new CannonMinion() }, new List<DropItem> { new DropItem(items[2].DeepCopy(), 0.1f), new DropItem(items[6].DeepCopy(), 0.1f), new DropItem(new Item("존나 큰 결정석", "어려운 던전 보상입니다.", 3000, ItemType.Misc, new Stats()), 100) }));

        }
    }
}
