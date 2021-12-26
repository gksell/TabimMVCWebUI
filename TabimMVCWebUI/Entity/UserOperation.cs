using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Belge")]
        public string Documantation { get; set; }
        [DisplayName("Değerlendirme")]
        public bool? IsApproved { get; set; }
        [DisplayName("Admin Açıklaması")]
        public string ManagerDescription { get; set; }
        [DisplayName("Kullanıcı Talep Tarihi")]
        public DateTime? UploadTime { get; set; }
        [DisplayName("Admin Değerlendirme Tarihi")]
        public DateTime? ManagerUploadTime { get; set; }
        [DisplayName("Kullanıcı ID")]
        public string UserId { get; set; }
    }
}