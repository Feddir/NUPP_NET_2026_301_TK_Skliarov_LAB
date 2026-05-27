using System;

namespace BikeShop.Common
{
    public abstract class Bicycle : Vehicle
    {
        public string Brand { get; set; } = "Unknown";
        public string Model { get; set; } = "Unknown";
        public virtual void DisplayInfo() => Console.WriteLine($"Бренд: {Brand}, Модель: {Model}");
    }
}