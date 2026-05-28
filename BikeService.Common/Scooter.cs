using System;

namespace BikeShop.Common
{
    public class Scooter : Vehicle
    {
        public override void DisplayInfo() =>
            Console.WriteLine($"Скутер: {Brand} {Model}, Ціна: {Price:C}");

        public static Scooter CreateNew()
        {
            var rnd = new Random();
            string[] brands = { "Xiaomi", "Ninebot", "Dualtron" };
            return new Scooter
            {
                Brand = brands[rnd.Next(brands.Length)],
                Model = "S-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (1200 - 300) + 300)
            };
        }
    }
}