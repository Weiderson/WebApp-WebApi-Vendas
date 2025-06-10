namespace WebApp.Models
{
    public class ErroAPIViewModel
    {
        public bool Erro { get; set; } = false;

        public string? Mensagem { get; set; }

        public string? Url { get; set; }

        public string? Site { get; set; }

        public string? StatusCodeString { get; set; }

        public int? StatusCode { get; set; }

        public string? MensagemErro { get; set; }
    }
}