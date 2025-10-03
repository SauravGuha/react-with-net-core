


using Azure.Storage.Blobs;
using Domain.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class BlobRepository : IImageRepository
    {
        private string? blobConnectionString;
        private string? containerName;
        private BlobServiceClient blobService;
        private BlobContainerClient blobContainer;

        public BlobRepository(IConfiguration configuration)
        {
            blobConnectionString = configuration.GetConnectionString("blob");
            containerName = configuration.GetConnectionString("container");
            if (string.IsNullOrWhiteSpace(blobConnectionString) || string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException("blob connection string or container name is missing");
            }
            this.blobService = new BlobServiceClient(blobConnectionString);
            this.blobContainer = blobService.GetBlobContainerClient(containerName);
        }
        public string GetImageUrl(string userId, string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> StoreImageAsync(string userId, byte[] data, CancellationToken token)
        {
            var blobClient = blobContainer.GetBlobClient(userId);

            using (var ms = new MemoryStream(data))
            {
                var result = await blobClient.UploadAsync(ms, token);
                return result!.Value.VersionId;
            }
        }
    }
}