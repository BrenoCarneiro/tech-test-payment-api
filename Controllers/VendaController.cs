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
        /// <summary>
        /// Registra uma nova venda
        /// </summary>
        /// <remarks>
        /// Preencher somente os dados do vendedor e ao menos um produto, os demais campos (id, data e status) são de preenchimento automático.
        /// Exemplo:
        /// 
        ///     {
        ///       "id": 1,
        ///       "idVendedor": 1,
        ///       "nomeVendedor": "string",
        ///       "cpfVendedor": "Nome do Vendedor",
        ///       "emailVendedor": "exemplo@email.com",
        ///       "telefoneVendedor": "(71)  999999999"
        ///       "data": "2023-01-13T14:49:06.806Z",
        ///       "produto": "Exemplo de Produto",
        ///       "status": "AguardandoPagamento"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("Registrar")]
        public ActionResult Registrar([FromServices] DataContext context, Venda venda)
        {
            if (venda == null)
                return BadRequest();

            context.Vendas.Add(venda);
            context.SaveChanges();
            return CreatedAtAction(nameof(Buscar), new { id = venda.Id }, venda);
        }

        /// <summary>
        /// Busca as informações da venda através do id
        /// </summary>
        /// <remarks>
        /// Informe abaixo o id de uma venda:   
        /// </remarks>
        [HttpGet("Buscar/{id}")]
        public IActionResult Buscar([FromServices] DataContext context, int id)
        {
            var venda = context.Vendas.Find(id);
            if (venda == null)
            {
                return NotFound(new { Erro = "Id não encontrado" });
            }
            else
            {
                return Ok(venda);
            }
        }

        /// <summary>
        /// Atualiza o status da venda através do id
        /// </summary>
        /// <remarks>
        /// Informe um número de id válido e selecione uma atualização de status respeitando a ordem abaixo:
        /// 
        /// De: Aguardando Pagamento Para: Pagamento Aprovado
        ///
        /// De: Aguardando Pagamento Para: Cancelada
        ///
        /// De: Pagamento Aprovado Para: Enviado para Transportadora
        ///
        /// De: Pagamento Aprovado Para: Cancelada
        ///
        /// De: Enviado para Transportador.Para: Entregue
        /// 
        /// Observação: Quando o status se encontra como "Enviado para Transportadora", não será mais possível alterar para "Cancelada". 
        /// </remarks>
        [HttpPut("AtualizarStatus/{id}")]
        public IActionResult AtualizarStatus([FromServices] DataContext context, Opcao status, int id)
        {

            var vendaDataBase = context.Vendas.Find(id);

            if (vendaDataBase == null)          
                return NotFound(new { Erro = "Id não encontrado" });
 
            if (status == Opcao.Cancelada && vendaDataBase.Status == Opcao.PagamentoAprovado || vendaDataBase.Status == Opcao.AguardandoPagamento)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase) ;
            }
            else if (vendaDataBase.Status == Opcao.AguardandoPagamento && status == Opcao.PagamentoAprovado)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else if (vendaDataBase.Status == Opcao.PagamentoAprovado && status == Opcao.EnviadoParaTransportadora)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else if (vendaDataBase.Status == Opcao.EnviadoParaTransportadora && status == Opcao.Entregue)
            {
                vendaDataBase.Status = status;
                context.Vendas.Update(vendaDataBase);
                context.SaveChanges();
                return Ok(vendaDataBase);
            }
            else
            {
                return BadRequest(new { Erro = "Operação não permitida" });
            }
        }
    }
}
