using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Models;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productCategoryService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productCategoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return BadRequest();

            // podria tener mas validaciones
            if (productCategory.Name == string.Empty)
                ModelState.AddModelError("Error al crear product category", "Agregue un nombre valido");

            await _productCategoryService.Create(productCategory);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] ProductCategory productCategory, string id)
        {
            if (productCategory == null)
                return BadRequest();

            // podria tener mas validaciones
            if (productCategory.Name == string.Empty)
                ModelState.AddModelError("Error al actualizar cliente", "Agregue un nombre valido");

            productCategory.Id = new MongoDB.Bson.ObjectId(id);
            await _productCategoryService.Update(productCategory);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productCategoryService.Delete(id);
            return NoContent(); // success
        }

    }
}
