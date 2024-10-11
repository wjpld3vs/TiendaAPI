using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Models;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _supplierService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _supplierService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Supplier supplier)
        {
            if (supplier == null)
                return BadRequest();

            // podria tener mas validaciones
            if (supplier.Name == string.Empty)
                ModelState.AddModelError("Error al crear supplier", "Agregue un nombre valido");

            await _supplierService.Create(supplier);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] Supplier supplier, string id)
        {
            if (supplier == null)
                return BadRequest();

            // podria tener mas validaciones
            if (supplier.Name == string.Empty)
                ModelState.AddModelError("Error al actualizar supplier", "Agregue un nombre valido");

            supplier.Id = new MongoDB.Bson.ObjectId(id);
            await _supplierService.Update(supplier);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _supplierService.Delete(id);
            return NoContent(); // success
        }
    }
}
