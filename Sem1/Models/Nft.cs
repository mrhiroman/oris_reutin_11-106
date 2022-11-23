namespace HttpServer.Models
{
    public class Nft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string ImagePath { get; set; }
        public int CollectionId { get; set; }
    }
}