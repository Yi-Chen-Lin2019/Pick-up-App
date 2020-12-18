using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFNav.Service;

namespace WPFNav
{
    /// Log in form for WPF client
    /// Interaction logic for LogInWindow.xaml
    /// login method is done using if statements blocks to check for user input and as latest stage, call authenticate
    /// method to check user input information
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usrBox.Text;
            string password = pwdBox.Password.ToString();
            if(String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
            {
                MessageBox.Show("type in username and password");
            } else if (String.IsNullOrEmpty(username))
            {
                MessageBox.Show("type in username");
            } else if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("type in password");
            } else
            {
                LocalService service = new LocalService();
                try
                {
                    //save the token information
                    //this is how we get it when we need it later on:
                    Application.Current.Resources["TokenInfo"] = Task.Run(async () => await service.Authenticate(username, password)).Result; 
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "bad credentials, try again");
                }
            }

        }


    }
}
