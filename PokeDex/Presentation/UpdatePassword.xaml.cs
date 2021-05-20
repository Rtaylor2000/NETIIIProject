using DataObject;
using Logic;
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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for UpdatePassword.xaml
    /// </summary>
    public partial class UpdatePassword : Window
    {
        private User _user;
        private IUserManager _userManager;
        private bool _isNewUser;
        public UpdatePassword(User user, IUserManager userManager,
            bool isNewUser = false)
        {
            _user = user;
            _userManager = userManager;
            _isNewUser = isNewUser;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!txtEmail.Text.IsValidEmail() || txtEmail.Text != _user.Email)
            {
                MessageBox.Show("Invalid Email Address.");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }
            if (!pwpOldPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Invalid Password.");
                pwpNewPassword.Clear();
                pwpNewPassword.Focus();
                return;
            }

            if (!pwpNewPassword.Password.IsValidPassword()
                || pwpNewPassword.Password == "newuser")
            {
                MessageBox.Show("Invalid Password.");
                pwpNewPassword.Clear();
                pwpNewPassword.Focus();
                return;
            }

            if (!((string)pwpNewPassword.Password == (string)pwpRetypePassword.Password))
            {
                MessageBox.Show("Passwords must match.");
                pwpRetypePassword.Clear();
                pwpRetypePassword.Focus();
                return;
            }

            try
            {
                if (_userManager.UpdatePassword(_user, pwpOldPassword.Password, pwpNewPassword.Password))
                {
                    MessageBox.Show("Profile updated", "Password Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
                else
                {
                    throw new ApplicationException("Password not changed.");
                }
            }
            catch (Exception ex)
            {
                pwpNewPassword.Clear();
                pwpRetypePassword.Clear();
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message); // object reference not set to an instance of an  object
                pwpNewPassword.Focus();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isNewUser)
            {
                tbkMessage.Text = "On First Login " + tbkMessage.Text;
                txtEmail.Text = _user.Email;
                txtEmail.IsEnabled = false;
                pwpOldPassword.Password = "newuser";
                pwpOldPassword.IsEnabled = false;
                pwpNewPassword.Focus();
            }
        }
    }
}
