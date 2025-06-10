using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public partial class Vendedor
    {
        [Key, ForeignKey(nameof(Fotos))]
        [DisplayName("Identificador")]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Vendedor")]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tipo do documento")]
        public string DocumentoTipo { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Número do documento")]
        public string DocumentoNumero { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [Required]
        [StringLength(50)]
        public string Relacionamento { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayName("Data de inclusão")]
        public DateTime DtInclusao { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayName("Data de exclusão")]
        public DateTime? DtExclusao { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Situação")]
        public string Situacao { get; set; }

        [StringLength(100)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        public virtual Foto Fotos { get; set; }

        [NotMapped]
        public bool FotoCadastrada { get; set; }

        [NotMapped]
        public Byte[]? FotoConteudo { get; set; }

        [NotMapped]
        [DisplayName("Total Venda")]
        public string ValorTotal { get; set; }

        [NotMapped]
        public Decimal ValorTotalSum { get; set; }
    }
}