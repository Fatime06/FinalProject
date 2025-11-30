namespace Service.DTOs.CommonDtos
{
    public class ErrorDto
    {
        public string Name { get; set; } = "Ooops,Error";
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
