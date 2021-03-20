namespace Core.Entities
{
    public class Product : BaseEntity
    {
     //   public int Id { get; set; }                                       // removed as we created BaseEntity.cs with single property
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }                            // these are related with BaseEntity.cs so that when we set-up entity framework, we use product keys via int Id
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}