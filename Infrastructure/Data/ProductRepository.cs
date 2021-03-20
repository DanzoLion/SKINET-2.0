using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data 

{
    public class ProductRepository : IProductRepository 
    {
        private readonly StoreContext _context;
        public ProductRepository (StoreContext context) {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync (int id) // the interface  methods defined in the IProductRepository
        {
            
           return await _context.Products               // we chain on methods to build up our database requests ie the exrpession tree
            .Include(p => p.ProductType)                // (p => p.ProductType) is a lambda expression and is used as a throwaway parameter
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);   // we supply predicate as lambda expression 
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync () // method defined in IProductRepository
        {
            return await _context.Products
            .Include(p => p.ProductType)                // (p => p.ProductType) is a lambda expression and is used as a throwaway parameter
            .Include(p => p.ProductBrand)
            .ToListAsync();   // NB: ToListAsync() is the method that touches our database and executes the query and returns the data from our database we use                     
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            
            return await _context.ProductTypes.ToListAsync();
        }
    }
}