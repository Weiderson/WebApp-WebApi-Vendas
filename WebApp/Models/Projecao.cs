using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain.Model
{
    [Table("Projecoes")]
    public class Projecao
    {
        [Key]
        public long? Id { get; set; }

        public string Ano { get; set; }

        public Int64 Valor { get; set; } = 0;

        public DateTime? DtInclusao { get; set; }

        public DateTime? DtAlteracao { get; set; }

        public DateTime? DtExclusao { get; set; }
    }
}