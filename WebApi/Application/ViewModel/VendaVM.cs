using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Application.ViewModel
{

    public class VendaVM
    {
        public long? Id { get; set; }        

        public string Descricao { get; set; }

        public string VendedorNome { get; set; }
             
        public Decimal Valor { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime? DtAlteracao { get; set; }

        public DateTime? DtExclusao { get; set; }

    }
}
