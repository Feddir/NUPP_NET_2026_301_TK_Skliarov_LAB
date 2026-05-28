using System;

namespace BikeShop.Common
{
    public class ElectricBike : Bicycle
    {
        public int BatteryPower { get; set; }

        public override void DisplayInfo() =>
            Console.WriteLine($"Електробайк: {Brand} {Model}, Батарея: {BatteryPower}Wh, Ціна: {Price:C}");

        public static ElectricBike CreateNew()
        {
            var rnd = new Random();
            return new ElectricBike
            {
                Brand = "Cube",
                Model = "E-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (6000 - 2000) + 2000),
                BatteryPower = rnd.Next(300, 1000)
            };
        }
    }
}