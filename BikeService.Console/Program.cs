using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using BikeShop.Common;

namespace BikeShop.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var bikeService = new VehicleService<Bicycle>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== BIKE SHOP (LAB 2) ===");
                Console.WriteLine("1. Сгенерувати 1000 велосипедів (Паралельно)");
                Console.WriteLine("2. Показати статистику (LINQ)");
                Console.WriteLine("3. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await RunParallelGeneration(bikeService);
                        break;
                    case "2":
                        await ShowStatistics(bikeService);
                        break;
                    case "3":
                        running = false;
                        break;
                }
            }
        }

        static async Task RunParallelGeneration(VehicleService<Bicycle> service)
        {
            Console.WriteLine("Генерація даних...");
            Stopwatch sw = Stopwatch.StartNew();

            Parallel.For(0, 1000, i =>
            {
                var bike = Bicycle.CreateNew();
                service.CreateAsync(bike).GetAwaiter().GetResult();
            });

            sw.Stop();
            Console.WriteLine($"Готово! Створено за {sw.ElapsedMilliseconds} мс.");
            await service.SaveAsync();
            Console.WriteLine("Дані збережено у data.json");
        }

        static async Task ShowStatistics(VehicleService<Bicycle> service)
        {
            var allBikes = await service.ReadAllAsync();
            if (!allBikes.Any())
            {
                Console.WriteLine("Список порожній. Спочатку згенеруйте дані!");
                return;
            }

            var min = allBikes.Min(b => b.Price);
            var max = allBikes.Max(b => b.Price);
            var avg = allBikes.Average(b => b.Price);

            Console.WriteLine($"--- Статистика цін ---");
            Console.WriteLine($"Мінімальна: {min:F2}");
            Console.WriteLine($"Максимальна: {max:F2}");
            Console.WriteLine($"Середня: {avg:F2}");
        }
    }
}