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
using WPFNav.Service;

namespace WPFNav.StartingPoint.ManageNavigation
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        LocalService ls = new LocalService();
        public ProductPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        public async void LoadCategories()
        {
            List<Category> categoryList = await ls.GetAllCategories();
            foreach (var category in categoryList)
            {
                CategoryList.Items.Add(category);
                CategoryList.DisplayMemberPath = "CategoryName";
                CategoryList.SelectedValuePath = "CategoryId";
                CategoryUpdateList.Items.Add(category);
                CategoryUpdateList.DisplayMemberPath = "CategoryName";
                CategoryUpdateList.SelectedValuePath = "CategoryId";
            }
        }
        public async void ReadProductsButton_Click(object sender, RoutedEventArgs e)
        {
            //List<Product> productList;
            //LocalService ls = new LocalService();
            List<Product> productList = await ls.GetAllProducts();

            ProductList.ItemsSource = productList;   
        }

        
        public async void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(ProductIdUpdateBox.Text))
            {
                Product updatedProduct = await ls.GetProduct(int.Parse(ProductIdUpdateBox.Text));
                if(updatedProduct != null)
                {
                    updatedProduct.ProductId = int.Parse(ProductIdUpdateBox.Text);
                    updatedProduct.ProductName = ProductNameUpdateBox.Text;
                    updatedProduct.Barcode = int.Parse(BarcodeUpdateBox.Text);
                    updatedProduct.ProductPrice = decimal.Parse(PriceUpdateBox.Text);
                    updatedProduct.StockQuantity = int.Parse(StockQuantityUpdateBox.Text);
                    updatedProduct.Category = (Category)CategoryUpdateList.SelectedItem;
                };

                if (await ls.UpdateProduct(updatedProduct))
                {
                    MessageBox.Show("done");
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }


            }

        }


        public async void ReadProductById_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ProductIdBox.Text))
            {
                MessageBox.Show("type in id");
            }
            else
            {
                Product product = await ls.GetProduct(int.Parse(ProductIdBox.Text));
                ProductByIdBox.Text = product.ToString();
            }
        }

        public async void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {

            Product product = new Product
            {
                ProductName = ProductNameBox.Text,
                Barcode = int.Parse(BarcodeBox.Text),
                ProductPrice = decimal.Parse(PriceBox.Text),
                StockQuantity = int.Parse(StockQuantityBox.Text),
                Category = (Category)CategoryList.SelectedItem
            };

            bool success;

               if(await ls.PostProduct(product))
            {
                success = true;
                MessageBox.Show("done");
            } else
            {
                success = false;
                MessageBox.Show("Something went wrong, try using unique barcode");
            }


        }

    }
}
