using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using MyORM;


namespace HttpServer.Controllers
{
    [HttpController("nfts")]
    public class Nfts
    {
        private NftRepository _repository = new NftRepository();

        [HttpGET("")]
        public List<Nft> GetAllByCollection(HttpListenerContext context, string collectionName)
        {
            return new List<Nft>();
            //TODO smth..
        }

        [HttpGET("")]
        public Nft GetNftById(HttpListenerContext context, int id)
        {
            //TODO smth..
            return null;
        }

        [RequireAuth]
        [HttpPOST("add")]
        public string AddNftToSellList(HttpListenerContext context, int id)
        {
            // if user has nft -> add to list -> else do nothing
            return "";
        }
    }
}