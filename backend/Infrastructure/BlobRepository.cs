
using System.Threading.Tasks;
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

        public async Task DeleteImage(string userId, string imageId, string ext, CancellationToken token)
        {
            var blobClient = blobContainer.GetBlobClient($"{userId}/{imageId}.{ext}");
            await blobClient.DeleteAsync(cancellationToken: token);
        }

        public string GetImageUrl(string userId, string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<ImageCreateResult> StoreImageAsync(string userId, string ext, Stream data, CancellationToken token)
        {
            var imageId = Guid.NewGuid();
            var blobClient = blobContainer.GetBlobClient($"{userId}/{imageId}.{ext}");
            await blobClient.UploadAsync(data, token);
            return new ImageCreateResult(imageId.ToString(), blobClient.Uri.AbsoluteUri);
        }
    }
}