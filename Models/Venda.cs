using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApi.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public int IdVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public int CpfVendedor { get; set; }
        public string EmailVendedor { get; set; }
        public int TelefoneVendedor { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public string Produto { get; set; }
        public Opcao Status { get; set; }

        public enum Opcao
        {
            [Display(Name = "Aguardando Pagamento")]
            AguardandoPagamento,
            [Description("Cancelada")]
            Cancelada,
            [Description("Pagamento Aprovado")]
            PagamentoAprovado,
            [Description("Enviado Para Transportadora")]
            EnviadoParaTransportadora,
            [Description("Entregue")]
            Entregue
        }
    }
}
