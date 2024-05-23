using System.Net;

namespace HouseKPN.Dto
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get;  set; } = new List<string>();
    }
}
