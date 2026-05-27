using System;

namespace BikeShop.Common
{
    public static class VehicleExtensions
    {
        public static void ShowStatus(this Vehicle vehicle)
        {
            Console.WriteLine("Статус: Об'єкт успішно створено.");
        }
    }
}