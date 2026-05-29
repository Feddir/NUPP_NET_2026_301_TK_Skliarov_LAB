namespace BikeShop.Common;

public class Order : IEntity
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public Product Product { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }

    // конструктор
    public Order(Customer customer, Product product, int quantity)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        Product = product;
        Quantity = quantity;
        OrderDate = DateTime.Now;
    }

    // метод
    public decimal GetTotalPrice()
    {
        return Product.Price * Quantity;
    }

    public override string ToString()
    {
        return $"Замовлення: {Customer.FullName}, товар: {Product.Name}, кількість: {Quantity}, сума: {GetTotalPrice()} грн";
    }
}