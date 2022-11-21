using System.Collections.Generic;

namespace HttpServer.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<Nft> Nfts { get; set; }
    }
}