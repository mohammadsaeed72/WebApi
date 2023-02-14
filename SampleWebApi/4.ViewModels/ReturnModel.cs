namespace SampleWebApi._4.ViewModels
{
    public class ReturnModel
    {
        public ReturnModel()
        {
            IsSuccess = true;
        }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class ReturnModel<Tmodel>:ReturnModel
    {
        public Tmodel? Data { get; set; }
    }
}
