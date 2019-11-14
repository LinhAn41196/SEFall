using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Organization name required")]
        public string OrganizationName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Email required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }


    }
} 