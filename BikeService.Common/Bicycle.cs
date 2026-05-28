namespace BikeShop.Common
{
    public class Bicycle : Vehicle
    {
        public string Type { get; set; } 

        public override void DisplayInfo() =>
            Console.WriteLine($"Bike: {Brand} {Model}, Type: {Type}, Price: {Price:C}");

        public static Bicycle CreateNew()
        {
            var rnd = new Random();
            string[] brands = { "Giant", "Specialized", "Trek", "Cannondale" };
            return new Bicycle
            {
                Brand = brands[rnd.Next(brands.Length)],
                Model = "B-" + rnd.Next(100, 999),
                Price = (decimal)(rnd.NextDouble() * (5000 - 500) + 500),
                Type = "Mountain"
            };
        }
    }
}