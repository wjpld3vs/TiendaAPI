using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Services;
using TiendaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace TiendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        { 
            var userValid = await _userService.Validate(user.Name, user.Password);

            if (userValid == null)
                return Unauthorized("Usuario invalido");

            var token = _userService.GenerateToken(userValid);

            return Ok(new { Token = token});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                await Create(user);

                return Ok("Usuario registrado exitosamente");
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            // podria tener mas validaciones
            if (user.Name == string.Empty)
                ModelState.AddModelError("Error al crear user", "Agregue un nombre valido");

            await _userService.Create(user);

            return Created("Created", true);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromBody] User user, string id)
        {
            if (user == null)
                return BadRequest();

            // podria tener mas validaciones
            if (user.Name == string.Empty)
                ModelState.AddModelError("Error al actualizar user", "Agregue un nombre valido");

            user.Id = new MongoDB.Bson.ObjectId(id);
            await _userService.Update(user);

            return Created("Updated", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return NoContent(); // success
        }

    }
}
