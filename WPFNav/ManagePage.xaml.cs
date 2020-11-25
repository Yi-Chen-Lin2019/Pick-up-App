using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        static readonly HttpClient client = new HttpClient();
        public ManagePage()
        {
            InitializeComponent();
        }

        public void HiButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Hello");
        }

        public async void ReadApiButton_Click(object sender, RoutedEventArgs e)
        {
            string sResults = "API Results on /Products: ";
            var uri = $"https://localhost:44386/Products";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            string responseBody = await client.GetStringAsync(uri);
            IEnumerable<Product> result = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseBody);

            foreach (var product in result)
            {
                sResults += Environment.NewLine + "Id= " + product.ProductId + " Name= " + product.ProductName + " Barcode= " + product.Barcode + " Price= " + product.ProductPrice + " Quantity= " + product.StockQuantity + " CategoryId = " + product.Category.CategoryId + " Category name= " + product.Category.CategoryName;
                TxtBoxReadApi.Text = sResults;
            }

        }


    }
}
