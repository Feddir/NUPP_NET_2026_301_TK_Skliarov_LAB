namespace BikeShop.Common;

public class Bike : Product
{
    public string Brand { get; set; }
    public string FrameSize { get; set; }
    public int WheelSize { get; set; }

    private static readonly Random Random = new();

    // делегат
    public delegate void PriceChangedHandler(string message);

    // подія
    public event PriceChangedHandler? PriceChanged;

    // порожній конструктор для десеріалізації
    public Bike() : base()
    {
        Brand = string.Empty;
        FrameSize = string.Empty;
    }

    // конструктор
    public Bike(string name, decimal price, string brand, string frameSize, int wheelSize)
        : base(name, price)
    {
        Brand = brand;
        FrameSize = frameSize;
        WheelSize = wheelSize;
    }

    // метод
    public void ChangePrice(decimal newPrice)
    {
        Price = newPrice;

        // виклик події
        PriceChanged?.Invoke($"Ціну велосипеда {Name} змінено на {Price} грн");
    }

    // перевизначений метод
    public override string GetInfo()
    {
        return $"Велосипед: {Name}, бренд: {Brand}, рама: {FrameSize}, колеса: {WheelSize}, ціна: {Price} грн";
    }

    // статичний метод створення нового об'єкта із згенерованими даними
    public static Bike CreateNew()
    {
        string[] names =
        {
            "Mountain Pro",
            "City Comfort",
            "Road Speed",
            "Gravel Expert",
            "Urban Ride"
        };

        string[] brands =
        {
            "Trek",
            "Giant",
            "Cube",
            "Scott",
            "Merida"
        };

        string[] frameSizes =
        {
            "S",
            "M",
            "L",
            "XL"
        };

        lock (Random)
        {
            string name = names[Random.Next(names.Length)];
            string brand = brands[Random.Next(brands.Length)];
            string frameSize = frameSizes[Random.Next(frameSizes.Length)];
            int wheelSize = Random.Next(26, 30);
            decimal price = Random.Next(8000, 60000);

            return new Bike(name, price, brand, frameSize, wheelSize);
        }
    }
}