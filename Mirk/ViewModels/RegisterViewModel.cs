using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string VerifyPassword { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "E-Mail is required")]
        public string Email { get; set; }

        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
