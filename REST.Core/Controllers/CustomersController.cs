using Microsoft.AspNetCore.Mvc;
using REST.Core.Models;
using REST.Core.Services;

namespace REST.Core.Controllers
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
        public ActionResult<Customer> Create(Customer item)
        {
            _dataService.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); // 201 Created
        }

        [HttpPut]
        public ActionResult<Customer> Update(Customer item)
        {
            if (item.Id == 0)
            {
                return BadRequest(); // 400 Bad Request
            }

            var updatedItem = _dataService.Update(item);

            if (updatedItem == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(updatedItem); // 200 OK
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _dataService.Delete(id);

            if (item == null)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }
    }
}
