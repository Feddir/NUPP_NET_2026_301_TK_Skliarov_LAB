using System;
using BikeShop.Common;

namespace BikeShop.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ: BIKE SHOP ===");
                Console.WriteLine("1. Велосипеди | 2. Скутери | 3. Вихід");
                Console.Write("Ваш вибір: ");

                string mainChoice = Console.ReadLine();

                if (mainChoice == "1") ShowBikeMenu();
                else if (mainChoice == "2")
                {
                    var myScooter = new Scooter();
                    myScooter.Honk();
                    Console.ReadLine();
                }
                else if (mainChoice == "3") running = false;
            }
        }

        static void ShowBikeMenu()
        {
            Console.WriteLine("\n1. Гірський | 2. Електро | 3. Шосейний | 4. Туристичний");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": new MountainBike { Brand = "Trek", Model = "Fuel EX" }.DisplayInfo(); break;
                case "2": new ElectricBike { Brand = "Cube", Model = "Reaction" }.DisplayInfo(); break;
                case "3": new RoadBike { Brand = "Specialized", Model = "Tarmac" }.DisplayInfo(); break;
                case "4": new TouringBike { Brand = "Author", Model = "Grand" }.DisplayInfo(); break;
            }
            Console.ReadLine();
        }
    }
}