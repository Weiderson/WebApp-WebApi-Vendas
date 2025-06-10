using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApi.Application.ViewModel;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/venda")]
    [ApiVersion("1.0")]
    public class VendaController : ControllerBase
    {
        private readonly Context _db;
        private readonly ILogger<VendaController> _logger;

        public VendaController(Context db, ILogger<VendaController> iLogger)
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

                var _vendas = (from v in _db.Vendas
                               join ve in _db.Vendedores on v.VendedorId equals ve.Id where v.DtExclusao == null && ve.DtExclusao == null

                               select new VendaVM
                               {
                                   Id = v.Id,
                                   Descricao = v.Descricao,
                                   Valor = v.Valor,
                                   VendedorNome = ve.NomeCompleto,
                                   DtInclusao = v.DtInclusao,
                                   DtAlteracao = v.DtAlteracao,
                                   DtExclusao = v.DtExclusao,

                               }).ToList();

                if (_vendas.Count <= 0)
                    return NotFound("Lista de Vendas nula ou vazia.");
                                
                return Ok(_vendas);
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
                var _venda = await _db.Vendas.FirstOrDefaultAsync(f => f.Id == id);

                if (_venda == null)
                    return BadRequest("Venda não pode ser nulo."); 

                _logger.Log(LogLevel.Information, "Registro retornado.");

                return Ok(_venda); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar registro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(Venda venda)
        {
            try
            {
                _logger.Log(LogLevel.Error, "Adicionando registro.");                               

                if (venda == null)
                    return BadRequest("Venda não pode ser nulo.");

                var vendedor = _db.Vendedores.FirstOrDefault(v => v.Id == venda.VendedorId);
                if (vendedor == null)
                    return BadRequest("Vendedor inexistente");

                if (venda.Valor <= 0)
                    return BadRequest("Venda tem que ser maior que ZERO");

                venda.Id = null;                       

                await _db.Vendas.AddAsync(venda);
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
        public async Task<IActionResult> Update(Venda venda)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Atualizando registro.");

                if (venda == null)
                    return BadRequest("Venda não pode ser nulo.");

                var _venda = await _db.Vendas.FindAsync(venda.Id);
                if (_venda == null)
                    return BadRequest("Venda não encontrada.");

                var _vendedor = await _db.Vendedores.FindAsync(venda.VendedorId);
                if (_vendedor == null)
                    return BadRequest("Vendedor ref. a venda não encontrado."); 

                _venda.Valor = venda.Valor; // Atualizando o valor da venda
                _venda.Descricao = venda.Descricao; // Atualizando a descrição da venda
                _venda.DtAlteracao = DateTime.Now; // Atualizando a data de alteração

                _db.Vendas.Update(_venda);
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

                var _venda = await _db.Vendas.FindAsync(id);

                if (_venda == null)
                    return BadRequest("Venda inexistente.");

                _venda.DtExclusao = DateTime.Now;

                _db.Vendas.Update(_venda);
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
