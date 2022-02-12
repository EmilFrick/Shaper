using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ShaperUser> ShaperUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Transparency> Transparencies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartProduct>().HasKey(cp=> new {cp.ShoppingCartId,cp.ProductId});
            base.OnModelCreating(builder);
        }
    }
}
