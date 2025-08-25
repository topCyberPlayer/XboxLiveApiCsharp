namespace Domain.Exceptions
{
    // Когда ресурс не найден
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
