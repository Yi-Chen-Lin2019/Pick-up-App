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
                MessageBox.Show("choose category");
            }
            else
            {
                product.Category = (Category)CategoryUpdateList.SelectedItem;
                if (await service.UpdateProduct(this.product))
                {
                    MessageBox.Show("updated successfully");
                }
                else
                {
                    MessageBox.Show("failed to update. please try again");
                }
                ReadProductsButton_Click(null, null);
            }

            //if (!String.IsNullOrWhiteSpace(ProductIdUpdateBox.Text))
            //{
            //    Product updatedProduct = await ls.GetProduct(int.Parse(ProductIdUpdateBox.Text));
            //    if(updatedProduct != null)
            //    {
            //        updatedProduct.ProductId = int.Parse(ProductIdUpdateBox.Text);
            //        updatedProduct.ProductName = ProductNameUpdateBox.Text;
            //        updatedProduct.Barcode = int.Parse(BarcodeUpdateBox.Text);
            //        updatedProduct.ProductPrice = decimal.Parse(PriceUpdateBox.Text);
            //        updatedProduct.StockQuantity = int.Parse(StockQuantityUpdateBox.Text);
            //        updatedProduct.Category = (Category)CategoryUpdateList.SelectedItem;
            //    };

            //    if (await ls.UpdateProduct(updatedProduct))
            //    {
            //        MessageBox.Show("done");
            //        ClearTextBoxes();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Something went wrong");
            //    }


            //}
            //else
            //{
            //    MessageBox.Show("type in id");
            //}


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
                ImageUrl = ProductImageBox.Text,
                ProductName = ProductNameBox.Text,
                Barcode = int.Parse(BarcodeBox.Text),
                ProductPrice = decimal.Parse(PriceBox.Text),
                StockQuantity = int.Parse(StockQuantityBox.Text),
                Category = (Category)CategoryList.SelectedItem
            };

               if(await ls.PostProduct(product))
                {
                MessageBox.Show("done");
                ClearTextBoxes();
                } else
                {
                MessageBox.Show("Something went wrong, try using unique barcode");
                }

        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView v = (ListView)sender;
            if (v.HasItems)
            {
                ListViewItem selectedItem = (ListViewItem)e.AddedItems[0];
                loadProduct(int.Parse(selectedItem.Uid));
            }
        }

        private async void loadProduct(int productId)
        {
            LocalService service = new LocalService();
            product = await service.GetProduct(productId);
            ProductIdUpdateBox.Text = product.ProductId.ToString();
            ProductNameUpdateBox.Text = product.ProductName;
            BarcodeUpdateBox.Text = product.Barcode.ToString();
            PriceUpdateBox.Text = product.ProductPrice.ToString();
            StockQuantityUpdateBox.Text = product.StockQuantity.ToString();
            CategoryUpdateList.SelectedItem = product.Category;
        }

    }
}
