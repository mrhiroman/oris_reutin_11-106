using System;
using System.Runtime.Caching;

namespace HttpServer
{
    public class SessionManager
    {
        ObjectCache _cache = MemoryCache.Default;
        
        public void CreateSession(int accountId, string login)
        {
            var session = new Session(Guid.NewGuid(), accountId, login);
            _cache.Set(session.Id.ToString(), session, DateTimeOffset.Now.AddMinutes(2));
        }

        public bool ValidateSession(int sessionId)
        {
            var session = GetInformation(sessionId);
            if (DateTime.Now - TimeSpan.FromMinutes(15) >= session.CreateDateTime) return true;
            return false;
        }

        public Session GetInformation(int sessionId)
        {
            var session = _cache.Get(sessionId.ToString());
            return (Session)session;
        }

        public void UpdateSession(int sessionId)
        {
            var session = GetInformation(sessionId);
            if(ValidateSession(sessionId)) _cache.Set(session.Id.ToString(), session, DateTimeOffset.Now.AddMinutes(2));
        }
    }
}