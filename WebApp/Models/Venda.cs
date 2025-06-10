using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Model
{
    [Table("Vendas")]
    public class Venda
    {
        [Key]
        public long? Id { get; set; }

        public string Descricao { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime? DtAlteracao { get; set; }

        public DateTime? DtExclusao { get; set; }

        public Decimal Valor { get; set; }

        public long VendedorId { get; set; } = 0;
    }
}