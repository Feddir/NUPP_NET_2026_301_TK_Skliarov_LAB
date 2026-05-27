using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BikeShop.Common
{
    public class CrudShop<T> : ICrudShop<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Create(T item) => _items.Add(item);

        public void Save(string filePath)
        {
            string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}