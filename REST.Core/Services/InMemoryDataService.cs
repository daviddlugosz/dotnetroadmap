using REST.Core.Models;

namespace REST.Core.Services
{
    public class InMemoryDataService<T> : IDataService<T> where T : class
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Product> _products = new List<Product>();
        public void Add(T t)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
