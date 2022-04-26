namespace SteamMarketplace.Model.Common
{
    public class BaseResponseModel<T>
    {
        public T Result { get; }

        public Status Status { get; }

        public BaseResponseModel(T result, Status status)
        {
            Result = result;
            Status = status;
        }
    }
}
