namespace BikeShop.Common;

public class Bike : Product
{
    public string Brand { get; set; }
    public string FrameSize { get; set; }
    public int WheelSize { get; set; }

    // делегат
    public delegate void PriceChangedHandler(string message);

    // подія
    public event PriceChangedHandler? PriceChanged;

    // порожній конструктор
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
}