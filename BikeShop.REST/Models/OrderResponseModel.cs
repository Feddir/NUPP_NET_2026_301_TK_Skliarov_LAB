namespace BikeShop.REST.Models;

public class OrderResponseModel
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public Guid CustomerId { get; set; }

    public Guid BikeId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}