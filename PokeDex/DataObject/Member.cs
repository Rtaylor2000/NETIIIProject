/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a member object.
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
    public class Member
    {
        [Display(Name = "Member ID")]
        public int MemberID { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
    }
}
