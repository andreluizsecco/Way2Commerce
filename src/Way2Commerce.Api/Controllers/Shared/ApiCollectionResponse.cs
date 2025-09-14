namespace Way2Commerce.Api.Controllers.Shared
{
    public class ApiCollectionResponse<T>
    {
        public int Count { get; private set; }
        public IEnumerable<T> Data { get; private set; }

        public ApiCollectionResponse(IEnumerable<T> data)
        {
            Count = data.Count();
            Data = data;
        }
    }

    public static class ApiCollectionResponseExtensions
    {
        public static ApiCollectionResponse<T> ToApiCollectionResponse<T>(this IEnumerable<T> data) =>
            new ApiCollectionResponse<T>(data);
    }
}
