using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("AboutMe")]
    public class AboutMe
    {
        [Key]
        public int AboutMeId { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }




    }
}