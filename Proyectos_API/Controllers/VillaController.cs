using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyectos_API.Datos;
using Proyectos_API.Models;
using Proyectos_API.Models.Dto;

namespace Proyectos_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error al traer la villa con el id: " + id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {

            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            if(_db.Villas.FirstOrDefault(v=>v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                return BadRequest();
            }

            if(villaDto == null)
            {
                return BadRequest();
            }

            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa modelo = new()
            {
                Nombre = villaDto.Nombre,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad,
                ImagenURL = villaDto.ImagenURL,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa
            };

            _db.Villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new {id = villaDto.Id}, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult EliminarVilla(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v=>v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if(villaDto == null || id != villaDto.Id) 
            {
                return BadRequest();
            }

            Villa modelo = new()
            {
                Id = villaDto.Id,
                ImagenURL = villaDto.ImagenURL,
                Nombre = villaDto.Nombre,
                Tarifa = villaDto.Tarifa,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Detalle = villaDto.Detalle
            };
            _db.Villas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PatchVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id <= 0)
            {
                return BadRequest();
            }
            
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v=> v.Id == id);

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Tarifa = villa.Tarifa,
                Ocupantes= villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                Detalle= villa.Detalle,
                Amenidad = villa.Amenidad,
                ImagenURL = villa.ImagenURL
            };

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre= villaDto.Nombre,
                Ocupantes = villaDto.Ocupantes,
                Detalle = villaDto.Detalle,
                Amenidad= villaDto.Amenidad,
                Tarifa= villaDto.Tarifa,
                ImagenURL= villaDto.ImagenURL,
                MetrosCuadrados = villaDto.MetrosCuadrados
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
