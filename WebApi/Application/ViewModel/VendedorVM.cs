using System.ComponentModel;

namespace WebApi.Application.ViewModel
{    
    public class VendedorVM
    {
        public long Id { get; set; }

        public string NomeCompleto { get; set; }

        public string DocumentoTipo { get; set; }

        public string DocumentoNumero { get; set; }

        public string Telefone { get; set; }

        public string Relacionamento { get; set; }

        public DateTime? DtInclusao { get; set; }

        public DateTime? DtExclusao { get; set; }

        public string Situacao { get; set; }

        public Int64? VlVendaAnual { get; set; }

        public string Email { get; set; }
    }
}
