namespace HttpServer
{
    public class ServerSetting
    {
        public int Port { get; set; } = 4800;
        public string Site { get; set; } = @"./site";
        public string TemplateFolder { get; set; } = @"./templates";
        public string ConnectionString { get; set; }
    }
}