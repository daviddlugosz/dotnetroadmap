using REST.Core.Models;
using REST.Core.Utils;

namespace REST.Core.Services
{
    public class InMemoryDataService<T> : IDataService<T> where T : IId
    {
        private int _requestCounter;
        private List<IId> _data = new List<IId>
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
                Description = "Red item-shirt size XXL",
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

        public void Add(T item)
        {
            _requestCounter++;


            if (_data.Any(x => x.Id == item.Id))
            {
                return;
            }

            _data.Add(item);
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

            return item;
        }

        public T? Update(T updatedItem)
        {
            _requestCounter++;
            var id = GenericCollectionOperations<T?>.GetObjectId(updatedItem);

            if (id != null)
            {
                var existingItem = GenericCollectionOperations<T?>.GetById(_data, (int)id);

                if (existingItem != null)
                {
                    _data.Remove(existingItem); //delete existing
                    _data.Add(updatedItem);   //add new(updated) with same id

                    return updatedItem;
                }
            }

            return default(T);
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
