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
    /// Interaction logic for MemberAddEdit.xaml
    /// </summary>
    public partial class MemberAddEdit : Window
    {
        private User _user;
        private Member _member;

        private bool _addUser = false;
        private IMemberManager _memberManager = new MemberManager();
        public MemberAddEdit()
        {
            _member = new Member();
            _addUser = true;

            InitializeComponent();
        }

        public MemberAddEdit(User user) 
        {
            _user = user;
            _member = new Member()
            {
                MemberID = _user.UserID,
                Email = _user.Email,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Active = true,
                Role = _user.Role
            };

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addUser)
            {
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";

                setUpEdit();
            }
            else 
            {
                txtFirstName.Text = _member.FirstName;
                txtLastName.Text = _member.LastName;
                txtEmail.Text = _member.Email;
            }

        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Edit")
            {
                setUpEdit();
            }
            else 
            {
                if (_addUser == false)
                {
                    if (!txtFirstName.Text.isValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }
                    if (!txtLastName.Text.isValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }
                    if (!txtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        return;
                    }
                    var newMember = new Member()
                    {
                        MemberID = _member.MemberID,
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Active = true,
                        Role = _member.Role
                    };
                    try
                    {
                        _memberManager.EditMemberProfile(_member, newMember,
                            newMember.Role, _member.Role);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
                else  //_addUser == true do this
                {
                    if (!txtFirstName.Text.isValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }
                    if (!txtLastName.Text.isValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }
                    if (!txtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        return;
                    }
                    var newMember = new Member()
                    {
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Active = true,
                        Role = "user"
                    };
                    try
                    {
                        _memberManager.AddNewMember(newMember);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void setUpEdit()
        {
            btnEditSave.Content = "Save";
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtFirstName.BorderBrush = Brushes.Black;
            txtLastName.BorderBrush = Brushes.Black;
            txtEmail.BorderBrush = Brushes.Black;
            txtFirstName.Focus();
        }
    }
}
