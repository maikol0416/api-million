using System.Threading.Tasks;

namespace ServiceApplication.Port
{
    public interface IBlobStorage
    {
        Task<string> UploadFileStorage(byte[] file, string name, string ext, string destino);
    }
}
