namespace SteamMarketplace.Model.Common
{
    public class PagedResponseModel<T> : BaseResponseModel<Paged<T>>
    {
        public PagedResponseModel() : base(new Paged<T>(new List<T>(), 0, 0, 0), Statuses.Success)
        {

        }

        public PagedResponseModel(List<T> result, int page, int limit, int totalItems, Status status) :
            base(new Paged<T>(result, page, limit, totalItems), status)
        {

        }
    }
}
