using Model;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFNav
{
    /// <summary>
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : Page
    {
        static readonly HttpClient client = new HttpClient();

        public CreateProductPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        public async void LoadCategories()
        {
            var url = $"https://localhost:44386/Categories";
            var uri = new Uri(string.Format(url, string.Empty));
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            string responseBody = await client.GetStringAsync(uri);
            IEnumerable<Category> result = JsonConvert.DeserializeObject<IEnumerable<Category>>(responseBody);
            //CategoryComboList.ItemsSource = result;
            foreach (var category in result)
            {
                CategoryList.Items.Add(category);
                CategoryList.DisplayMemberPath = "CategoryName";
                CategoryList.SelectedValuePath = "CategoryId";
            }
            
        }

        public async void CreateProductButton_Click(object sender, EventArgs e)
        {

            //Category category = new Category
            //{
            //    CategoryId = CategoryList.SelectedIndex,
            //    CategoryName = CategoryList.SelectedItem
            //};
            await PostProduct();

            //var json = JsonConvert.SerializeObject(product);
            //var url = $"https://localhost:44386/Products";
            //var uri = new Uri(string.Format(url, string.Empty));
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            //var result = await client.PostAsync(uri, output, new JsonMediaTypeFormatter());
            //result.EnsureSuccessStatusCode();
            //string resultString = await result.Content.ReadAsAsync<string>();
            //MessageBox.Show(resultString);
            //string responseBody = await client.GetStringAsync(uri);
        }

        public async Task<bool> PostProduct()
        {
            Product product = new Product
            {

                ProductName = ProductNameBox.Text,
                Barcode = int.Parse(BarcodeBox.Text),
                ProductPrice = decimal.Parse(PriceBox.Text),
                StockQuantity = int.Parse(StockQuantityBox.Text),
                Category = (Category)CategoryList.SelectedItem
            };

            bool PostedOk;
            string url = $"https://localhost:44386/Products";
            var uri = new Uri(string.Format(url, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

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
