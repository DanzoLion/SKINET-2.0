using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
        :  base( x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&                  // filter for product string name
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&                   // filter for product ProductBrandId then ProductTypeId
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
        }
    }
}