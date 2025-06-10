using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class VendaVM
    {
        public long? Id { get; set; }

        [DisplayName("Vendedor")]
        public string VendedorNome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public Decimal Valor { get; set; }

        [DisplayName("Valor")]
        public string ValorCurrency { get; set; }

        [DisplayName("Data")]
        public DateTime DtInclusao { get; set; } = DateTime.Now;

        public DateTime? DtAlteracao { get; set; }

        public DateTime? DtExclusao { get; set; }

        public string DtInclusaoStr { get; set; }
    }
}