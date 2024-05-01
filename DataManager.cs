
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

        public static Player _player;
        public static Shop _shop;
        public static DungeonManager _dungeonManager;

        public DataManager()
        {
            _player = new Player("르탄", "전사", 100, 0);
            _shop = new Shop();
            _dungeonManager = new DungeonManager();
        }

        public static void InitShopItem()
        {
            _shop = new Shop();
            _shop.Items.Add(new ShopItem("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, defense: 5));
            _shop.Items.Add(new ShopItem("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.Armor, defense: 9));
            _shop.Items.Add(new ShopItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor, defense: 15));
            _shop.Items.Add(new ShopItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon, attack: 2));
            _shop.Items.Add(new ShopItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon, attack: 5));
            _shop.Items.Add(new ShopItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.Weapon, attack: 7));
            _shop.Items.Add(new ShopItem("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.Weapon, attack: 15));
        }
        public static void InitDungeon()
        {
            _dungeonManager = new DungeonManager();
            _dungeonManager.DungeonList.Add(new Dungeon("쉬운", 5, 1000, 50));
            _dungeonManager.DungeonList.Add(new Dungeon("일반", 11, 1700, 75));
            _dungeonManager.DungeonList.Add(new Dungeon("어려운", 17, 2500, 100));
        }
    }
}
