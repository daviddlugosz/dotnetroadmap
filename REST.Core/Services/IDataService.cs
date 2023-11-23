namespace REST.Core.Services
{
    public interface IDataService<T> where T : class
    {
        void Add(T t);
        ICollection<T> GetAll();
        T? GetById(int id);
    }
}
