using BikeShop.Common;

namespace BikeShop.Infrastructure.Models;

public class OrderModel : IEntity
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public Guid CustomerId { get; set; }

    public CustomerModel Customer { get; set; } = null!;

    public Guid BikeId { get; set; }

    public BikeModel Bike { get; set; } = null!;

    public decimal GetTotalPrice()
    {
        return UnitPrice * Quantity;
    }

    public override string ToString()
    {
        return $"Замовлення: велосипед Id {BikeId}, кількість: {Quantity}, сума: {GetTotalPrice()} грн";
    }
}