using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Identification")]
    public class Identification
    {
        [Key]
        public int IdentificationId { get; set; }
       
        [DisplayName("Website Title")]

        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public string Degree { get; set; }



    }
}