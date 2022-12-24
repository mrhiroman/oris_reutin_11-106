using System.IO;
using System.Threading.Tasks;

namespace HTMLEngineLibrary
{
    public interface IEngineHtmlService
    {
        string GetHtml(string template, object model);
        string GetHtml(Stream pathTemplate, object model);
        string GetHtml(byte[] bytes, object model);
        Stream GetHtmlInStream(string template, object model);
        Stream GetHtmlInStream(Stream pathTemplate, object model);
        Stream GetHtmlInStream(byte[] bytes, object model);
        byte[] GetHtmlInBytes(string template, object model);
        byte[] GetHtmlInBytes(Stream pathTemplate, object model);
        byte[] GetHtmlInBytes(byte[] bytes, object model);

        void GenerateAndSaveInDirectory(string templatePath, string outputPath, string outputNameFile, object model);
        void GenerateAndSaveInDirectory(Stream templatePath, string outputPath, string outputNameFile, object model);
        void GenerateAndSaveInDirectory(byte[] bytes, string outputPath, string outputNameFile, object model);

        Task GenerateAndSaveInDirectoryAsync(string templatePath, string outputPath, string outputNameFile, object model);
        Task GenerateAndSaveInDirectoryAsync(Stream templatePath, string outputPath, string outputNameFile, object model);
        Task GenerateAndSaveInDirectoryAsync(byte[] bytes, string outputPath, string outputNameFile, object model);
    } 
}