namespace BikeShop.REST.Models;

public class OrderCreateModel
{
    public Guid CustomerId { get; set; }

    public Guid BikeId { get; set; }

    public int Quantity { get; set; }
}