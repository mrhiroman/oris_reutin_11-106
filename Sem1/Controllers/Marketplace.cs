using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using HttpServer.Models.Repositories;
using MyORM;
using Scriban;

namespace HttpServer.Controllers
{
    [HttpController("marketplace")]
    public class Marketplace
    {
        [HttpGET("")]
        public string RenderMarketplace(HttpListenerContext context)
        {
            var nftRepository = new NftRepository();
            var dealRepository = new DealRepository();
            var dealList = dealRepository.RetrieveSellList(0, 5);
            var nftList = new List<NftWithPrice>();
            foreach (var deal in dealList)
            {
                var nft = nftRepository.GetById(deal.NftId);
                nftList.Add(new NftWithPrice
                {
                    Id = nft.Id, Name = nft.Name, Price = deal.Cost, DealId = deal.Id,
                    CollectionId = nft.CollectionId, ImagePath = nft.ImagePath, OwnerId = nft.OwnerId
                });
            }

            var collectionRepository = new CollectionRepository();
            var collections = collectionRepository.GetAll();
            var collectionList = new List<CollectionWithNftList>();
            foreach (var collection in collections.Skip(2))
            {
                var nfts = new List<NftWithPrice>();
                var deals = dealRepository.RetrieveCollection(collection.Id, 0, 5);
                foreach (var deal in deals)
                {
                    var nft = nftRepository.GetById(deal.NftId);
                    nfts.Add(new NftWithPrice
                    {
                        Id = nft.Id, Name = nft.Name, Price = deal.Cost, DealId = deal.Id,
                        CollectionId = nft.CollectionId, ImagePath = nft.ImagePath, OwnerId = nft.OwnerId
                    });
                }

                collectionList.Add(new CollectionWithNftList {Id = collection.Id, Name = collection.Name, Nfts = nfts});
            }

            var tpl = Template.Parse(File.ReadAllText(StaticSetting.TemplateFolder + "/marketplace/index.html"));
            return tpl.Render(new {sellNfts = nftList, Collections = collectionList}, member => member.Name);
        }

        [HttpGET("getitems")]
        public string GetItems(HttpListenerContext context, int collectionId, int from, int amount)
        {
            var nftRepository = new NftRepository();
            var dealRepository = new DealRepository();
            var dealList = dealRepository.RetrieveCollection(collectionId, from, amount);
            var nftList = new List<NftWithPrice>();
            foreach (var deal in dealList)
            {
                var nft = nftRepository.GetById(deal.NftId);
                nftList.Add(new NftWithPrice
                {
                    Id = nft.Id, Name = nft.Name, Price = deal.Cost, DealId = deal.Id,
                    CollectionId = nft.CollectionId, ImagePath = nft.ImagePath, OwnerId = nft.OwnerId
                });
            }
            
            var tpl = Template.Parse(File.ReadAllText(StaticSetting.TemplateFolder + "/marketplace/items.html"));
            return tpl.Render(new {sellNfts = nftList}, member => member.Name);
        }

        public class NftWithPrice : Nft
        {
            public int Price { get; set; }
            public int DealId { get; set; }
        }

        public class CollectionWithNftList : Collection
        {
            public List<NftWithPrice> Nfts;
        }

    }
}