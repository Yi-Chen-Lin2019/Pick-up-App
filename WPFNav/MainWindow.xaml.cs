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

namespace WPFNav
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HomeButton.Click += HomeButtonClick;
            ManageButton.Click += ManageButtonClick;
            AboutButton.Click += AboutButtonClick;
        }

        public void HomeButtonClick(object sender, EventArgs e)
        {
            MainFrame.Content = new HomePage();
            HomeText.Foreground = Brushes.MidnightBlue;
            //HomeText.FontWeight = FontWeights.Bold;

            ManageText.Foreground = Brushes.Gray;
            AboutText.Foreground = Brushes.Gray;
        }

        public void ManageButtonClick(object sender, EventArgs e)
        {
            MainFrame.Content = new ManagePage();
            ManageText.Foreground = Brushes.MidnightBlue;
            //ManageText.FontWeight = FontWeights.Bold;

            HomeText.Foreground = Brushes.Gray;
            AboutText.Foreground = Brushes.Gray;
        }

        public void AboutButtonClick(object sender, EventArgs e)
        {
            MainFrame.Content = new AboutPage();
            AboutText.Foreground = Brushes.MidnightBlue;
            //AboutText.FontWeight = FontWeights.Bold;

            HomeText.Foreground = Brushes.Gray;
            ManageText.Foreground = Brushes.Gray;
        }

    }
}
