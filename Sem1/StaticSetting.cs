namespace HttpServer
{
    public class StaticSetting
    {
        public static int Port { get; set; } = 4800;
        public static string Site { get; set; } = @"./site";
        public static string TemplateFolder { get; set; } = @"./templates";
        public static string ConnectionString { get; set; }
    }
}