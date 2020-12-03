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
using WPFNav.StartingPoint.ManageNavigation;

namespace WPFNav
{
    /// <summary>
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();
            ManageProductButton.Click += ManageProductClick;
            ManageOrderButton.Click += ManageOrderClick;
            ManageCategoryButton.Click += ManageCategoryClick;
        }

        public void ManageProductClick(object sender, EventArgs e)
        {
            ManagePageFrame.Content = new ProductPage();
            ManageProductText.Foreground = Brushes.MidnightBlue;
            //HomeText.FontWeight = FontWeights.Bold;

            ManageOrderText.Foreground = Brushes.Gray;
            ManageCategoryText.Foreground = Brushes.Gray;
        }

        public void ManageOrderClick(object sender, EventArgs e)
        {
            ManagePageFrame.Content = new ManageOrder(1);
            ManageOrderText.Foreground = Brushes.MidnightBlue;
            //HomeText.FontWeight = FontWeights.Bold;

            ManageProductText.Foreground = Brushes.Gray;
            ManageCategoryText.Foreground = Brushes.Gray;
        }

        public void ManageCategoryClick(object sender, EventArgs e)
        {
            ManagePageFrame.Content = new CategoryPage();
            ManageCategoryText.Foreground = Brushes.MidnightBlue;
            //HomeText.FontWeight = FontWeights.Bold;

            ManageOrderText.Foreground = Brushes.Gray;
            ManageProductText.Foreground = Brushes.Gray;
        }
    }
}
