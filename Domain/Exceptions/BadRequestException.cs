namespace Domain.Exceptions
{
    // Когда пользователь передал некорректные данные
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
