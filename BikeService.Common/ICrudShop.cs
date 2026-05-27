namespace BikeShop.Common
{
    public interface ICrudShop<T> where T : class
    {
        void Create(T item);
        void Save(string filePath);
    }
}