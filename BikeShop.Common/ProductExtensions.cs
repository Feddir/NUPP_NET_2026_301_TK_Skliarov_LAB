namespace BikeShop.Common;

public static class ProductExtensions
{
    // метод розширення
    public static bool IsExpensive(this Product product)
    {
        return product.Price > 20000;
    }
}