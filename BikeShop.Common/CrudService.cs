using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;

namespace BikeShop.Common;

public class CrudService<T> : ICrudServiceAsync<T> where T : IEntity
{
    // thread-safe колекція .NET
    private readonly ConcurrentDictionary<Guid, T> _storage = new();

    private readonly string _filePath;

    // приклад використання lock
    private readonly object _lockObject = new();

    // приклад використання SemaphoreSlim
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    // конструктор
    public CrudService(string filePath)
    {
        _filePath = filePath;
    }

    // асинхронний метод створення
    public Task<bool> CreateAsync(T element)
    {
        bool result = _storage.TryAdd(element.Id, element);
        return Task.FromResult(result);
    }

    // асинхронний метод читання одного елемента
    public Task<T> ReadAsync(Guid id)
    {
        if (_storage.TryGetValue(id, out T? element))
        {
            return Task.FromResult(element);
        }

        throw new Exception("Елемент не знайдено");
    }

    // асинхронний метод читання всіх елементів
    public Task<IEnumerable<T>> ReadAllAsync()
    {
        IEnumerable<T> result = _storage.Values.ToList();
        return Task.FromResult(result);
    }

    // асинхронний метод читання з пагінацією
    public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        if (page <= 0)
        {
            page = 1;
        }

        if (amount <= 0)
        {
            amount = 10;
        }

        IEnumerable<T> result = _storage.Values
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToList();

        return Task.FromResult(result);
    }

    // асинхронний метод оновлення
    public Task<bool> UpdateAsync(T element)
    {
        if (!_storage.ContainsKey(element.Id))
        {
            return Task.FromResult(false);
        }

        _storage[element.Id] = element;
        return Task.FromResult(true);
    }

    // асинхронний метод видалення
    public Task<bool> RemoveAsync(T element)
    {
        bool result = _storage.TryRemove(element.Id, out _);
        return Task.FromResult(result);
    }

    // асинхронне збереження у файл
    public async Task<bool> SaveAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            List<T> elements;

            lock (_lockObject)
            {
                elements = _storage.Values.ToList();
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(elements, options);

            await File.WriteAllTextAsync(_filePath, json);

            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // реалізація IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        return _storage.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}