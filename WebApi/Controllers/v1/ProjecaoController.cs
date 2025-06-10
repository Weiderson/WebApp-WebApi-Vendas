using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/projecao")]
    [ApiVersion("1.0")]
    public class ProjecaoController : ControllerBase
    {
        private readonly Context _db;
        private readonly ILogger<ProjecaoController> _logger;

        public ProjecaoController(Context db, ILogger<ProjecaoController> iLogger)
        {
            _db = db;
            _logger = iLogger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.Log(LogLevel.Information, "Buscando registros.");
                
                var _Projecoes = await _db.Projecoes.ToListAsync();

                if (!_Projecoes.Any())
                    return NotFound("Lista de Projecões nula ou vazia.");
                                
                return Ok(_Projecoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registros.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Buscando registro.");
                var _Projecao = await _db.Projecoes.FirstOrDefaultAsync(f => f.Id == id);

                if (_Projecao == null)
                    return BadRequest("Projeção não pode ser nulo."); 

                _logger.Log(LogLevel.Information, "Registro retornado.");

                return Ok(_Projecao); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(Projecao projecao)
        {
            try
            {
                _logger.Log(LogLevel.Error, "Adicionando registro.");
                               
                if (projecao == null)
                    return BadRequest("Projecão não pode ser nulo.");

                if (projecao.Valor <= 0)
                    return BadRequest("Projecão tem que ser maior que ZERO");

                if (projecao.Ano.Length != 4)
                    return BadRequest("Ano deve possuir quatro algarismos");
                

                var _Projecao = await _db.Projecoes.FirstOrDefaultAsync(f => f.Ano == projecao.Ano);
                if (_Projecao != null)
                    return BadRequest("Projeção já existente para o ano: " + projecao.Ano);

                projecao.Id = null;             

                await _db.Projecoes.AddAsync(projecao);
                await _db.SaveChangesAsync();

                _logger.Log(LogLevel.Error, "Registro adicionado.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }           

        
        [HttpPut]
        public async Task<IActionResult> Update(Projecao projecao)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Atualizando registro.");

                if (projecao == null)
                    return BadRequest("Projecao não pode ser nulo.");

                if (projecao.Ano.Length != 4)
                    return BadRequest("Ano deve possuir quatro algarismos");

                
                var _projecao = await _db.Projecoes.FindAsync(projecao.Id);
                if (_projecao == null)
                    return BadRequest("Projeção não encontrada: " + projecao.Ano);

                _projecao.Ano = projecao.Ano;
                _projecao.Valor = projecao.Valor;
                _projecao.DtAlteracao = DateTime.Now; 

                _db.Projecoes.Update(_projecao);
                _db.SaveChanges();                  
               
                _logger.Log(LogLevel.Information, "Registro atualizado.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                _logger.LogInformation("Excluindo registro.");

                var _Projecao = await _db.Projecoes.FindAsync(id);

                if (_Projecao == null)
                    return BadRequest("Projecao não pode ser nulo.");

                _Projecao.DtExclusao = DateTime.Now;
                _db.Projecoes.Update(_Projecao);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Registro excluído - logico.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

    }
}
