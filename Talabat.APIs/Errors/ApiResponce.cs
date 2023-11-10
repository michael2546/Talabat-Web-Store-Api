namespace Talabat.APIs.Errors
{
    public class ApiResponce
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponce(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int? statusCode )
        {
            // 500 => Internal Server Error
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized",
                404=>"Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
