using System;
using System.IO;
using System.Net;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using Scriban;

namespace HttpServer.Controllers
{
    [HttpController(("profile"))]
    public class Profile
    {
        [HttpGET("")]
        public string RenderProfile(HttpListenerContext context)
        {
            var sessionId = context.Request.Cookies["SessionId"] != null ? JsonSerializer.Deserialize<AuthCookie>(context.Request.Cookies["SessionId"]?.Value).Id : null;
            if (sessionId != null)
            {
                var status = SessionManager.ValidateSession(sessionId);
                if (status)
                {
                    var userId = SessionManager.GetInformation(sessionId).AccountId;
                    string text = File.ReadAllText("templates/profile/index.html");

                    var tpl = Template.Parse(text);
                    var userRepository = new UserRepository();
                    var nftRepository = new NftRepository();
                    var mdl = userRepository.GetById(userId);
                    var nfts = nftRepository.GetAllForUser(mdl.Id);
                    return tpl.Render(new {mdl.Login, mdl.Balance, mdl.Email, nfts}, m => m.Name);
                }
               
            }

            return "Redirect: login_page";
        }
    }
}