﻿using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFNav.Models;

namespace WPFNav.Service
{
    class LocalService
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
                    return result;
                }
                else
                {
                    throw (new Exception(response.ReasonPhrase));
                }
            }
        }

        public async Task<Order> GetOrder(int orderId)
        {
            Order orderFromService;

            // Create URI
            string useRestUrl = _restUrl + "Orders/" + orderId.ToString();
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    orderFromService = JsonConvert.DeserializeObject<Order>(content);
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
            return orderFromService;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            bool PutOk = false;
            string url = $"https://localhost:44386/Orders/" + order.OrderId.ToString();
            var uri = new Uri(string.Format(url, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(order);
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

        public async Task<List<Order>> GetAllOrders()
        {
            Token token = Application.Current.Resources["TokenInfo"] as Token;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.AccessToken }");
            List<Order> ordersFromService;

            // Create URI
            string useRestUrl = _restUrl + "Orders";
            var uri = new Uri(string.Format(useRestUrl));
            //
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ordersFromService = JsonConvert.DeserializeObject<List<Order>>(content);
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
            return ordersFromService;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> productsFromService;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
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
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            // Create URI
            string useRestUrl = _restUrl + "Categories";
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

        public async Task<bool> PostProduct(Product product)
        {
            bool PostedOk;
            string useRestUrl = _restUrl + "Products";
            var uri = new Uri(string.Format(useRestUrl));
            try
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    PostedOk = true;
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
        }

    }
}
