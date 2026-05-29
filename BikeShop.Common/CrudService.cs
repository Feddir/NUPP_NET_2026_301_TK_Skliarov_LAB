using System.Text.Json;

namespace BikeShop.Common;

public class CrudService<T> : ICrudService<T> where T : IEntity
{
    // вбудована колекція .NET для зберігання даних
    private readonly Dictionary<Guid, T> _storage = new();

    // метод створення
    public void Create(T element)
    {
        _storage[element.Id] = element;
    }

    // метод читання одного елемента
    public T Read(Guid id)
    {
        if (_storage.ContainsKey(id))
        {
            return _storage[id];
        }

        throw new Exception("Елемент не знайдено");
    }

    // метод читання всіх елементів
    public IEnumerable<T> ReadAll()
    {
        return _storage.Values;
    }

    // метод оновлення
    public void Update(T element)
    {
        if (_storage.ContainsKey(element.Id))
        {
            _storage[element.Id] = element;
        }
        else
        {
            throw new Exception("Елемент для оновлення не знайдено");
        }
    }

    // метод видалення
    public void Remove(T element)
    {
        _storage.Remove(element.Id);
    }

    // метод збереження даних у файл
    public void Save(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_storage.Values, options);

        File.WriteAllText(filePath, json);
    }

    // метод завантаження даних із файлу
    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не знайдено", filePath);
        }

        string json = File.ReadAllText(filePath);

        List<T>? elements = JsonSerializer.Deserialize<List<T>>(json);

        if (elements == null)
        {
            return;
        }

        _storage.Clear();

        foreach (var element in elements)
        {
            _storage[element.Id] = element;
        }
    }
}