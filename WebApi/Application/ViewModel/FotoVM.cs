using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace WebApi.Application.ViewModel
{

    public class FotoVM
    {
        [SwaggerIgnore]
        public long IdFoto { get; set; }

        public long VendedorId { get; set; }
        
        [SwaggerIgnore]
        public DateTime DtInclusao { get; set; }

        [SwaggerIgnore]
        public DateTime? DtExclusao { get; set; }

        public IFormFile? File { get; set; }       
    }
}
