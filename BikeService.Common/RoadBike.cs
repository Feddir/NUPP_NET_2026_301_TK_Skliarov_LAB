using System;

namespace BikeShop.Common
{
    public class RoadBike : Bicycle
    {
        public int WheelSize { get; set; } = 28;
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Тип: Шосейний, Колеса: {WheelSize} дюймів");
        }
    }
}