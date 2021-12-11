using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class DataContext : DbContext
    {
        private const string connectionString
     = "server=localhost;port=3306;database=quanlysieuthi;uid=root;password=";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);
        }
        public DbSet<Category> Category { set; get; }
        public DbSet<Product> Product { set; get; }
        public DbSet<Promotion> Promotion { set; get; }
        public DbSet<Image> Image { set; get; }
        public DbSet<Image_product> Image_product { set; get; }
        public DbSet<Comment> Comment { set; get; }
        public DbSet<Orders> Orders { set; get; }
        public DbSet<Orders_user> Orders_user { set; get; }
        public DbSet<Order_items> Order_items { set; get; }
        public DbSet<Order_user_items> Order_user_items { set; get; }
        public DbSet<News> News { set; get; }
        public DbSet<Customer> Customer { set; get; }
        public DbSet<Users> Users { set; get; }
        public DbSet<Banner> Banner { set; get; }
        public DbSet<Cart> Cart { set; get; }
        public DbSet<Wishlist> Wishlist { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => new { c.id_cat });
            modelBuilder.Entity<Product>().HasKey(c => new { c.id_pro });
            modelBuilder.Entity<Image>().HasKey(c => new { c.id_img });
            modelBuilder.Entity<Image_product>().HasKey(c => new { c.id_img });
            modelBuilder.Entity<Comment>().HasKey(c => new { c.id_cmt });
            modelBuilder.Entity<Orders>().HasKey(c => new { c.id_ord });
            modelBuilder.Entity<Orders_user>().HasKey(c => new { c.id_ord });
            modelBuilder.Entity<Order_items>().HasKey(c => new { c.id_ord });
            modelBuilder.Entity<Order_items>().HasKey(c => new { c.id_pro });
            modelBuilder.Entity<Order_user_items>().HasKey(c => new { c.id_ord });
            modelBuilder.Entity<Order_user_items>().HasKey(c => new { c.id_pro });
            modelBuilder.Entity<News>().HasKey(c => new { c.id_news });
            modelBuilder.Entity<Customer>().HasKey(c => new { c.id_cus });
            modelBuilder.Entity<Users>().HasKey(c => new { c.id });
            modelBuilder.Entity<Banner>().HasKey(c => new { c.id_banner });
            modelBuilder.Entity<Cart>().HasKey(c => new { c.id_cart });
            modelBuilder.Entity<Wishlist>().HasKey(c => new { c.id_user });
            modelBuilder.Entity<Wishlist>().HasKey(c => new { c.id_pro });
            modelBuilder.Entity<Promotion>().HasKey(c => new { c.id_promotion });
        }
    }
}
