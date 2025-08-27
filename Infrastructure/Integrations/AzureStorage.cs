using Azure.Storage.Blobs;
using Domain.Port;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Utilidades;

namespace Infrastructure.Integration
{
    public class AzureStorage : IAzureStorage
    {
        private IConfiguration Configuration { get; }

        private BlobContainerClient containerClient = null;

        public AzureStorage(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> UploadFileStorage(byte[] file, string name, string ext, string container)
        {
                if (file is null)
                {
                    return string.Empty;
                }
                await ConectionStorage(container);

                string fileName = name + ext;

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();

                using (var stream = new MemoryStream(file, writable: false))
                {
                    await blobClient.UploadAsync(stream, true);
                }

                return blobClient.Uri.ToString();
        }

        public   async Task<Stream>  DownloadFileStorage(string fileName, string container)
        {
            await ConectionStorage(container);

            var blob = containerClient.GetBlobClient(fileName);

            Stream blobStream = blob.OpenRead();

            return blobStream;

        }

        private async Task ConectionStorage(string container)
        {
             string connectionString = Configuration["Storage:conectionString"];
             BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
             containerClient = blobServiceClient.GetBlobContainerClient(container);
             await containerClient.CreateIfNotExistsAsync();

        }
    }
}
