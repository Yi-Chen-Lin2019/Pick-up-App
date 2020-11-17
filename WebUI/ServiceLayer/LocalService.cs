using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebUI.ServiceLayer
{
    public class LocalService
    {

        readonly HttpClient _client;
        readonly string _ipDomain = "https://localhost";
        readonly string _baseUrl;
        readonly string _restUrl;

       // public LocalService(int usePort)
        public LocalService()
        {
            _client = new HttpClient();
            _baseUrl = _ipDomain + ":" + 44386;
            _restUrl = _baseUrl + "/api/";
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> productsFromService;

            // Create URI
            string useRestUrl = _restUrl + "Products";
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    productsFromService = JsonConvert.DeserializeObject<List<Product>>(content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw (new HttpRequestValidationException(response.StatusCode.ToString()));
                }
                else
                {
                    throw (new Exception());
                }
            }
            catch
            {
                throw;
            }
            return productsFromService;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> catsFromService;

            // Create URI
            string useRestUrl = _restUrl + "Category";
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    catsFromService = JsonConvert.DeserializeObject<List<Category>>(content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw (new HttpRequestValidationException(response.StatusCode.ToString()));
                }
                else
                {
                    throw (new Exception());
                }
            }
            catch
            {
                throw;
            }
            return catsFromService;
        }

    }
}