using SimpleStoreDI.Models;
using System.Reflection;

namespace SimpleStoreDI.Services
{
    public class MockedDataService<T> : IDataService<T> where T : class
    {
        private List<object> _mockedObjects = new List<object>  //todo - this repo is being overwritten with each endpoint call. Not possible to store new data here!
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
            var sameTypeObjects = _mockedObjects.OfType<T>().ToList();
            var maxId = GetMaxId(sameTypeObjects);
            UpdateObjectId(t, maxId + 1);
            _mockedObjects.Add(t);
        }

        private int GetMaxId(List<T> sameTypeObjects)
        {
            List<int> existingIds = new List<int>();

            foreach (var obj in sameTypeObjects)
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        if (prop.Name.Equals("Id"))
                        {
                            existingIds.Add((int)(prop.GetValue(obj) ?? 0));
                        }
                    }
                }
            }

            return existingIds.Any() ? existingIds.Max() : 0;
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
            var sameTypeObjects = _mockedObjects.OfType<T>().ToList();

            return sameTypeObjects;
        }

        public T GetById(int id)
        {
            List<T> sameTypeObjects = _mockedObjects.OfType<T>().ToList();

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
