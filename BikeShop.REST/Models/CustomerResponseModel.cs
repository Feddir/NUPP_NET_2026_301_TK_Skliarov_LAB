namespace BikeShop.REST.Models;

public class CustomerResponseModel
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal DiscountPercent { get; set; }

    public string? Address { get; set; }

    public DateTime? RegistrationDate { get; set; }
}