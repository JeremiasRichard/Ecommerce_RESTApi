namespace RemitoApi.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message, int statusCode) : base(message, statusCode)
        {

        }
    }
}
