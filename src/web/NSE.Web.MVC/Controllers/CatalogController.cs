using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Web.MVC.Services;

namespace NSE.Web.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogService.GetAll();

            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var produto = await _catalogService.GetById(id);

            return View(produto);
        }
    }
}