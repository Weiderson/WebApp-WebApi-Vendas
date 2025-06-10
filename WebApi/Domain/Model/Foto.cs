using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Model
{
    [Table("Fotos")]
    public class Foto
    {
        [Key]
        public long? Id { get; set; }

        public long VendedorId { get; set; }

        public string? NomeArquivo { get; set; }

        public string TipoArquivo { get; set; }

        public byte[] ConteudoArquivo { get; set; }

        [SwaggerIgnore]
        public DateTime DtInclusao { get; set; } = DateTime.Now;

        [SwaggerIgnore]
        public DateTime? DtExclusao { get; set; }       

    }
}
