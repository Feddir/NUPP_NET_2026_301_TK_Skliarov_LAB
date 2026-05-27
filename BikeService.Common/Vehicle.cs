using System.Threading;

namespace BikeShop.Common
{
    public abstract class Vehicle
    {
        private static int _totalVehicles = 0;
        protected Vehicle() => Interlocked.Increment(ref _totalVehicles);
        public static int GetTotalVehicles() => _totalVehicles;
    }
}