using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Model
{
    [Table("Vendedores")]
    public class Vendedor
    {  
        [Key]
        public long? Id { get; set; }

        public string NomeCompleto { get; set; }

        public string DocumentoTipo { get; set; }

        public string DocumentoNumero { get; set; }

        public string Telefone { get; set; }

        public string Relacionamento { get; set; }       

        public string Situacao { get; set; }              

        public string Email { get; set; }

        [JsonIgnore]
        public DateTime DtInclusao { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime? DtAlteracao { get; set; }

        [JsonIgnore]
        public DateTime? DtExclusao { get; set; }


    }
}
