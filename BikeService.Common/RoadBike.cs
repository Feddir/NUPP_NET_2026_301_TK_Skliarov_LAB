using System;

namespace BikeShop.Common
{
    public class RoadBike : Bicycle
    {
        public override void DisplayInfo() =>
            Console.WriteLine($"Шосейник: {Brand} {Model}, Ціна: {Price:C}");

        public static RoadBike CreateNew()
        {
            var rnd = new Random();
            return new RoadBike
            {
                Brand = "Specialized",
                Model = "R-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (8000 - 1500) + 1500)
            };
        }
    }
}