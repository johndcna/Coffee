using Coffee.API.Common;

namespace Coffee.API.Models
{
    public class ResponseService<T>
    {
        public T? Data { get; set; }
        public int StatusCode {get;set;} = Constants.StatusCodes.Success;
    }
}
