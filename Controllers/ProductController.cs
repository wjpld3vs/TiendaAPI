using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Models;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            // podria tener mas validaciones
            if (product.Name == string.Empty || product.Price < 0)
                ModelState.AddModelError("Error al crear producto","Agregue un nombre valido y un precio correcto");
            
            await _productService.Create(product);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] Product product, string id)
        {
            if (product == null)
                return BadRequest();

            // podria tener mas validaciones
            if (product.Name == string.Empty || product.Price < 0)
                ModelState.AddModelError("Error al actualizar producto", "Agregue un nombre valido y un precio correcto");

            product.Id = new MongoDB.Bson.ObjectId(id);
            await _productService.Update(product);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        { 
            await _productService.Delete(id);
            return NoContent(); // success
        }


    }
}
