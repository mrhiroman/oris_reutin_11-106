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
    [HttpController("deals")]
    public class Deals
    {
        private DealRepository _repository = new DealRepository();

        [HttpGET("list")]
        public List<Deal> GetAllDeals(HttpListenerContext context, string collectionName)
        {
            return new List<Deal>();
        }

        [HttpGET("id")]
        public Deal GetDealById(HttpListenerContext context, int id)
        {
            return new Deal();
        }

        [RequireAuth]
        [HttpPOST("sell")]
        public string Sell(HttpListenerContext context, int nftId, int price)
        {
            var nft = new NftRepository().GetById(nftId);
            string sessionId = context.Request.Cookies["SessionId"]?.Value.Replace('.',',');;
            var status = JsonSerializer.Deserialize<AuthCookie>(sessionId);
            var isOwner = sessionId != null && SessionManager.ValidateSession(status.Id) && nft.OwnerId == SessionManager.GetInformation(status.Id).AccountId;
            if (nft.CollectionId == null) return "Error!";
            if (isOwner)
            {
                return _repository.AddToSellList(new Deal
                {
                    Cost = price, NftId = nftId, SellerId = nft.OwnerId, Status = "active",
                    CollectionId = Convert.ToInt32(2)
                });
            }

            return "Error! You are not the Owner";
        }

        [RequireAuth]
        [HttpGET("buy")]
        public string Buy(HttpListenerContext context, int dealId)
        {
            return "";
        }
        
    }
}