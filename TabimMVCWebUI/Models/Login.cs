using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TabimMVCWebUI.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Email Adresiniz")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Şifreniz")]
        public string Password { get; set; }
    }
}