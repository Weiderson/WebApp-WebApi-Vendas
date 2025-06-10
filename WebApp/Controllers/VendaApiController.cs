using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VendaAPIController : Controller
    {
        #region SESSÃO DESTINADA A VARIÁVEIS

        private readonly Data.AppContext _db;

        public VendaAPIController(Data.AppContext db)
        {
            _db = db;
            SetarVar();
        }

        public string Site { get; set; }

        public Uri Uri { get; set; }

        public JsonSerializerSettings? Jsonserializersettings { get; set; }

        public void SetarVar()
        {
            Site = "site3";

            string? local = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (local != null)
            {
                if (local == "Producao")
                    Uri = new Uri("https://localhost:8081/");
                else
                    Uri = new Uri("https://localhost:7141/");
            }

            //https://stackoverflow.com/questions/31813055/how-to-handle-null-empty-values-in-jsonconvert-deserializeobject
            Jsonserializersettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        #endregion VARIÁVEIS - SESSÃO DESTINADA A VARIÁVEIS GLOBAIS

        #region API - SESSÃO DESTINADA AOS MÉTODOS DE ACESSO A APIS

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<VendaVM>? venda = new List<VendaVM>();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = Uri;
                    HttpResponseMessage response = await client.GetAsync("api/v1/venda").ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        venda = JsonConvert.DeserializeObject<IEnumerable<VendaVM>>(result, Jsonserializersettings);
                    }
                }

                var listavenda = venda?.Select(
                    v =>
                    {
                        v.ValorCurrency = string.Format(new CultureInfo("pt-BR", false), "{0:c0}", v.Valor);
                        v.DtInclusaoStr = v.DtInclusao.ToString("dd/MM/yyyy");
                        return v;
                    })
                    .OrderByDescending(o => o.Valor)
                    .ThenBy(o => o.VendedorNome);

                return View(listavenda);
            }
            catch (Exception ex)
            {
                return View(
                    "ErroAPI",
                    new ErroAPIViewModel
                    {
                        Erro = true,
                        Mensagem = ex.Message,
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Url = Uri.ToString(),
                        Site = "API Web"
                    });
            }
        }

        #endregion API - SESSÃO DESTINADA AOS MÉTODOS DE ACESSO A APIS
    }
}