using Microsoft.AspNetCore.Mvc;
using REST.Core.Models;
using REST.Core.Services;

namespace REST.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataService<Product> _dataService;

        public ProductsController(IDataService<Product> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllItems()
        {
            return Ok(_dataService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var item = _dataService.GetById(id);

            if (item == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(item); // 200 OK
        }

        [HttpPost]
        public ActionResult<Product> PostProduct(Product item)
        {
            _dataService.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); // 201 Created
        }
    }
}
