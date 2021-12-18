using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TabimMVCWebUI.Entity
{
    public class UserOperation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Soyisim alanı zorunludur.")]
        [DisplayName("Ad-Soyad")]
        public string NameSurname { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Belge Zorunludur.")]
        [DisplayName("Eklenecek Belge")]
        public string Documantation { get; set; }
        public bool? IsApproved { get; set; }
        public string ManagerDescription { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? ManagerUploadTime { get; set; }
        public string UserId { get; set; }
    }
}