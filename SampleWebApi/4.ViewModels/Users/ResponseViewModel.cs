namespace SampleWebApi._4.ViewModels.Users
{
    public class ResponseViewModel
    {
        public ResponseStatus Status { get; set; }
        public string? Message { get; set; }
    }

    public class ResponseViewModel<TData>: ResponseViewModel
    {
        public TData? Data { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Error,
        ValidationError
    }
}
