using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebAPI.Models
{
    public class WebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public WebAPIContext() : base("name=WebAPIContext")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            //modelBuilder.Entity<Order>()
            //        .HasRequired(c=>c.Customer)
            //        .WithMany()
            //        .WillCascadeOnDelete(false);

            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<OrderDetail>()
            //       .HasRequired(c => c.Order)
            //       .WithMany()
            //       .WillCascadeOnDelete(true);

            //base.OnModelCreating(modelBuilder);
        }

    }
}