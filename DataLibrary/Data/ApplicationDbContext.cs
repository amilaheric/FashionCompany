using DataLibrary;
using DataLibrary.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionCo.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();

            modelBuilder.Entity<ShoppingCartItem>()
               .HasKey(x => new { x.ShoppingCartId, x.ProductId });
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(x => x.ShoppingCart)
                .WithMany(x => x.ShoppingCartItem)
                .HasForeignKey(x => x.ShoppingCartId);
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ShoppingCartItem)
                .HasForeignKey(x => x.ProductId);

        }

        public DbSet<User> user { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<Gender> gender { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
     
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }






    }
}
