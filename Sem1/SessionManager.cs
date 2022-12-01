using System;
using System.Runtime.Caching;

namespace HttpServer
{
    public class SessionManager
    {
        static MemoryCache _cache = MemoryCache.Default;
        
        public static string CreateSession(int accountId, string login, bool isLong)
        {
            var session = new Session(Guid.NewGuid(), accountId, login);
            _cache.Set(session.Id.ToString(), session, isLong ? DateTimeOffset.Now.AddMinutes(10) : DateTimeOffset.Now.AddMonths(1));
            var s = _cache.Get(session.Id.ToString());
            return session.Id.ToString();
        }

        public static bool ValidateSession(string sessionId)
        {
            var session = GetInformation(sessionId);
            if (session == null) return false;
            if (DateTime.Now - TimeSpan.FromMinutes(15) <= session.CreateDateTime) return true;
            return false;
        }

        public static Session GetInformation(string sessionId)
        {
            var session = _cache.Get(sessionId);
            return (Session)session;
        }

        public static string UpdateSession(string sessionId, bool isLong)
        {
            var session = GetInformation(sessionId);
            if(ValidateSession(sessionId)) _cache.Set(session.Id.ToString(), session, isLong ? DateTimeOffset.Now.AddMinutes(10) : DateTimeOffset.Now.AddMonths(1));;
            return sessionId.ToString();
        }

        public static void ExpireSession(Session session)
        {
            _cache.Remove(session.Id.ToString());
        }
    }
}