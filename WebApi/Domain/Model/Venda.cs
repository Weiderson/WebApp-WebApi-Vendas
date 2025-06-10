using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Model
{

    [Table("Vendas")]
    public class Venda
    {
        [Key]
        public long? Id { get; set; }

        public long VendedorId { get; set; }

        public string Descricao { get; set; }
        public Decimal Valor { get; set; }      

        [SwaggerIgnore]
        public DateTime DtInclusao { get; set; } = DateTime.Now;

        [SwaggerIgnore]
        public DateTime? DtAlteracao { get; set; }

        [SwaggerIgnore]
        public DateTime? DtExclusao { get; set; }

       
    }
}
