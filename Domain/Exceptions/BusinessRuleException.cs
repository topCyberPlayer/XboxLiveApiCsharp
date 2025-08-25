namespace Domain.Exceptions
{
    // Когда данные есть, но нарушена бизнес-логика
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }


}
