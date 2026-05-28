using System;
using System.Threading;

namespace BikeShop.Common
{
    public abstract class Vehicle
    {
        private static int _totalVehicles = 0;
        public static int GetTotalVehicles() => _totalVehicles;

        public Guid Id { get; set; }

        public string Brand { get; set; } = "Unknown";
        public string Model { get; set; } = "Unknown";
        public decimal Price { get; set; }

        protected Vehicle()
        {
            Interlocked.Increment(ref _totalVehicles);
            Id = Guid.NewGuid(); 
        }

        public abstract void DisplayInfo();
    }
}