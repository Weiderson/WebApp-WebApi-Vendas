using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApp.Models;
using WebApp.ViewModels;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace WebApp.Controllers
{
    //[Authorize]
    public class VendedorAppController : Controller
    {
        #region SESSÃO DESTINADA A VARIÁVEIS

        private readonly Data.AppContext _db;

        public VendedorAppController(Data.AppContext db)
        {
            _db = db;
            SetarVar();
        }

        public string Site { get; set; }

        public Uri Uri { get; set; }

        public double Totalvendaanual { get; set; }
        public double? ValorTotalSum { get; private set; }

        public void SetarVar()
        {
            Site = "site3";

            string? local = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (local == "Producao")
            {
                Uri = new Uri("https://localhost:8081/");
            }
            else
            {
                Uri = new Uri("https://localhost:7141/");
            }
        }

        #endregion SESSÃO DESTINADA A VARIÁVEIS

        #region SESSÃO DESTINADA AOS MÉTODOS DOS CONTROLADORES
        public async Task<IActionResult> Index()
        {
            int qtdvendedores = _db.Vendedores.Count();
            var ano = DateTime.Now.Year.ToString();
            var projecao = _db.Projecoes.FirstOrDefault(p => p.Ano == ano)?.Valor.ToString();
            Int64 projecaoc = Convert.ToInt64(projecao);
            var ListaVendedores = (from v in _db.Vendedores
                                   join ve in _db.Vendas on v.Id equals ve.VendedorId
                                   into agrupado
                                   let arg = agrupado.Where(a => a.DtExclusao == null).Sum(ve => ve.Valor)
                                   let dt = agrupado.Select(ve => ve.DtInclusao).First()
                                   select new VendedorVM
                                   {
                                       Id = v.Id,
                                       NomeCompleto = v.NomeCompleto,
                                       Email = v.Email,
                                       Telefone = v.Telefone,
                                       Relacionamento = v.Relacionamento,
                                       DtInclusao = v.DtInclusao,
                                       DtExclusao = v.DtExclusao,
                                       DtExclusaoV = dt,
                                       Situacao = v.Situacao,
                                       ValorTotal = arg,
                                       Perc = (arg / (projecaoc / qtdvendedores)).ToString("0.00%")
                                   }).AsEnumerable()
                                     .Select(v =>
                                     {
                                         v.ValorTotalCurrency = string.Format(new CultureInfo("pt-BR", false), "{0:c0}", v.ValorTotal);
                                         return v;
                                     })
                                     .Where(w => w.DtExclusao == null)
                                     .OrderByDescending(o => o.ValorTotalCurrency).ThenByDescending(o => o.NomeCompleto);

            var totalvendaanual = ListaVendedores.Sum(v => v.ValorTotal);

            ViewData["totalvenda"] = string.Format(new CultureInfo("pt-BR", false), "{0:c0}", totalvendaanual);
            ViewData["projecao"] = Convert.ToInt32(projecao);
            ViewData["qtdvendedores"] = qtdvendedores;

            return View(ListaVendedores);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return NotFound();

            Vendedor? vendedor = await _db.Vendedores.FindAsync(id);
            if (vendedor == null)
                return NotFound();

            Foto? foto = await _db.Fotos
                .FirstOrDefaultAsync(f => f.VendedorId == id, CancellationToken.None);

            if (foto != null)
            {
                ViewBag.Base64String = "data:image/png;base64," +
                    Convert.ToBase64String(foto.ConteudoArquivo, 0, foto.ConteudoArquivo.Length);
            }
            return PartialView(@"~/Views/VendedorApp/Details.cshtml", vendedor);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var vendedor = await _db.Vendedores.FindAsync(id);
            if (vendedor == null)
                return NotFound();

            vendedor.DtExclusao = DateTime.Now;

            _db.Entry(vendedor).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "VendedorApp");
        }
    }

    #endregion SESSÃO DESTINADA AOS MÉTODOS DOS CONTROLADORES
}