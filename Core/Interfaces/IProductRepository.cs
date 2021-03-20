using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
         Task<Product> GetProductByIdAsync(int id);                      // these are our interface members
        Task<IReadOnlyList<Product>> GetProductsAsync();          // we return a list of items here and we are specific about a Read Only list in this case
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();          // temporatily, we are adding all our types under a single repository as we build up the application 
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();          // 
    }
}