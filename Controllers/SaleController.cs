using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Models;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly SaleService _saleService;

        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _saleService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _saleService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sale sale)
        {
            if (_saleService == null)
                return BadRequest();


            await _saleService.Create(sale);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] Sale sale, string id)
        {
            if (sale == null)
                return BadRequest();

            sale.Id = new MongoDB.Bson.ObjectId(id);
            await _saleService.Update(sale);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _saleService.Delete(id);
            return NoContent(); // success
        }
    }
}
