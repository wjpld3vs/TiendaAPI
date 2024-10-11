using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Models;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _customerService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _customerService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            // podria tener mas validaciones
            if (customer.Name == string.Empty)
                ModelState.AddModelError("Error al crear client", "Agregue un nombre valido");

            await _customerService.Create(customer);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] Customer customer, string id)
        {
            if (customer == null)
                return BadRequest();

            // podria tener mas validaciones
            if (customer.Name == string.Empty)
                ModelState.AddModelError("Error al actualizar cliente", "Agregue un nombre valido");

            customer.Id = new MongoDB.Bson.ObjectId(id);
            await _customerService.Update(customer);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.Delete(id);
            return NoContent(); // success
        }
    }
}
