namespace Core.Dtos
{
    public class StandardResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }

    }

    public class StandardResult<T> : StandardResult
    {
        public T Result { get; set; }
    }
}
