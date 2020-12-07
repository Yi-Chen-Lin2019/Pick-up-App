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

namespace WPFNav
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window, INotifyPropertyChanged
    {
        private string _userName;
        private string _password;
        public event PropertyChangedEventHandler PropertyChanged;

        public LogInWindow()
        {
            InitializeComponent();
            //IsUsernameAndPasswordValid();
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        //private string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        _password = value;
        //        OnPropertyChanged("Password");
        //    }
        //}

        //public bool CanLogIn(ref string userName, ref string password)
        //{
        //    bool output = false;
        //    //userName = this.UserName;
        //    //password = this.Password;
        //    if(userName.Length > 0 && password.Length > 0)
        //    {
        //        output = true;
        //    }
        //    return output;
        //}

        //public bool CanLogIn(string userName)
        //{
        //    bool output = false;
        //    if (OnPropertyChanged(userName))
        //    {
        //        output = true;
        //    }
        //    return output;
        //}

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!String.IsNullOrWhiteSpace(usrBox.Text))
        //    {
        //        MainWindow main = new MainWindow();
        //        main.Show();
        //        this.Close();
        //    } else
        //    {
        //        MessageBox.Show("type in your username");
        //    }
        //}

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (usrBox.Text != "" && pwdBox.SecurePassword.Length != 0)
        //    {
                
        //        if (pwdBox.SecurePassword.Length == 1) //if user found it returns 1  
        //        {

        //            MainWindow obj = new MainWindow();
        //            obj.Show(); //after login Redirect to second window  
        //            this.Close(); 


        //        }
        //        else
        //        {

        //            MessageBox.Show("InValid UserId Or word");

        //        }
        //    }
        //    else
        //    {

        //        MessageBox.Show("UserId and word Is Required");

        //    }
        //}

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
                try
                {
                    //if(await api.Authenticate(username, password))
                    //{
                        MainWindow main = new MainWindow();
                         main.Show();
                        this.Close();
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        //public async Task Login()
        //{
        //    try
        //    {
        //        var result = await api.Authenticate(Username, Password);
        //    } catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void pwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    //if (String.IsNullOrWhiteSpace(pwdBox.Password) && String.IsNullOrWhiteSpace(usrBox.Text))
        //    //{
        //    //    LoginButton.IsEnabled = false;
        //    //}
        //    //else
        //    //{
        //    //    LoginButton.IsEnabled = true;
        //    //}

        //    if (pwdBox.SecurePassword.Length == 0)
        //    {
        //        LoginButton.IsEnabled = false;
        //    }
        //    else
        //    {
        //        LoginButton.IsEnabled = true;
        //    }
        //}

        public void IsUsernameAndPasswordValid()
        {
            //return (String.IsNullOrEmpty(usrBox.Text) && pwdBox.SecurePassword.Length == 0);
            //bool canLogin = false;
            if (pwdBox.SecurePassword.Length != 0 && !String.IsNullOrWhiteSpace(usrBox.Text))
            {
                LoginButton.IsEnabled = true;
                //canLogin = false;
            }
            else
            {
                LoginButton.IsEnabled = false;
                //canLogin = true;
            }
            //return canLogin;
            ////return LoginButton.IsEnabled;
        }

    }
}
