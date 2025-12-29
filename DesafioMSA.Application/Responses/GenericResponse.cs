namespace DesafioMSA.Application.Responses
{
    public class GenericResponse<T>
    {
        public T? Data { get; set; }
        public bool Success => !Errors.Any();
        public string? Message { get; set; }
        public bool NotFounded { get; set; } = false;
        public string[] Errors { get; set; } = [];
    }
}
