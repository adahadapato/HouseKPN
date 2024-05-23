using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HouseKPN.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
           // Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            //    textPassword.Visibility = Visibility.Collapsed;
            //else
            //    textPassword.Visibility = Visibility.Visible;

            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Successfully Signed In");
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            //    //textEmail.Visibility = Visibility.Collapsed;
            //else
            //    //textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }
    }
}
