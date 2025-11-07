

namespace Application.ViewModels
{
    public class ActivityPaginatedRequest : PaginatedRequest<string?>
    {
        public string? FilterBy { get; set; }

        public string? FilterDate { get; set; } = DateTime.UtcNow.ToString("o");
    }
}