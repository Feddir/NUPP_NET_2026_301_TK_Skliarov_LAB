using BikeShop.Common;

namespace BikeShop.Infrastructure.Models;

public class BikeModel : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string FrameSize { get; set; } = string.Empty;

    public int WheelSize { get; set; }

    public decimal Price { get; set; }

    public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();

    public static BikeModel CreateNew()
    {
        string[] names = { "Mountain Pro", "City Comfort", "Road Speed", "Gravel Expert" };
        string[] brands = { "Trek", "Giant", "Cube", "Merida" };
        string[] frameSizes = { "S", "M", "L", "XL" };

        return new BikeModel
        {
            Id = Guid.NewGuid(),
            Name = names[Random.Shared.Next(names.Length)],
            Brand = brands[Random.Shared.Next(brands.Length)],
            FrameSize = frameSizes[Random.Shared.Next(frameSizes.Length)],
            WheelSize = Random.Shared.Next(26, 30),
            Price = Random.Shared.Next(8000, 60000)
        };
    }

    public override string ToString()
    {
        return $"Велосипед: {Name}, бренд: {Brand}, рама: {FrameSize}, колеса: {WheelSize}, ціна: {Price} грн";
    }
}