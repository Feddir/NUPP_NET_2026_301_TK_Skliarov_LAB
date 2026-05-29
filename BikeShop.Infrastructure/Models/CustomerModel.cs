using BikeShop.Common;

namespace BikeShop.Infrastructure.Models;

public class CustomerModel : IEntity
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal DiscountPercent { get; set; }

    public CustomerProfileModel? Profile { get; set; }

    public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();

    public static CustomerModel CreateNew()
    {
        string[] names =
        {
            "Іван Петренко",
            "Олег Шевченко",
            "Марія Коваль",
            "Анна Мельник"
        };

        return new CustomerModel
        {
            Id = Guid.NewGuid(),
            FullName = names[Random.Shared.Next(names.Length)],
            PhoneNumber = "+38050" + Random.Shared.Next(1000000, 9999999),
            Email = $"customer{Random.Shared.Next(1000, 9999)}@gmail.com",
            DiscountPercent = Random.Shared.Next(0, 21)
        };
    }

    public override string ToString()
    {
        return $"Клієнт: {FullName}, email: {Email}, знижка: {DiscountPercent}%";
    }
}