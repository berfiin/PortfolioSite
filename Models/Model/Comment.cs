using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Yorum")]
    public class Comment
    {
        public int CommentId { get; set; }
        [Required,StringLength(50, ErrorMessage = "50 karakter olabilir")]
        public string FirstNameLastName { get; set; }
        public string Email { get; set; }
        public string _Content { get; set; }
        public bool Confirmation { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }


    }
}