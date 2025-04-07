namespace Domain.Exceptions
{
    public class FieldIsRequiredException : Exception
    {
        public FieldIsRequiredException(string fieldName) : base($"{fieldName} is required")
        {
        }
    }
}
