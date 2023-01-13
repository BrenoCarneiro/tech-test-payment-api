using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }
        public int IdVendedor { get; set;}
        public string NomeVendedor { get; set;}
        [MaxLength(11)]
        public int CpfVendedor { get; set;}
        public string EmailVendedor { get; set; }
        [MaxLength(11)]
        public int TelefoneVendedor { get; set; }
        [Required]
        public string Produto { get; set; }
        public enum Status
        {
            [Description("Aguardando Pagamento")]
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
