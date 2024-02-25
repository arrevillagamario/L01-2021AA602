using L01_2021AA602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2021AA602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public PedidoController(RestauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        // GET: api/<PedidoController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Pedido> listadoPedidos = (from e in _restauranteContext.Pedidos
                                           select e).ToList();

            if (listadoPedidos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedidos);
        }
    

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            Pedido? pedido = (from e in _restauranteContext.Pedidos
                              where e.PedidoId == id
                              select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(pedido);
            }
        }

        // POST api/<PedidoController>
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] Pedido pedido)
        {
            try
            {
                _restauranteContext.Pedidos.Add(pedido);
                _restauranteContext.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PedidoController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Pedido pedidoModificar)
        {
            Pedido? pedidoActual = (from e in _restauranteContext.Pedidos
                              where e.PedidoId == id
                              select e).FirstOrDefault();

            if(pedidoActual== null)
            {
                return NotFound();
            }
            else
            {
                pedidoActual.MotoristaId = pedidoModificar.MotoristaId;
                pedidoActual.ClienteId = pedidoModificar.ClienteId;
                pedidoActual.PlatoId = pedidoModificar.PlatoId;
                pedidoActual.Cantidad = pedidoModificar.Cantidad;
                pedidoActual.Precio = pedidoModificar.Precio;
                _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
                _restauranteContext.SaveChanges();
                return Ok(pedidoActual);
            }
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Pedido? pedido = (from e in _restauranteContext.Pedidos
                              where e.PedidoId == id
                              select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            else
            {
                _restauranteContext.Pedidos.Attach(pedido);
                _restauranteContext.Pedidos.Remove(pedido);
                _restauranteContext.SaveChanges();
                return Ok(pedido);
            }
        }


        [HttpGet]
        [Route("FindByCliente/{cliente}")]
        public IActionResult FiltrarCliente(int cliente)
        {
            Pedido? pedido = (from e in _restauranteContext.Pedidos
                                    where e.ClienteId == cliente
                                    select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);

        }

        [HttpGet]
        [Route("FindByMotorista/{motorista}")]
        public IActionResult FiltrarMotorista(int motorista)
        {
            Pedido? pedido = (from e in _restauranteContext.Pedidos
                              where e.MotoristaId == motorista
                              select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);

        }


    }
}
