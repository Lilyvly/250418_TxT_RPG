namespace TxTDungeon;

class Item
{
    public string Name;
    public string Type;
    public int Power;
    public string Description;
    public bool IsEquipped;

    public Item(string name, string type, int power, string description)
    {
        Name = name;
        Type = type;
        Power = power;
        Description = description;
        IsEquipped = false;
    }

}

class Program
{
    static void InitInventory()
    {
        inventory.Add(new Item("Old Sword", "Attack", 2, "A worn-out sword commonly found."));
        inventory.Add(new Item("Iron Armor", "Defense", 5, "Sturdy armor made of solid iron."));
        inventory.Add(new Item("Spear of Sparta", "Attack", 7, "A legendary spear used by Spartan warriors."));
    }

    static List<Item> inventory = new List<Item>();
    static int baseAttack = 10;
    static int baseDefense = 5;
    static int baseHP = 100;
    static int gold = 1500;
    static void Main(string[] args)
    {
        InitInventory();
        InitShop();

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Text Dungeon!");
            Console.WriteLine("Get Ready before enter the dengeon.");
            Console.WriteLine("1. Status");
            Console.WriteLine("2. Inventory");
            Console.WriteLine("3. Shop");
            Console.WriteLine("99. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Status();
                    break;
                case "2":
                    Inventory();
                    break;
                case "3":
                    Shop();
                    break;
                case "99":
                    Console.WriteLine("Exiting the game. Goodbye!");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void Status()
    {
        Console.Clear();
        Console.WriteLine("Status");

        // 장착 아이템에서 능력치 추가 계산
        int totalAttack = baseAttack;
        int totalDefense = baseDefense;

        foreach (Item item in inventory)
        {
            if (item.IsEquipped)
            {
                if (item.Type == "Attack") totalAttack += item.Power;
                if (item.Type == "Defense") totalDefense += item.Power;
            }
        }

        Console.WriteLine("Level: 1");
        Console.WriteLine("Character: Warrior");
        Console.WriteLine($"Health: {baseHP}");
        Console.WriteLine($"Strength: {totalAttack}");
        Console.WriteLine($"Defense: {totalDefense}");
        Console.WriteLine($"Gold : {gold} G\n");
        Console.WriteLine("Press 0 to go back.");
        Console.ReadLine();

    }

    static void EquipItem()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Equip Item");
            Console.WriteLine("Select an item to equip:");

            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < inventory.Count; i++)
            {
                string equipTag = inventory[i].IsEquipped ? "[E]" : " ";
                Console.WriteLine($"{i + 1}. {equipTag} {inventory[i].Name} - {inventory[i].Type} (Power: {inventory[i].Power})");
            }

            Console.WriteLine("0. Go Back");
            Console.Write("Choose an item to equip: ");

            string choice = Console.ReadLine();
            bool isParsed = int.TryParse(choice, out int itemIndex);
            if (!isParsed || itemIndex < 0 || itemIndex > inventory.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ReadLine();
                continue;
            }

            if (itemIndex == 0)
            {
                return;
            }

            Item selectedItem = inventory[itemIndex - 1];
            selectedItem.IsEquipped = !selectedItem.IsEquipped;

            Console.WriteLine($"You have {(selectedItem.IsEquipped ? "equipped" : "unequipped")} {selectedItem.Name}.");

            // 장착 후 상태 출력
            int totalAttack = baseAttack;
            int totalDefense = baseDefense;
            foreach (Item item in inventory)
            {
                if (item.IsEquipped)
                {
                    if (item.Type == "Attack") totalAttack += item.Power;
                    if (item.Type == "Defense") totalDefense += item.Power;
                }
            }

            Console.WriteLine("\nUpdated Stats:");
            Console.WriteLine($"Attack: {totalAttack}");
            Console.WriteLine($"Defense: {totalDefense}");

            Console.WriteLine("\nPress any key to continue...");
        }

    }
    static void Inventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Inventory");
            Console.WriteLine("Check your items here.");

            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Your Item List:");
                foreach (var item in inventory)
                {
                    string equipTag = item.IsEquipped ? "[E]" : " ";
                    Console.WriteLine($"{equipTag} {item.Name} - {item.Type} (Power: {item.Power})");
                    Console.WriteLine($"Description: {item.Description}");
                }


            }

            Console.WriteLine("\n1. Equip Item");
            Console.WriteLine("0. Go Back");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    EquipItem();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        }
    }

    class ShopItem
    {
        public Item Item;
        public int Price;
        public bool IsPurchased;

        public ShopItem(Item item, int price)
        {
            Item = item;
            Price = price;
            IsPurchased = false;
        }
    }
    static List<ShopItem> shopItems = new List<ShopItem>();

    static void InitShop()
    {
        shopItems.Add(new ShopItem(new Item("Training Armor", "Defense", 5, "Armor that helps with training."), 1000));
        shopItems.Add(new ShopItem(new Item("Iron Armor", "Defense", 9, "Solid armor made of iron."), 1500));
        shopItems.Add(new ShopItem(new Item("Spartan Armor", "Defense", 15, "Legendary armor of Spartan warriors."), 3500));
        shopItems.Add(new ShopItem(new Item("Old Sword", "Attack", 2, "A worn-out sword."), 600));
        shopItems.Add(new ShopItem(new Item("Bronze Axe", "Attack", 5, "Used-looking axe."), 1500));
        shopItems.Add(new ShopItem(new Item("Spear of Sparta", "Attack", 7, "A legendary spear."), 2000));
    }

    static void Shop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Shop - Buy Items");
            Console.WriteLine($"[Gold] {gold} G\n");

            int index = 1;
            foreach (var shopItem in shopItems)
            {
                string status = shopItem.IsPurchased ? "구매완료" : $"{shopItem.Price} G";
                Console.WriteLine($"{index}. {shopItem.Item.Name} | {shopItem.Item.Type} +{shopItem.Item.Power} | {shopItem.Item.Description} | {status}");
                index++;
            }

            Console.WriteLine("0. Go Back");
            Console.Write("Choose an item to buy: ");
            string input = Console.ReadLine();
            bool isParsed = int.TryParse(input, out int choice);

            if (!isParsed || choice < 0 || choice > shopItems.Count)
            {
                Console.WriteLine("Invalid input.");
                Console.ReadLine();
                continue;
            }

            if (choice == 0) return;

            var selected = shopItems[choice - 1];

            if (selected.IsPurchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (gold < selected.Price)
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            else
            {
                gold -= selected.Price;
                inventory.Add(selected.Item);
                selected.IsPurchased = true;
                Console.WriteLine($"'{selected.Item.Name}' 아이템을 구매했습니다!");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}

