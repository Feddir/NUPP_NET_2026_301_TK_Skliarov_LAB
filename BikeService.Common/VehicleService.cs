using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.Json;
using System.IO;

namespace BikeShop.Common
{
    public class VehicleService<T> : ICrudServiceAsync<T> where T : Vehicle
    {
        private readonly ConcurrentBag<T> _storage = new ConcurrentBag<T>();
        private readonly string _filePath = "data.json";

        public async Task<bool> CreateAsync(T element)
        {
            return await Task.Run(() => {
                _storage.Add(element);
                return true;
            });
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await Task.Run(() => _storage.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Task.Run(() => _storage.ToList());
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await Task.Run(() => _storage.Skip((page - 1) * amount).Take(amount).ToList());
        }

        public async Task<bool> UpdateAsync(T element)
        {
            return await Task.Run(() => {
                var item = _storage.FirstOrDefault(x => x.Id == element.Id);
                if (item == null) return false;

                var list = _storage.ToList();
                list.Remove(item);
                list.Add(element);

                while (!_storage.IsEmpty) _storage.TryTake(out _);
                foreach (var i in list) _storage.Add(i);

                return true;
            });
        }

        public async Task<bool> RemoveAsync(T element)
        {
            return await Task.Run(() => {
                var list = _storage.ToList();
                var removed = list.Remove(element);

                while (!_storage.IsEmpty) _storage.TryTake(out _);
                foreach (var i in list) _storage.Add(i);

                return removed;
            });
        }

        public async Task<bool> SaveAsync()
        {
            return await Task.Run(() => {
                string json = JsonSerializer.Serialize(_storage);
                File.WriteAllText(_filePath, json);
                return true;
            });
        }

        public IEnumerator<T> GetEnumerator() => _storage.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}