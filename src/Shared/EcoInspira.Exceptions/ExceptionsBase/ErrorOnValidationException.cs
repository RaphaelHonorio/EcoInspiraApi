namespace EcoInspira.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : EcoInspiraException
    {
        public IList<string> ErrorMensages { get; set; }

        public ErrorOnValidationException(IList<string> errorMensages) : base(string.Empty)
        {
            ErrorMensages = errorMensages;
        }
    }
}
