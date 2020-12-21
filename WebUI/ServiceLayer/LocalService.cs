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
using System.Text;

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
            if (null != HttpContext.Current.Session["TokenInfo"])
            {
                Token token = HttpContext.Current.Session["TokenInfo"] as Token;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.AccessToken }");
            }
        }

        public async Task<bool> GetUserOrders()
        {
            // Create URI
            string useRestUrl = _restUrl + "Orders/UserId";
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UserViewModel.Current.Orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(content);
                    List<OrderViewModel> orders = UserViewModel.Current.Orders;
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
            return true;
        }

        public async Task<bool> Register(RegisterViewModel registeration)
        {
            bool registerOk;
            string useRestUrl = _restUrl + "api/Account/Register";
            var uri = new Uri(string.Format(useRestUrl));
            try
            {
                var json = JsonConvert.SerializeObject(registeration);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    registerOk = true;
                }
                else
                {
                    registerOk = false;
                    throw (new HttpRequestValidationException(response.StatusCode.ToString()));
                }
            }
            catch
            {
                registerOk = false;
            }

            return registerOk;
        }
        public async Task<Token> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            string useRestUrl = _restUrl + "Token";
            var uri = new Uri(string.Format(useRestUrl));

            using (HttpResponseMessage response = await _client.PostAsync(uri, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Token>();
                    HttpContext.Current.Session["TokenInfo"] = result;
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> PutUserInfo(Person person)
        {
                bool PutOk = false;
                string useRestUrl = _restUrl + "/Person/UpdateInfo";
                var uri = new Uri(string.Format(useRestUrl, string.Empty));

                try
                {
                    var json = JsonConvert.SerializeObject(person);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await _client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        PutOk = true;
                    }
                }
                catch
                {
                    PutOk = false;
                }

            return PutOk;
        }

        public async Task<UserViewModel> GetUserInfo()
        {
            Person personFromService;

            // Create URI
            string useRestUrl = _restUrl + "Person/Info";
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

        public async Task<HttpResponseMessage> PostOrder(OrderViewModel order)
        {
            HttpResponseMessage response;
            string useRestUrl = _restUrl + "Orders";
            var uri = new Uri(string.Format(useRestUrl));
            try
            {
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await _client.PostAsync(uri, content);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return response;
            /*
            bool PostedOk;
            string useRestUrl = _restUrl + "Orders";
            var uri = new Uri(string.Format(useRestUrl));
            try
            {
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    PostedOk = true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    PostedOk = false;
                    throw (new HttpRequestValidationException(response.StatusCode.ToString()));
                }
                else
                {
                    PostedOk = false;
                }
            }
            catch
            {
                PostedOk = false;
            }

            return PostedOk;
            */
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