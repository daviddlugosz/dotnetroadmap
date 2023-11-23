using SimpleStoreDI.Models;

namespace SimpleStoreDI.Services
{
    public class InMemoryDataService
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Product> _products = new List<Product>();
    }
}
