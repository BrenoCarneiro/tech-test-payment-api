using Microsoft.AspNetCore.Mvc;
using PaymentApi.Data;
using PaymentApi.Models;
using static PaymentApi.Models.Venda;

namespace PaymentApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        [HttpPost("Registrar")]
        public ActionResult Registrar([FromServices] DataContext context, Venda venda)
        {
            if (venda == null)
                return BadRequest();

            context.Vendas.Add(venda);
            context.SaveChanges();
            return CreatedAtAction(nameof(Buscar), new { id = venda.Id }, venda);
        }

        [HttpGet("Buscar/{id}")]
        public IActionResult Buscar([FromServices] DataContext context, int id)
        {
            var venda = context.Vendas.Find(id);
            if (venda == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(venda);
            }
        }

        [HttpPut("AtualizarStatus/{id}")]
        public IActionResult AtualizarStatus([FromServices] DataContext context, Op status, int id)
        {

            var vendaDataBase = context.Vendas.Find(id);

            if (vendaDataBase == null)          
                return NotFound();
 
            if (status == Op.Cancelada && vendaDataBase.Status == Op.PagamentoAprovado || vendaDataBase.Status == Op.AguardandoPagamento)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else if (vendaDataBase.Status == Op.AguardandoPagamento && status == Op.PagamentoAprovado)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else if (vendaDataBase.Status == Op.PagamentoAprovado && status == Op.EnviadoParaTransportadora)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else if (vendaDataBase.Status == Op.EnviadoParaTransportadora && status == Op.Entregue)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else
            {
                return BadRequest();
            }
            

        }

    }
}
