using System;

namespace BikeShop.Common
{
    public class TouringBike : Bicycle
    {
        public bool HasLuggageRack { get; set; } = true;

        public override void DisplayInfo() =>
            Console.WriteLine($"Туристичний: {Brand} {Model}, Багажник: {(HasLuggageRack ? "Так" : "Ні")}, Ціна: {Price:C}");

        public static TouringBike CreateNew()
        {
            var rnd = new Random();
            string[] brands = { "Author", "Kona", "Surly" };

            return new TouringBike
            {
                Brand = brands[rnd.Next(brands.Length)],
                Model = "T-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (4000 - 1200) + 1200),
                HasLuggageRack = true
            };
        }
    }
}