using System;

namespace BikeShop.Common
{
    public class Scooter : Vehicle
    {
        public string Brand { get; set; } = "Xiaomi";
        public string Model { get; set; } = "Mi Pro 2";
        public void Honk() => Console.WriteLine($"[Сигнал] Скутер {Brand} {Model} робить: Піп-піп!");
    }
}