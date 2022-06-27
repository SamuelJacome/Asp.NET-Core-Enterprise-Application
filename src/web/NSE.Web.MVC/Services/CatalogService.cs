using Microsoft.Extensions.Options;
using NSE.Web.MVC.Extensions;
using NSE.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            httpClient.BaseAddress = new Uri(_settings.CatalogUrl);
        }
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("catalog/products");
            HandleErrorsReponse(response);

            return await DeserializerObjectResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"catalog/product/{id}");
            HandleErrorsReponse(response);
            return await DeserializerObjectResponse<ProductViewModel>(response);
        }

      
    }
}
