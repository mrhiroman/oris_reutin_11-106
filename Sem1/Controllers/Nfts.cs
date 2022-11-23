using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using MyORM;
using Scriban;


namespace HttpServer.Controllers
{
    [HttpController("nfts")]
    public class Nfts
    {
        private NftRepository _repository = new NftRepository();

        [HttpGET("list")]
        public List<Nft> GetAllByCollection(HttpListenerContext context, string collectionName)
        {
            return new List<Nft>();
        }

        [HttpGET("id")]
        public string GetNftById(HttpListenerContext context, int id)
        {
            var nft = _repository.GetById(id);
            var user = new UserRepository().GetById(nft.OwnerId);
            var tpl = Template.Parse(File.ReadAllText("templates/nft/index.html"));
            string sessionId = context.Request.Cookies["SessionId"]?.Value.Replace('.',',');;
            var status = JsonSerializer.Deserialize<AuthCookie>(sessionId);
            var isOwner = sessionId != null && SessionManager.ValidateSession(status.Id) && nft.OwnerId == SessionManager.GetInformation(status.Id).AccountId;
            return tpl.Render(new {Id= nft.Id, Name = nft.Name, ImagePath = nft.ImagePath, Owner = user.Login, IsOwner = isOwner}, m => m.Name);
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