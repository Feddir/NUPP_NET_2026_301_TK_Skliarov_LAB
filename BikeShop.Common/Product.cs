namespace BikeShop.Common;

public abstract class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // статичне поле
    public static int TotalProducts;

    // статичний конструктор
    static Product()
    {
        TotalProducts = 0;
    }

    protected Product()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
    }

    // конструктор
    protected Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        TotalProducts++;
    }

    // метод
    public virtual string GetInfo()
    {
        return $"{Name}, ціна: {Price} грн";
    }

    // статичний метод
    public static int GetTotalProducts()
    {
        return TotalProducts;
    }
}