using L01_2021AA602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2021AA602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public MotoristaController(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }
        // GET: api/<MotoristaController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Motorista> listadoMoto = (from e in _restauranteContext.Motoristas
                                         select e).ToList();

            if (listadoMoto.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoMoto);
        }

        // GET api/<MotoristaController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            Motorista? moto = (from e in _restauranteContext.Motoristas
                            where e.MotoristaId == id
                            select e).FirstOrDefault();
            if (moto == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(moto);
            }
        }

        // POST api/<MotoristaController>
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] Motorista motorista)
        {
            try
            {
                _restauranteContext.Motoristas.Add(motorista);
                _restauranteContext.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MotoristaController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Modificarplato(int id, [FromBody] Motorista motoModificado)
        {
            Motorista? motoActual = (from e in _restauranteContext.Motoristas
                                  where e.MotoristaId == id
                                  select e).FirstOrDefault();
            if (motoActual == null)
            {
                return NotFound();
            }
            else
            {
                motoActual.NombreMotorista = motoModificado.NombreMotorista;
            }
            _restauranteContext.Entry(motoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(motoActual);

        }

        // DELETE api/<MotoristaController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Motorista? moto = (from e in _restauranteContext.Motoristas
                            where e.MotoristaId == id
                            select e).FirstOrDefault();
            if (moto == null)
            {
                return NotFound();
            }
            else
            {
                _restauranteContext.Motoristas.Attach(moto);
                _restauranteContext.Motoristas.Remove(moto);
                _restauranteContext.SaveChanges();
                return Ok(moto);
            }
        }

        [HttpGet]
        [Route("FindByNombre/{nombre}")]
        public IActionResult FiltrarCliente(string nombre)
        {
            List<Motorista> motoristas = (from e in _restauranteContext.Motoristas
                                 where e.NombreMotorista == nombre
                                 select e).ToList();
            if (motoristas == null)
            {
                return NotFound();
            }
            return Ok(motoristas);

        }
    }
}
