


using System.Dynamic;
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

        public async Task<ImageCreateResult> StoreImageAsync(string userId, Stream data, CancellationToken token)
        {
            var blobClient = blobContainer.GetBlobClient($"{userId}/{Guid.NewGuid()}");
            var result = await blobClient.UploadAsync(data, token);

            return new ImageCreateResult(result!.Value.VersionId, blobClient.Uri.AbsoluteUri);
        }
    }
}