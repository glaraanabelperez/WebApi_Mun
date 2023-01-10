namespace WebApi_Mun.Controllers
{
    public class ItemBuy
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CurrencyId { get; set; }

        public ItemBuy()
        {
            this.CurrencyId = "ARS";
        }
    }
}