
namespace Domain.Infrastructure
{
    public interface IImageRepository
    {
        public Task<ImageCreateResult> StoreImageAsync(string userId, string ext, Stream data, CancellationToken token);

        public string GetImageUrl(string userId, string imageId);
    }

    public record ImageCreateResult(string photoId, string url);
}