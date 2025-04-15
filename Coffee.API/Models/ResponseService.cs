namespace Coffee.API.Models
{
    public class ResponseService<T>
    {
        public T? Data { get; set; }
        public int StatusCode {get;set;} = 200;
    }
}
