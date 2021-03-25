// NB: A dto is basically a container for moving data between layers -no buisenss logic, only simple setters and getters

namespace API.Dtos
{
    public class ProductToReturnDto
    {                                                                                       // this is returned as a flat object that contains the below properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }                            // these are related with BaseEntity.cs so that when we set-up entity framework, we use product keys via int Id
        public string ProductBrand { get; set; }

    }
}