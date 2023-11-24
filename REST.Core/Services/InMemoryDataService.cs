using REST.Core.Models;
using REST.Core.Utils;

namespace REST.Core.Services
{
    public class InMemoryDataService<T> : IDataService<T> where T : class
    {
        private int _requestCounter;
        private List<object> _data = new List<object>
        {
            new Customer
            {
                Id = 1,
                Name = "John Doe",
                Email = "jdoe@seznam.cz"
            },
            new Customer
            {
                Id = 2,
                Name = "Austin Powers",
                Email = "ap007@email.cz"
            },
            new Product
            {
                Id = 1,
                Name = "T-Shirt",
                Description = "Red t-shirt size XXL",
                Price = 1.25F
            },
            new Product
            {
                Id = 2,
                Name = "Jacket",
                Description = "Brown insulated ski jacket",
                Price = 2.50F
            },
            new Product
            {
                Id = 3,
                Name = "Cap",
                Description = "Baseball cap with peak",
                Price = 1.00F
            }
        };

        public InMemoryDataService()
        {
            _requestCounter = 0;
        }

        public void Add(T t)
        {
            _requestCounter++;
            var maxId = GenericCollectionOperations<T>.GetMaxId(_data);
            GenericCollectionOperations<T>.UpdateObjectId(t, maxId + 1);
            _data.Add(t);
        }

        public ICollection<T> GetAll()
        {
            _requestCounter++;
            return _data.OfType<T>().ToList();
        }

        public T? GetById(int id)
        {
            _requestCounter++;
            var item = GenericCollectionOperations<T?>.GetById(_data, id);

            return item == default(T) ? null : item;
        }

        public T? Update(T t)
        {
            _requestCounter++;
            var id = GenericCollectionOperations<T?>.GetObjectId(t);

            if (id != null)
            {
                var item = GenericCollectionOperations<T?>.GetById(_data, (int)id);

                if (item != null)
                {
                    _data.Remove(item); //delete existing
                    _data.Add(t);   //add new(updated) with same id

                    return t;
                }
            }

            return null;
        }

        public T? Delete(int id)
        {
            _requestCounter++;
            var item = GenericCollectionOperations<T?>.GetById(_data, id);
            
            if (item != null)
            {
                _data.Remove(item);
            }

            return item;
        }
    }
}
