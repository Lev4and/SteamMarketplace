namespace SteamMarketplace.Model.Common
{
    public class BaseResponseModel<T>
    {
        public T Result { get; private set; }

        public Status Status { get; private set; }

        public BaseResponseModel(T result, Status status)
        {
            Result = result;
            Status = status;
        }
    }
}
