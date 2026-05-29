namespace BikeShop.Common;

public class Accessory : Product
{
    public string Category { get; set; }
    public string Material { get; set; }
    public bool IsUniversal { get; set; }

    // порожній конструктор
    public Accessory() : base()
    {
        Category = string.Empty;
        Material = string.Empty;
    }

    // конструктор
    public Accessory(string name, decimal price, string category, string material, bool isUniversal)
        : base(name, price)
    {
        Category = category;
        Material = material;
        IsUniversal = isUniversal;
    }

    // метод
    public override string GetInfo()
    {
        return $"Аксесуар: {Name}, категорія: {Category}, матеріал: {Material}, ціна: {Price} грн";
    }
}