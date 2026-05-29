using BikeShop.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Асинхронний thread-safe CRUD сервіс для веломагазину\n");

string bikesFilePath = "async_bikes.json";

// використовується той самий CrudService<T>, але вже асинхронний
CrudService<Bike> bikeService = new CrudService<Bike>(bikesFilePath);

int count = 1000;

// приклад AutoResetEvent
AutoResetEvent autoResetEvent = new AutoResetEvent(false);

// приклад SemaphoreSlim
SemaphoreSlim semaphore = new SemaphoreSlim(5);

// приклад lock
object consoleLock = new object();

Console.WriteLine("Початок паралельного створення велосипедів...");

// паралельне створення 1000 об'єктів
Parallel.For(0, count, i =>
{
    Bike bike = Bike.CreateNew();

    bikeService.CreateAsync(bike).Wait();

    if (i == 500)
    {
        autoResetEvent.Set();
    }
});

Console.WriteLine("Очікування сигналу від AutoResetEvent...");
autoResetEvent.WaitOne();

Console.WriteLine("Сигнал отримано. Частина об'єктів уже створена.");

Console.WriteLine("\nДемонстрація SemaphoreSlim:");

List<Task> tasks = new List<Task>();

for (int i = 0; i < 10; i++)
{
    int taskNumber = i;

    tasks.Add(Task.Run(async () =>
    {
        await semaphore.WaitAsync();

        try
        {
            lock (consoleLock)
            {
                Console.WriteLine($"Потік {taskNumber} отримав доступ до обмеженого ресурсу");
            }

            await Task.Delay(200);
        }
        finally
        {
            semaphore.Release();
        }
    }));
}

await Task.WhenAll(tasks);

Console.WriteLine("\nУсі додаткові потоки завершили роботу.");

IEnumerable<Bike> bikes = await bikeService.ReadAllAsync();

Console.WriteLine($"\nКількість створених велосипедів: {bikes.Count()}");

// мінімальне, максимальне та середнє значення ціни
decimal minPrice = bikes.Min(b => b.Price);
decimal maxPrice = bikes.Max(b => b.Price);
decimal averagePrice = bikes.Average(b => b.Price);

// мінімальне, максимальне та середнє значення розміру коліс
int minWheelSize = bikes.Min(b => b.WheelSize);
int maxWheelSize = bikes.Max(b => b.WheelSize);
double averageWheelSize = bikes.Average(b => b.WheelSize);

Console.WriteLine("\nСтатистика по цінах:");
Console.WriteLine($"Мінімальна ціна: {minPrice} грн");
Console.WriteLine($"Максимальна ціна: {maxPrice} грн");
Console.WriteLine($"Середня ціна: {averagePrice:F2} грн");

Console.WriteLine("\nСтатистика по розміру коліс:");
Console.WriteLine($"Мінімальний розмір коліс: {minWheelSize}");
Console.WriteLine($"Максимальний розмір коліс: {maxWheelSize}");
Console.WriteLine($"Середній розмір коліс: {averageWheelSize:F2}");

// демонстрація пагінації
Console.WriteLine("\nПерша сторінка, 10 велосипедів:");

IEnumerable<Bike> firstPage = await bikeService.ReadAllAsync(1, 10);

foreach (var bike in firstPage)
{
    Console.WriteLine(bike.GetInfo());
}

// демонстрація IEnumerable
Console.WriteLine("\nПеревірка роботи IEnumerable, перші 5 елементів:");

int printed = 0;

foreach (var bike in bikeService)
{
    Console.WriteLine(bike.GetInfo());

    printed++;

    if (printed == 5)
    {
        break;
    }
}

// збереження у файл
bool saved = await bikeService.SaveAsync();

if (saved)
{
    Console.WriteLine($"\nКолекцію збережено у файл: {bikesFilePath}");
}
else
{
    Console.WriteLine("\nПомилка під час збереження файлу");
}