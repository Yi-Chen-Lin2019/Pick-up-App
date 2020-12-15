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
        Product product;
        public ProductPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void ClearTextBoxes()
        {
            foreach (UIElement ctl in ProductGrid.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = string.Empty;
            }
        }

        public async void LoadCategories()
        {
            try
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
            catch 
            {
                MessageBox.Show("Please start API project, otherwise application can not get categories");
            }
            
        }
        public async void ReadProductsButton_Click(object sender, RoutedEventArgs e)
        {
            //List<Product> productList;
            //LocalService ls = new LocalService();
            ProductList.Items.Clear();
            List<Product> productList = await ls.GetAllProducts();
            foreach (var item in productList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = item.ToString();
                listViewItem.Uid = item.ProductId.ToString();
                ProductList.Items.Add(listViewItem);
            }
            //ProductList.ItemsSource = productList;   
        }

        
        public async void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {

            LocalService service = new LocalService();
            product.ImageUrl = ProductImageUpdateBox.Text;
            product.ProductId = int.Parse(ProductIdUpdateBox.Text);
            product.ProductName = ProductNameUpdateBox.Text;
            product.Barcode = int.Parse(BarcodeUpdateBox.Text);
            product.ProductPrice = decimal.Parse(PriceUpdateBox.Text);
            product.StockQuantity = int.Parse(StockQuantityUpdateBox.Text);
            if (CategoryUpdateList.SelectedItem == null)
            {
                MessageBox.Show("Choose category");
            }
            else
            {
                product.Category = (Category)CategoryUpdateList.SelectedItem;
                if (await service.UpdateProduct(this.product))
                {
                    MessageBox.Show("Updated successfully");
                }
                else
                {
                    MessageBox.Show("Failed to update. Please try again");
                }
                ReadProductsButton_Click(null, null);
            }
        }


        public async void ReadProductById_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ProductIdBox.Text))
            {
                MessageBox.Show("Type in Id.");
            }
            else
            {
                Product product = await ls.GetProduct(int.Parse(ProductIdBox.Text));
                product = await loadProduct(int.Parse(ProductIdBox.Text));
                ProductByIdBox.Text = product.ToString();
            }
        }

        public async void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {

            Product product = new Product
            {
                ImageUrl = ProductImageBox.Text,
                ProductName = ProductNameBox.Text,
                Barcode = int.Parse(BarcodeBox.Text),
                ProductPrice = decimal.Parse(PriceBox.Text),
                StockQuantity = int.Parse(StockQuantityBox.Text),
                Category = (Category)CategoryList.SelectedItem
            };

               if(await ls.PostProduct(product))
                {
                MessageBox.Show("Product created.");
                ClearTextBoxes();
                } else
                {
                MessageBox.Show("Something went wrong.");
                }

        }

        private async void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView v = (ListView)sender;
            if (v.HasItems)
            {
                ListViewItem selectedItem = (ListViewItem)e.AddedItems[0];
               product = await loadProduct(int.Parse(selectedItem.Uid));
            }
        }

        private async Task<Product> loadProduct(int productId)
        {
            LocalService service = new LocalService();
            Product productById = await service.GetProduct(productId);
            ProductIdUpdateBox.Text = productById.ProductId.ToString();
            ProductNameUpdateBox.Text = productById.ProductName;
            BarcodeUpdateBox.Text = productById.Barcode.ToString();
            PriceUpdateBox.Text = productById.ProductPrice.ToString();
            StockQuantityUpdateBox.Text = productById.StockQuantity.ToString();
            CategoryUpdateList.SelectedItem = productById.Category;
            return productById;
        }
    }
}
