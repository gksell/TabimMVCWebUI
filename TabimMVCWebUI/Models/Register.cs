using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TabimMVCWebUI.Entity;

namespace TabimMVCWebUI.Models
{
    public class Register
    {
        
        [Required]
        [DisplayName("İsim")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyisim")]
        public string SurName { get; set; }
        [Required]
        [DisplayName("Email Adresiniz")]
        [EmailAddress(ErrorMessage = "Eposta Formatında giriş yapınız.")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Telefon Numaranız")]
        [Phone(ErrorMessage ="Telefon numarası formatında giriş yapınız")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Şifreniz")]
        
        public string Password { get; set; }
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string RePassword { get; set; }

       
    }
}