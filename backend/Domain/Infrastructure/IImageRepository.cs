
namespace Domain.Infrastructure
{
    public interface IImageRepository
    {
        public Task<ImageCreateResult> StoreImageAsync(string userId, string ext, Stream data, CancellationToken token);

        public string GetImageUrl(string userId, string imageId);

        public Task DeleteImage(string userId, string imageId, string ext,  CancellationToken token);
    }

    public record ImageCreateResult(string photoId, string url);
}