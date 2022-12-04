using Roya.Errors;

namespace BooksApi.Errors
{
    public class ApiValidationErrorRespose : ApiErroeResponse
    {
        public IEnumerable <string> Errors;    
        public ApiValidationErrorRespose() : base(400)
        {

        }
    }
}
