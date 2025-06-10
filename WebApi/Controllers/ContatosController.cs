using ContatosWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContatosWebApi.Controllers
{
    [ApiController]
    [Route("api/contato")]
    public class ContatosController : ControllerBase
    {
        //ContatosContext db = new ContatosContext();
        private readonly ContatosContext _db;
        private readonly ILogger<ContatosController> _logger;
        //private readonly ContatosContext db = new ContatosContext();


        public ContatosController(ContatosContext db, ILogger<ContatosController> logger)
        {
            _db = db;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpPost]

        []
        public IActionResult Add([FromForm] Contato contatos)
        {

            var contato = new Contato(contatos.IdContato, contatos.NomeCompleto, contatos.DocumentoTipo, contatos.DocumentoNumero, contatos.Telefone1, contatos.Relacionamento, contatos.DtInclusao, contatos.DtExclusao, contatos.Situacao);

            _db.Contatos.Add(contato);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.Log(LogLevel.Error, "Erro");

            var contatos = _db.Contatos.OrderBy(o => o.NomeCompleto).ToList();

            //var contatos = db.Contatos.Where(d => d.DtExclusao == null).OrderBy(o => o.NomeCompleto).ToList();
            //var contatos = _db.Contatos.Where(i => i.Situacao == "Relular");

            _logger.LogInformation("Retornou a lista de contatos");

            return Ok(contatos);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            _logger.Log(LogLevel.Error, "Teve um Erro");

            var contato = _db.Contatos.FirstOrDefault(f => f.IdContato == id);
            //var contatos = _db.Contatos.Where(i => i.Situacao == "Relular");

            _logger.LogInformation("Retornou os dados do contato");

            return Ok(contato);
        }


    }
}
