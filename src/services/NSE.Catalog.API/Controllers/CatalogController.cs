using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Identity;


namespace NSE.Catalogo.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAll();
        }
        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/product/{id}")]
        public async Task<Product> Detail(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}