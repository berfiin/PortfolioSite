using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        public string Title { get; set; }
        public string _Content { get; set; }
        public string ImageURL { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}