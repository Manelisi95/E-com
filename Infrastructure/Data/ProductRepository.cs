using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        //Product
        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            return await _context.Products
            .Include(p=> p.ProductType).
            Include(p=>p.ProductBrand).
            ToListAsync();
        }
         public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _context.Products
            .Include(p=> p.ProductType)
            .Include(p=>p.ProductBrand)
            .FirstOrDefaultAsync(p=>p.Id== Id);
        }

        //Brand

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
          return await _context.ProductBrands.ToListAsync();
        }

       //Types

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
           return await _context.ProductTypes.ToListAsync();
        }
    }
}