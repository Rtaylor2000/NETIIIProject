/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a user object for the current person logged in.
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObject
{
    public class User
    {
        [Display(Name = "User ID")]
        public int UserID { get; private set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; private set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; private set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; private set; }
        public string Role { get; private set; }

        public User(int userID, string firstName, string lastName,
            string email, string role)
        {
            this.UserID = userID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Role = role;
        }
    }
}
