namespace WebApp.Models
{
    internal sealed class ListaVendedores
    {
        public long Id { get; set; }

        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Relacionamento { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime? DtExclusao { get; set; }

        public string Situacao { get; set; }

        public decimal ValorTotalSum { get; set; }

        public string ValorTotal { get; set; }
    }
}