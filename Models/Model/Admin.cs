using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır.")]
        public string Email { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır.")]

        public string Password { get; set; }
        public string Auyhority { get; set; }
    }
}