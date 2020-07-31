using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.DataContext
{
    public class WebDBContext:DbContext
    {
        public WebDBContext() : base("WebDB")
        {
                
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Category> Category{ get; set; }
        public DbSet<Identification> Identification { get; set; }
        public DbSet<Service> Sevices { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
}