using Newtonsoft.Json;
using System.Globalization;

namespace JobPortal.Infrastructure.Global
{
    public class APIResponse
    {
        public APIResponse() { }

        public APIResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
    }

    public class ServiceResponse<T> : APIResponse
    {
        public ServiceResponse(string message, int statusCode, T? data) : base(message, statusCode)
        {
            Data = data;
        }

        public ServiceResponse(string message, int statusCode) : base(message, statusCode) { }

        public ServiceResponse() { }

        [JsonProperty("data")]
        public T? Data { get; set; }
    }

    public class PagedResponse<T> : ServiceResponse<T>
    {
        public PagedResponse(string message, int statusCode, T? data, int pageIndex, int pageSize, int totalRecords)
            : base(message, statusCode, data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; } = 0;
    }

    public class PagedResponseWithQuery<T>
    {
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; } = 0;

        [JsonProperty("data")]
        public T? Data { get; set; }
    }

    public class PagedResponseInput
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 100;

        [JsonProperty("searchString")]
        public string SearchString { get; set; } = string.Empty;

        public string FormattedSearchString() => SearchString.ToLower().Replace(" ", string.Empty);
    }
}
