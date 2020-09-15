using System.Linq;
using System.Reflection;
using API.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet< Product> Products{get;set;}
        public DbSet<ProductBrand> ProductBrands{get;set;}
        public DbSet<ProductType> ProductTypes{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var type in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = type.ClrType.GetProperties().Where(x => x.PropertyType == typeof(decimal));
                    
                     foreach(var property in properties)
                        {
                            modelBuilder.Entity(type.Name).Property(property.Name).HasConversion<double>();
                        }  
                }
            }
        }
    

    }
}