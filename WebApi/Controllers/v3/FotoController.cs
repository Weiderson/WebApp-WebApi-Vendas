using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Application.ViewModel;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Controllers.v3
{
    [ApiController]
    [Route("api/v{version:apiVersion}/foto")]
    [ApiVersion("3.0")]
    [SwaggerResponse(200, "OK", typeof(FotoVM))]
    public class FotoController : ControllerBase
    {
        private readonly Context _db;
        private readonly ILogger _logger;

        public FotoController(Context db, ILogger<FotoController> logger)
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

                var _fotos = await _db.Fotos.ToListAsync();

                if (!_fotos.Any())
                    return NotFound("Lista de fotos nula ou vazia.");

                _logger.Log(LogLevel.Information, "Registros retornados.");

                return Ok(_fotos);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro na requisição: " + ex.Message);
                return NotFound();
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Buscando registros.");

                var _foto = await _db.Fotos.FirstOrDefaultAsync(f => f.VendedorId == id);

                if (_foto == null)
                    return NotFound("Lista de fotos nula ou vazia.");

                _logger.Log(LogLevel.Information, "Registros retornados.");

                return Ok(_foto);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro na requisição: " + ex.Message);
                return NotFound();
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Excluindo registro - físico.");

                var _foto = await _db.Fotos.FindAsync(id);

                if (_foto == null)
                    return NotFound("Foto inexistente.");

                //_db.Fotos.RemoveRange(_db.Fotos.Where(f => f.VendedorId == _foto.VendedorId));

                _db.Fotos.Remove(_foto);
                await _db.SaveChangesAsync();

                _logger.Log(LogLevel.Information, "Registro excluído - físico.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro na requisição: " + ex.Message);
                return NotFound();
            }
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] FotoVM foto)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Adicionando registro.");

                var _foto = await _db.Fotos.FirstOrDefaultAsync(e => e.VendedorId == foto.VendedorId);

                if (_foto != null)
                    return NotFound("Já existe uma foto na base de dados para o vendedor informado.");             

                if (foto.VendedorId <= 0)
                    return BadRequest("VendedorId inválido.");

                var vendedor = await _db.Vendedores.FindAsync(foto.VendedorId);
                if (vendedor == null || vendedor.DtExclusao != null)
                    return NotFound("Vendedor inexistente.");

                byte[] filedata = Array.Empty<byte>();

                if (foto.File.Length > 0)
                {
                    using (MemoryStream str = new MemoryStream())
                    {
                        await foto.File.CopyToAsync(str);
                        filedata = str.ToArray();
                    }
                }

                var novafoto = new Foto
                {
                    VendedorId = foto.VendedorId,
                    ConteudoArquivo = filedata,
                    NomeArquivo = foto.File.FileName,
                    TipoArquivo = foto.File.ContentType,
                };

                _db.Fotos.Add(novafoto);
                await _db.SaveChangesAsync();

                _logger.Log(LogLevel.Information, "Registro adicionado.");

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro na requisição: " + ex.Message);
                return NotFound();
            }
        }
    }
}


