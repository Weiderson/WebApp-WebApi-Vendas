using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/vendedor")]
    [ApiVersion("2.0")]
    [SwaggerResponse(200, "OK", typeof(Vendedor))]
    public class VendedorController : ControllerBase
    {
        private readonly Context _db;
        private readonly ILogger<VendedorController> _logger;

        public VendedorController(Context db, ILogger<VendedorController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.Log(LogLevel.Information, "Buscando registros.");

                var _clientes = await _db.Vendedores.Where(c => c.DtExclusao == null).ToListAsync();

                if (!_clientes.Any())
                    return NotFound("Lista de vendedores nula ou vazia.");

                return Ok(_clientes);
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
                var _cliente = await _db.Vendedores.FirstOrDefaultAsync(f => f.Id == id);

                if (_cliente == null)
                    return BadRequest("Vendedor não pode ser nulo.");

                _logger.Log(LogLevel.Information, "Registro retornado.");

                return Ok(_cliente);
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
                _logger.LogInformation("Excluindo registro - físico.");

                var _cliente = await _db.Vendedores.FindAsync(id);

                if (_cliente == null)
                    return BadRequest("Vendedor não pode ser nulo.");

                var _vendas = await _db.Vendas.Where(a => a.VendedorId == id).CountAsync();

                if (_vendas > 0)
                    return BadRequest("Vendedor possui vendas.");

                _db.Vendedores.Entry(_cliente).State = EntityState.Deleted;
                await _db.SaveChangesAsync();

                    _logger.LogInformation("Registro excluído - físico.");
               
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
