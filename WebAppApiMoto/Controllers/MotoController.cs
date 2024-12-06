using Microsoft.AspNetCore.Mvc;
using WebAppApiMoto.Model;
using WebAppApiMoto.Services;

namespace WebAppApiMoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] bool? Disponible = null)
        {
            return Ok(_motoService.GetAllMotos(Disponible));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var moto = _motoService.GetMotoByID(id); 
            if (moto == null)
            {
                return NotFound();
            }
            return Ok(moto);
        }

        [HttpPost]
        public IActionResult Post(AddUpdateMoto motoObject)
        {
            var moto = _motoService.AddMoto(motoObject);

            return Ok(new
            {
                message = "Moto creada exitosamente.",
                id = moto.Id
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] AddUpdateMoto motoObject)
        {
            var moto = _motoService.UpdateMoto(id, motoObject);
            if (moto == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Moto actualizada exitosamente.",
                id = moto.Id
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_motoService.DeleteMotoByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Moto eliminada exitosamente.",
                id = id
            });
        }
    }
}