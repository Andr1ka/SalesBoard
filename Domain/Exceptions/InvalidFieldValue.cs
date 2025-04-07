namespace Domain.Exceptions
{
    public class InvalidFieldValue : Exception
    {
        public InvalidFieldValue(string fieldName) : base($"{fieldName} has invalid value")
        {
        }
    }
}
