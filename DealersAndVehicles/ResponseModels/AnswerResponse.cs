namespace DealersAndVehicles
{
    public class AnswerResponse : IApiError
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int totalMilliseconds { get; set; }
        public string ErrorMessage { get; set; }
    }
}