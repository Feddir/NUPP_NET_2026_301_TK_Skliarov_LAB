using BikeShop.Common;

namespace BikeShop.Infrastructure.Models;

public class CustomerProfileModel : IEntity
{
    public Guid Id { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string Address { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public CustomerModel Customer { get; set; } = null!;
}