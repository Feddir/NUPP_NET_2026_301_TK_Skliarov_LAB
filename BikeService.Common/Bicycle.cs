using System;

namespace BikeShop.Common
{
    public class Bicycle : Vehicle
    {
        public string Type { get; set; } = "Mountain";

        public override void DisplayInfo() =>
            Console.WriteLine($"Велосипед: {Brand} {Model}, Тип: {Type}, Ціна: {Price:C}");
        public static Bicycle CreateNew(Random rnd = null)
        {
            rnd ??= new Random();
            string[] brands = { "Giant", "Specialized", "Trek", "Cannondale" };
            string[] types = { "Mountain", "Road", "Touring", "City" };

            return new Bicycle
            {
                Brand = brands[rnd.Next(brands.Length)],
                Model = "B-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (5000 - 500) + 500),
                Type = types[rnd.Next(types.Length)]
            };
        }
    }
}