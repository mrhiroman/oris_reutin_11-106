using System;

namespace HttpServer
{
    public class Session
    {
        public Guid Id { get; private set; }
        public int AccountId { get; private set; }
        public string Login { get; private set; }
        public DateTime CreateDateTime { get; private set; }

        public Session(Guid id, int accountId, string login)
        {
            Id = id;
            AccountId = accountId;
            Login = login;
            CreateDateTime = DateTime.Now;
        }
    }
}