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
    /// Interaction logic for MemberRoleActive.xaml
    /// </summary>
    public partial class MemberRoleActive : Window
    {
        private IMemberManager _memberManager = new MemberManager();
        private Member _member;
        public MemberRoleActive(Member member)
        {
            _member = member;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMemberID.Text = _member.MemberID.ToString();
            txtFirstName.Text = _member.FirstName;
            txtLastName.Text = _member.LastName;
            txtEmail.Text = _member.Email;
            chkActive.IsChecked = _member.Active;
            txtRole.Text = _member.Role;

        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Edit")
            {
                setUpEdit();
            }
            else //if it says save
            {
                if (!txtRole.Text.isValidRole())
                {
                    MessageBox.Show("Invalid Role. (admin, researcher, user)");
                    txtRole.Focus();
                    txtRole.SelectAll();
                    return;
                }
                var newMember = new Member()
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    Email = txtEmail.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Active = (bool)chkActive.IsChecked,
                    Role = txtRole.Text
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
        }
        private void setUpEdit()
        {
            btnEditSave.Content = "Save";
            chkActive.IsEnabled = true;
            txtRole.IsReadOnly = false;
            txtRole.BorderBrush = Brushes.Black;
            txtRole.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
