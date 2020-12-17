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
        List<Category> categories = new List<Category>();
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

        private async Task<List<Category>> getAllCategoriesAsync()
        {
            return categories = await ls.GetAllCategories();
        }

        #region CreateCategory
        public async void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category
            {
                CategoryName = CategoryNameBox.Text
            };

            if (await ls.PostCategory(category))
            {
                MessageBox.Show("Done");
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Something went wrong, try again");
            }
        }

        #endregion


        #region UpdateCategory
        public async void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;
            if (String.IsNullOrWhiteSpace(OldCategoryNameBox.Text) || int.TryParse(OldCategoryNameBox.Text, out parsedValue))
            {
                MessageBox.Show("Type in Name");
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
                    MessageBox.Show("Done");
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }
        #endregion



        #region ReadCategories
        public async void ReadCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Category> categoryList = await getAllCategoriesAsync();
            CategoriesList.ItemsSource = categoryList;
        }

        public async void ReadCategoryByNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ReadCategoryNameBox.Text))
            {
                MessageBox.Show("Type in category name");
            }
            else
            {
                IEnumerable<Category> possibleSuspects = await getAllCategoriesAsync();
                possibleSuspects = possibleSuspects.Where(c => c.CategoryName.ToLower().Contains(ReadCategoryNameBox.Text.ToLower()));
                categories = possibleSuspects.ToList();
                CategoriesList.ItemsSource = categories;
            }
        }
        #endregion

    }
}
