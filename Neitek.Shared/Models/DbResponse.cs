namespace Neitek.Shared.Models
{
    public class DbResponse<T>
    {
        public bool success { get; set; }
        public T data { get; set; }
        public string error { get; set; }

        public DbResponse()
        {
            success = false;
            data = default;
            error = string.Empty;
        }
        public DbResponse(string err = "")
        {
            success = false;
            data = default;
            error = err;
        }
    }
}


