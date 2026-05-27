using System;

namespace BikeShop.Common
{
    public class TouringBike : Bicycle
    {
        public int TrunkCapacity { get; set; } = 20;
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Тип: Туристичний, Багажник: {TrunkCapacity} л");
        }
    }
}