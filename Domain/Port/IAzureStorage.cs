using System.IO;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Port
{
    public interface IAzureStorage 
    {
        Task<string> UploadFileStorage(byte[] file, string name, string ext, string container);
        Task<Stream> DownloadFileStorage(string filetoDownload, string container);
    }
}