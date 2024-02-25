using L01_2021AA602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2021AA602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public PlatoController(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }
        // GET: api/<PlatoController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Plato> listadoPlatos = (from e in _restauranteContext.Platos
                                           select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);
        }

        // GET api/<PlatoController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            Plato? plato = (from e in _restauranteContext.Platos
                              where e.PlatoId == id
                              select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(plato);
            }
        }

        // POST api/<PlatoController>
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] Plato plato)
        {
            try
            {
                _restauranteContext.Platos.Add(plato);
                _restauranteContext.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PlatoController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Modificarplato(int id, [FromBody] Plato platoModificar)
        {
            Plato? platoActual = (from e in _restauranteContext.Platos
                                  where e.PlatoId == id
                                  select e).FirstOrDefault();
            if (platoActual == null)
            {
                return NotFound();
            }
            else
            {
                platoActual.NombrePlato = platoModificar.NombrePlato;
                platoActual.Precio = platoModificar.Precio;

            }
            _restauranteContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(platoActual);

        }

        // DELETE api/<PlatoController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Plato? plato = (from e in _restauranteContext.Platos
                              where e.PlatoId == id
                              select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }
            else
            {
                _restauranteContext.Platos.Attach(plato);
                _restauranteContext.Platos.Remove(plato);
                _restauranteContext.SaveChanges();
                return Ok(plato);
            }
        }
        [HttpGet]
        [Route("FindByPrecio/{precio}")]
        public IActionResult FiltrarCliente(int precio)
        {
            List<Plato> plato = (from e in _restauranteContext.Platos
                              where e.Precio > precio
                              select e).ToList();
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);

        }
    }
}
