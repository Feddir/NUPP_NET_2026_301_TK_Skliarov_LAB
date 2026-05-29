namespace BikeShop.REST.Models;

public class BikeResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string FrameSize { get; set; } = string.Empty;

    public int WheelSize { get; set; }

    public decimal Price { get; set; }
}