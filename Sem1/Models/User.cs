namespace HttpServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public int Balance { get; set; }
    }
}