namespace BikeShop.Common;

public class Customer : Person
{
    public string Email { get; set; }
    public decimal DiscountPercent { get; set; }
    public int OrdersCount { get; set; }

    // конструктор
    public Customer(string fullName, string phoneNumber, string email, decimal discountPercent, int ordersCount)
        : base(fullName, phoneNumber)
    {
        Email = email;
        DiscountPercent = discountPercent;
        OrdersCount = ordersCount;
    }

    // метод
    public decimal CalculateDiscount(decimal price)
    {
        return price - price * DiscountPercent / 100;
    }

    public override string GetContactInfo()
    {
        return $"Клієнт: {FullName}, email: {Email}, знижка: {DiscountPercent}%";
    }
}