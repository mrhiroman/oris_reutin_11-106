namespace HttpServer.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int NftId { get; set; }
        public int SellerId { get; set; }
        public string Status { get; set; }
        public int CollectionId { get; set; }
    }
}