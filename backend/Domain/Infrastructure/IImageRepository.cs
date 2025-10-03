
namespace Domain.Infrastructure
{
    public interface IImageRepository
    {
        public Task<string> StoreImageAsync(string userId, byte[] data, CancellationToken token);

        public string GetImageUrl(string userId, string imageId);
    }
}