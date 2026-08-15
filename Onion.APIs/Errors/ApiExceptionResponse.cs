namespace Onion.APIs.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public ApiExceptionResponse(int? StatusCode,string? Message,string? Details):base(StatusCode,Message)
        {
            details = Details;
        }
        public string? details { get; set; }
    }

}
