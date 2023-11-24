using REST.Core.Models;
using REST.Core.Utils;

namespace REST.Core.Services
{
    public class MockedDataService<T> : IDataService<T> where T : class
    {
        private static readonly string[] CustomerFirstNames = new[]
        {
            "John", "Joe", "Bill", "Kevin", "George", "Patrick", "Ellen", "Jane", "Nikki", "Sandra", "Kate", "Gwenn"
        };

        private static readonly string[] CustomerLastNames = new[]
        {
            "Doe", "Black", "Murray", "Spacey", "Lucas", "Stewart", "Paige", "Brown", "Carson", "Bullock", "Moss", "Stefani"
        };

        private static readonly string[] CustomerEmailNameSeparators = new[]
        {
            "", "-", "_", "."
        };

        private static readonly string[] CustomerEmailSuffixs = new[]
        {
            "@seznam.cz", "@email.cz","@gmail.com","@yahoo.com","@centrum.cz", "@hotmail.com", "@outlook.com"
        };

        private static readonly string[] ProductNames = new[]
        {
            "Driller", "Car", "Toilet", "Gun", "Baseball cap", "Shoes",
        };

        private static readonly string[] ProductAdjectives = new[]
        {
            "Awesome", "Useful", "Great", "Amazing", "Cheap", "Handy", "Comfortable","Easy-to-use","Efficient","Trendy", "Cool", "Powerful"
        };

        private readonly Random _rng = new Random();

        private List<object> _data = new List<object>();

        public MockedDataService()
        {
            //generate some initial data
            AddCustomers(3);
            AddProducts(4);
        }

        private void AddCustomers(int numberOfCustomers)
        {
            var id = GenericCollectionOperations<T>.GetMaxId(_data);

            for (var i = 0; i < numberOfCustomers; i++)
            {
                id++;
                var name = GetName();

                var customer = new Customer
                {
                    Id = id,
                    Name = name,
                    Email = GetEmail(name)
                };

                _data.Add(customer);
            }
        }
        
        private string GetName()
        {
            return $"{CustomerFirstNames[_rng.Next(CustomerFirstNames.Length)]} {CustomerLastNames[_rng.Next(CustomerLastNames.Length)]}";
        }

        private string GetEmail(string fullName)
        {
            var names = fullName.Split(" ");
            string email = $"{names[0].ToLower().Substring(0,_rng.Next(1,names[0].Length))}" +
                           $"{CustomerEmailNameSeparators[_rng.Next(CustomerEmailNameSeparators.Length)]}" +
                           $"{names[1].ToLower().Substring(0, _rng.Next(1,names[1].Length))}" +
                           $"{CustomerEmailSuffixs[_rng.Next(CustomerEmailSuffixs.Length)]}";

            return email;
        }

        private void AddProducts(int numberOfProducts)
        {
            var id = GenericCollectionOperations<T>.GetMaxId(_data);
            var notYetUsedProducts = ProductNames.ToList();

            for (var i = numberOfProducts - 1; i >= 0; i--)
            {
                id++;
                var productName = GetProductName(notYetUsedProducts);

                var product = new Product
                {
                    Id = id,
                    Name = productName,
                    Description = GetProductDescription(productName),
                    Price = GetPrice(2, 150)
                };

                _data.Add(product);
                notYetUsedProducts.RemoveAt(notYetUsedProducts.IndexOf(productName));
            }
        }

        private string GetProductName(List<string> productNames)
        {
            return $"{productNames[_rng.Next(productNames.Count)]}";
        }

        private string GetProductDescription(string productName)
        {
            string description = $"{ProductAdjectives[_rng.Next(ProductNames.Length)]}";
            var notYetUsedAdjectives = ProductAdjectives.Where(x=> x != description).ToList();
            var numberOfExtraAdjectives = _rng.Next(5);

            for (int i = numberOfExtraAdjectives - 1; i >= 0; i--)
            {
                description += $" {notYetUsedAdjectives[i]}".ToLower();
                notYetUsedAdjectives.RemoveAt(i);
            }

            return $"{description} {productName.ToLower()}";
        }

        private float GetPrice(float min, float max)
        {
            var price = (_rng.NextDouble() * (max - min) + min);
            price = Math.Round(price, 2);

            return (float)price;
        }

        public void Add(T t)
        {
            var maxId = GenericCollectionOperations<T>.GetMaxId(_data);
            GenericCollectionOperations<T>.UpdateObjectId(t, maxId + 1);
            _data.Add(t);
        }

        public ICollection<T> GetAll()
        {
            return _data.OfType<T>().ToList();
        }

        public T? GetById(int id)
        {
            var item = GenericCollectionOperations<T?>.GetById(_data, id);

            return item == default(T) ? null : item;
        }

        public T? Update(T t)
        {
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
            var item = GenericCollectionOperations<T?>.GetById(_data, id);

            if (item != null)
            {
                _data.Remove(item);
            }

            return item;
        }
    }
}
