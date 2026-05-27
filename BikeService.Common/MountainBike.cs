using System;

namespace BikeShop.Common
{
    public class MountainBike : Bicycle
    {
        public string Suspension { get; set; } = "Standard";
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Тип: Гірський, Амортизація: {Suspension}");
        }
    }
}