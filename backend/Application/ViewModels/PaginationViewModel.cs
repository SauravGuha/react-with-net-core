

namespace Application.ViewModels
{
    public class PaginationViewModel<TResult, TCursor>
    {
        public TResult Result { get; set; }

        public TCursor? Cursor { get; set; }
    }
}