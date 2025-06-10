using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Model
{
    [Table("Projecoes")]
    public class Projecao
    {
        [Key]
        public long? Id { get; set; }       

        public string Ano { get; set; }

        public Int64 Valor { get; set; } = 0;

        [SwaggerIgnore]
        public DateTime? DtInclusao { get; set; } = DateTime.Now;

        [SwaggerIgnore]
        public DateTime? DtAlteracao { get; set; }

        [SwaggerIgnore]
        public DateTime? DtExclusao { get; set; }

      
    }
}
