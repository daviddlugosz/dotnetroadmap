using REST.Core.Models;

namespace REST.Core.Services
{
    public interface IDataService<T> where T : IId
    {
        void Add(T item);
        ICollection<T> GetAll();
        T? GetById(int id);
        T? Update(T updatedItem);
        T? Delete(int id);
    }
}
