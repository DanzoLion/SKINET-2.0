namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string prictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PrictureUrl = prictureUrl;
        }

        public int ProductItemId {get; set;}
        public string ProductName {get; set;}
        public string PrictureUrl {get; set;}
    }
}
