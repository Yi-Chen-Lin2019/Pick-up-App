using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebUI.ViewModels;
using System.Web.Script.Serialization;

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
            _restUrl = _baseUrl + "/";
        }

        public async Task<UserViewModel> GetPersonById(int id)
        {
            Person personFromService;

            // Create URI
            string useRestUrl = _restUrl + "Persons/"+id.ToString();
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    personFromService = JsonConvert.DeserializeObject<Person>(content);
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
            catch (Exception ex)
            {
                throw ex;
            }
            return new UserViewModel(personFromService);
        }

        public async Task PostOrder(OrderViewModel order)
        {
            // Create URI
            string useRestUrl = _restUrl + "Orders";
            var uri = new Uri(string.Format(useRestUrl));
            
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(order));
                var response = await _client.PostAsync(uri, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw (new HttpRequestValidationException(response.StatusCode.ToString()));
                }
                else
                {
                    throw (new Exception());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            catch (Exception ex)
            {
                throw ex;
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