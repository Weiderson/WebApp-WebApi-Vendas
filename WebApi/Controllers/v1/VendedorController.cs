using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/vendedor")]
    [ApiVersion("1.0")]
    public class VendedorController : ControllerBase
    {
        private readonly Context _db;
        private readonly ILogger<VendedorController> _logger;

        public VendedorController(Context db, ILogger<VendedorController> iLogger)
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
                
                var _vendedores = await _db.Vendedores.Where(c => c.DtExclusao == null).ToListAsync();

                if (!_vendedores.Any())
                    return NotFound("Lista de vendedores nula ou vazia.");

                return Ok(_vendedores);
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
              
                var _vendedor = await _db.Vendedores.FirstOrDefaultAsync(f => f.Id == id);

                if (_vendedor == null)
                    return BadRequest("Vendedor inexistente.");

                _logger.Log(LogLevel.Information, "Registro retornado.");

                return Ok(_vendedor); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(Vendedor vendedor)
        {
            try
            {
                _logger.Log(LogLevel.Error, "Adicionando registro.");

                if (vendedor == null)
                    return BadRequest("Vendedor não pode ser nulo.");

                vendedor.Id = null;                

                await _db.Vendedores.AddAsync(vendedor);
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
        public async Task<IActionResult> Update(Vendedor vendedor)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Atualizando registro.");

                if (vendedor == null)
                    return BadRequest("Vendedor não pode ser nulo.");

                var _vendedor = await _db.Vendedores.FindAsync(vendedor.Id);
                if (_vendedor == null)
                    return BadRequest("Vendedor inexistente.");

                _vendedor.NomeCompleto = vendedor.NomeCompleto;
                _vendedor.DocumentoTipo = vendedor.DocumentoTipo;   
                _vendedor.DocumentoNumero = vendedor.DocumentoNumero;
                _vendedor.Telefone = vendedor.Telefone;
                _vendedor.Relacionamento = vendedor.Relacionamento;
                _vendedor.Situacao = vendedor.Situacao;
                _vendedor.Email = vendedor.Email;
                _vendedor.DtAlteracao = DateTime.Now;

                _db.Vendedores.Update(_vendedor);
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

                var _vendedor = await _db.Vendedores.FindAsync(id);

                if (_vendedor == null)
                    return BadRequest("Vendedor não pode ser nulo.");

                var _vendas = await _db.Vendas.Where(a => a.VendedorId == id).CountAsync();

                if (_vendas > 0)
                    return BadRequest("Vendedor possui vendas.");

                _vendedor.DtExclusao = DateTime.Now;
                _db.Vendedores.Update(_vendedor);
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
