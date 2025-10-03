
namespace Domain.Infrastructure
{
    public interface IImageRepository
    {
        public Task<ImageCreateResult> StoreImageAsync(string userId, Stream data, CancellationToken token);

        public string GetImageUrl(string userId, string imageId);
    }

    public record ImageCreateResult(string photoId, string url);
}