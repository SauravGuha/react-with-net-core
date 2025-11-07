

namespace Application.ViewModels
{
    public class PaginatedRequest<T>
    {
        public T? Cursor { get; set; }

        private int limit = 5;
        public int? Limit
        {
            get { return limit; }
            set
            {
                if (value != null && value < 50)
                    limit = value.Value;
            }
        }
    }
}