using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class VendedorVM
    {
        public long? Id { get; set; }

        [DisplayName("Vendedor")]
        public string NomeCompleto { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        public string DocumentoTipo { get; set; }

        public string DocumentoNumero { get; set; }

        public string Telefone { get; set; }

        public string Relacionamento { get; set; }

        public DateTime? DtInclusao { get; set; }

        public DateTime? DtExclusao { get; set; }

        [DisplayName("Situação")]
        public string Situacao { get; set; }

        public bool FotoCadastrada { get; set; }

        [DisplayName("Percentual/Valor de Vendas Anual")]
        public Decimal ValorTotal { get; set; }

        public string ValorTotalCurrency { get; set; }

        public string Perc { get; set; }

        public DateTime? DtExclusaoV { get; set; }

    }
}