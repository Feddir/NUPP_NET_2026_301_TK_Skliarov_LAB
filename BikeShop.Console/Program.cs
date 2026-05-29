using BikeShop.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Демонстрація роботи CRUD сервісу для веломагазину\n");

// створення CRUD сервісів
CrudService<Bike> bikeService = new CrudService<Bike>();
CrudService<Accessory> accessoryService = new CrudService<Accessory>();
CrudService<Customer> customerService = new CrudService<Customer>();
CrudService<Order> orderService = new CrudService<Order>();

// створення об'єктів
Bike bike1 = new Bike("Mountain Pro", 25000, "Trek", "M", 29);
Bike bike2 = new Bike("City Comfort", 15000, "Giant", "L", 28);

Accessory helmet = new Accessory("Шолом захисний", 1200, "Безпека", "Пластик", true);

Customer customer = new Customer(
    "Іван Петренко",
    "+380501112233",
    "ivan@gmail.com",
    10,
    2
);

// підписка на подію
bike1.PriceChanged += message => Console.WriteLine($"Подія: {message}");

// CREATE
bikeService.Create(bike1);
bikeService.Create(bike2);
accessoryService.Create(helmet);
customerService.Create(customer);

Console.WriteLine("Додані велосипеди:");
foreach (var bike in bikeService.ReadAll())
{
    Console.WriteLine(bike.GetInfo());
}

Console.WriteLine("\nДодані аксесуари:");
foreach (var accessory in accessoryService.ReadAll())
{
    Console.WriteLine(accessory.GetInfo());
}

Console.WriteLine("\nДодані клієнти:");
foreach (var item in customerService.ReadAll())
{
    Console.WriteLine(item.GetContactInfo());
}

// READ
Console.WriteLine("\nПошук велосипеда за Id:");
Bike foundBike = bikeService.Read(bike1.Id);
Console.WriteLine(foundBike.GetInfo());

// UPDATE
Console.WriteLine("\nОновлення ціни велосипеда:");
bike1.ChangePrice(23000);
bikeService.Update(bike1);

Console.WriteLine("Після оновлення:");
Console.WriteLine(bikeService.Read(bike1.Id).GetInfo());

// метод розширення
Console.WriteLine("\nПеревірка методу розширення:");
Console.WriteLine($"{bike1.Name} дорогий товар: {bike1.IsExpensive()}");

// створення замовлення
Order order = new Order(customer, bike1, 1);
orderService.Create(order);

Console.WriteLine("\nСтворене замовлення:");
foreach (var item in orderService.ReadAll())
{
    Console.WriteLine(item);
}

// REMOVE
Console.WriteLine("\nВидалення велосипеда City Comfort");
bikeService.Remove(bike2);

Console.WriteLine("Список велосипедів після видалення:");
foreach (var bike in bikeService.ReadAll())
{
    Console.WriteLine(bike.GetInfo());
}

// SAVE
Console.WriteLine("\nЗбереження даних у файли:");

string bikesFilePath = "bikes.json";
string accessoriesFilePath = "accessories.json";
string customersFilePath = "customers.json";

bikeService.Save(bikesFilePath);
accessoryService.Save(accessoriesFilePath);
customerService.Save(customersFilePath);

Console.WriteLine($"Велосипеди збережено у файл: {bikesFilePath}");
Console.WriteLine($"Аксесуари збережено у файл: {accessoriesFilePath}");
Console.WriteLine($"Клієнти збережено у файл: {customersFilePath}");

// LOAD
Console.WriteLine("\nЗавантаження даних із файлів:");

CrudService<Bike> loadedBikeService = new CrudService<Bike>();
CrudService<Accessory> loadedAccessoryService = new CrudService<Accessory>();
CrudService<Customer> loadedCustomerService = new CrudService<Customer>();

loadedBikeService.Load(bikesFilePath);
loadedAccessoryService.Load(accessoriesFilePath);
loadedCustomerService.Load(customersFilePath);

Console.WriteLine("\nВелосипеди, завантажені з файлу:");
foreach (var bike in loadedBikeService.ReadAll())
{
    Console.WriteLine(bike.GetInfo());
}

Console.WriteLine("\nАксесуари, завантажені з файлу:");
foreach (var accessory in loadedAccessoryService.ReadAll())
{
    Console.WriteLine(accessory.GetInfo());
}

Console.WriteLine("\nКлієнти, завантажені з файлу:");
foreach (var item in loadedCustomerService.ReadAll())
{
    Console.WriteLine(item.GetContactInfo());
}

// статичні методи
Console.WriteLine($"\nЗагальна кількість створених товарів: {Product.GetTotalProducts()}");
Console.WriteLine($"Загальна кількість створених людей: {Person.GetTotalPersons()}");