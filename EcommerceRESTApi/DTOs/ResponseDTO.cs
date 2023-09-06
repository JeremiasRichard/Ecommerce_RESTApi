namespace EcommerceRESTApi.DTOs
{
    public class ResponseDTO
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public ResponseDTO(string statusCode, string message) { StatusCode = statusCode; Message = message; }

        public ResponseDTO()
        {
        }

    }
}
