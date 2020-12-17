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
        List<Category> categoryList = new List<Category>();
        Category category;
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



        #region CreateCategory
        public async void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, try again");
            }
        }

        #endregion


        #region UpdateCategory
        private async Task<Category> LoadCategory(string categoryName)
        {
            try
            {
                Category categoryByName = await ls.GetCategory(categoryName);
                OldCategoryNameBox.Text = categoryByName.CategoryName;
                return categoryByName;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
                return null;
            }
        }

        public async void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }
        #endregion


        #region ReadCategories
        private async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return categoryList = await ls.GetAllCategories();
            } catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
                return null;
            }
        }

        private void RepopulateCategoryList()
        {
            CategoryList.Items.Clear();
            foreach (var item in categoryList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = item.ToString();
                listViewItem.Name = item.CategoryName;
                CategoryList.Items.Add(listViewItem);
            }
        }

        public async void ReadCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetAllCategoriesAsync();
                RepopulateCategoryList();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.");
            }
            //try
            //{
            //    List<Category> categoryList = await getAllCategoriesAsync();
            //    CategoryList.ItemsSource = categoryList;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Something went wrong, try again");
            //}
        }

        public async void ReadCategoryByNameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(ReadCategoryNameBox.Text))
                {
                    MessageBox.Show("Type in category name");
                }
                else
                {
                    IEnumerable<Category> possibleSuspects = await GetAllCategoriesAsync();
                    possibleSuspects = possibleSuspects.Where(c => c.CategoryName.ToLower().Contains(ReadCategoryNameBox.Text.ToLower()));
                    categoryList = possibleSuspects.ToList();
                    CategoryList.ItemsSource = categoryList;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, try again");
            }
        }

        private async void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView v = (ListView)sender;
                if (v.HasItems)
                {
                    ListViewItem selectedItem = (ListViewItem)e.AddedItems[0];
                    category = await LoadCategory(selectedItem.Name);
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
