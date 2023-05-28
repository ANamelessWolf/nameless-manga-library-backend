namespace Nameless.WebApi.Models
{
    public enum OrderType
    {
        Asc,
        Desc
    }
    public class OrderRequest
    {
        public string orderField { get; set; }
        public OrderType orderType { get; set; }
        public OrderRequest()
        {
            this.orderType = OrderType.Asc;

        }
    }
}