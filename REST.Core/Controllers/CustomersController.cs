using Microsoft.AspNetCore.Mvc;
using SimpleStoreDI.Models;
using SimpleStoreDI.Services;

namespace SimpleStoreDI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IDataService<Customer> _dataService;

        public CustomersController(IDataService<Customer> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllItems()
        {
            return Ok(_dataService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var item = _dataService.GetById(id);

            if (item == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(item); // 200 OK
        }

        [HttpPost]
        public ActionResult<Customer> PostProduct(Customer item)
        {
            _dataService.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); // 201 Created
        }
    }
}
