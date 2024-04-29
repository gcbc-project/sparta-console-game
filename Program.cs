using SpartaConsoleGame;

internal class Program
{
    static Player player = new Player("르탄", "전사");
    private static void Main(string[] args)
    {
        Menu mainMenu = new Menu();

        mainMenu.SetDesc("스파르타 마을에 오신 것을 환영합니다! \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

        mainMenu.AddMenuItem("상태보기", () =>
        {
            Console.WriteLine("test");
            Console.ReadKey();
            SatusMenu();
        });
        mainMenu.AddMenuItem("인벤토리", () => Console.WriteLine("인벤토리 소환"));
        mainMenu.AddMenuItem("상점", () => Console.WriteLine("상점 소환"));

        mainMenu.Run();
    }

    public static void SatusMenu()
    {
        Console.Clear();

        Menu mainMenu = new Menu();

        mainMenu.SetTitle("[스테이터스]");
        mainMenu.SetInfo(player.GetStatus());

        mainMenu.Run();
    }
}