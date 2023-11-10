namespace Talabat.APIs.Errors
{
    public class ApiExeptionResponce : ApiResponce
    {
        public string? Details { get; set; }
        public ApiExeptionResponce(int statusCode , string? message = null , string? details = null):base(statusCode , message)
        {
            Details = details;
        }
    }
}
