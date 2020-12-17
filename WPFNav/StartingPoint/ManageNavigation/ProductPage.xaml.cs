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
        List<Product> productList = new List<Product>();
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


        #region CreateProduct
        public async void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
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

                if (await ls.PostProduct(product))
                {
                    MessageBox.Show("Product created.");
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Something went wrong.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        #endregion
        #region ReadProducts
        public async void ReadProductsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await getAllProductsAsync();
                repopulateProductList();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }

        }
        private void repopulateProductList()
        {
            ProductList.Items.Clear();
            foreach (var item in productList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = item.ToString();
                listViewItem.Uid = item.ProductId.ToString();
                ProductList.Items.Add(listViewItem);
            }
        }
        private async Task<List<Product>> getAllProductsAsync()
        {
            try
            {
                return productList = await ls.GetAllProducts();

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
                return null;
            }
        }
        public async void FilterProductsByName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(ProductNameFilterBox.Text))
                {
                    MessageBox.Show("Type in text");
                }
                else
                {
                    IEnumerable<Product> possibleSuspects = await getAllProductsAsync();
                    possibleSuspects = possibleSuspects.Where(p => p.ToString().ToLower().Contains(ProductNameFilterBox.Text.ToLower()));
                    productList = possibleSuspects.ToList();
                    repopulateProductList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }
        }
        private async void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            ListView v = (ListView)sender;
            if (v.HasItems)
            {
                ListViewItem selectedItem = (ListViewItem)e.AddedItems[0];
                product = await loadProduct(int.Parse(selectedItem.Uid));
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        #endregion
        #region UpdateProduct
        private async Task<Product> loadProduct(int productId)
        {
            try
            {
            Product productById = await ls.GetProduct(productId);
            ProductIdUpdateBox.Text = productById.ProductId.ToString();
            ProductNameUpdateBox.Text = productById.ProductName;
            BarcodeUpdateBox.Text = productById.Barcode.ToString();
            PriceUpdateBox.Text = productById.ProductPrice.ToString();
            StockQuantityUpdateBox.Text = productById.StockQuantity.ToString();
            CategoryUpdateList.SelectedItem = productById.Category;
            return productById;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
                return null;
            }
        }

        public async void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                if (await ls.UpdateProduct(this.product))
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
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        #endregion







    }
}
