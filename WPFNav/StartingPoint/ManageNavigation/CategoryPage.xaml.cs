using Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        LocalService ls = new LocalService();
        public CategoryPage()
        {
            InitializeComponent();
        }

        private void ClearTextBoxes()
        {
            foreach (UIElement ctl in CategoryGrid.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = string.Empty;
            }
        }

        public async void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category
            {
                CategoryName = CategoryNameBox.Text
            };

            if (await ls.PostCategory(category))
            {
                MessageBox.Show("done");
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Something went wrong, try again");
            }
        }

        public async void ReadCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Category> categoryList = await ls.GetAllCategories();
            CategoriesList.ItemsSource = categoryList;
        }

        public async void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;
            if (String.IsNullOrWhiteSpace(OldCategoryNameBox.Text) || int.TryParse(OldCategoryNameBox.Text, out parsedValue))
            {
                MessageBox.Show("type in name");
            } 
            else
            {
                Category updatedCategory = await ls.GetCategory(OldCategoryNameBox.Text);
                if (updatedCategory != null)
                {
                    updatedCategory.CategoryName = NewCategoryNameBox.Text;
                };

                if (await ls.UpdateCategory(updatedCategory))
                {
                    MessageBox.Show("done");
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }

        public async void ReadCategoryByNameButton_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;
            if (String.IsNullOrWhiteSpace(ReadCategoryNameBox.Text) || int.TryParse(ReadCategoryNameBox.Text, out parsedValue))
            {
                MessageBox.Show("type in name");
            }
            else
            {
                Category category = await ls.GetCategory(ReadCategoryNameBox.Text);
                CategoryByNameBox.Text = category.ToString();
            }
        }
    }
}
