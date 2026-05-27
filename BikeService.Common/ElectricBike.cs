using System;

namespace BikeShop.Common
{
    public class ElectricBike : Bicycle
    {
        public int MotorPower { get; set; } = 250;
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Тип: Електро, Потужність: {MotorPower}W");
        }
    }
}