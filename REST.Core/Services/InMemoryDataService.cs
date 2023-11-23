using REST.Core.Models;
using REST.Core.Utils;
using System.Reflection;

namespace REST.Core.Services
{
    public class InMemoryDataService<T> : IDataService<T> where T : class
    {
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

        public void Add(T t)
        {
            var maxId = GenericCollectionOperations<T>.GetMaxId(_data);
            UpdateObjectId(t, maxId + 1);
            _data.Add(t);
        }

        private void UpdateObjectId(T t, int newId)
        {
            Type type = t.GetType();
            PropertyInfo[] props = type.GetProperties();

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    if (prop.Name.ToLower().Equals("id"))
                    {
                        prop.SetValue(t, newId);
                        break;
                    }
                }
            }
        }

        public ICollection<T> GetAll()
        {
            var sameTypeObjects = _data.OfType<T>().ToList();

            return sameTypeObjects;
        }

        public T GetById(int id)
        {
            List<T> sameTypeObjects = _data.OfType<T>().ToList();

            foreach (var obj in sameTypeObjects)
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        if (prop.Name.ToLower().Equals("id"))
                        {
                            if (prop.GetValue(obj).Equals(id))
                            {
                                return obj;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
