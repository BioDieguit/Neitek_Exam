namespace Neitek.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public T data { get; set; }
        public string error { get; set; }

        public ApiResponse()
        {
            success = false;
            data = default(T);
            error = string.Empty;
        }
        public ApiResponse(string err = "")
        {
            success = false;
            data = default(T);
            error = err;
        }

    }
}
