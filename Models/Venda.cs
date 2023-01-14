using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


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
        public string Produto { get; set; }
        public Op Status { get; set; }

        public enum Op
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
