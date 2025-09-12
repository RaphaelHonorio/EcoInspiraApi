namespace EcoInspira.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : EcoInspiraException
    {
        public InvalidLoginException(): base (ResourceMessagesException.CPF_OR_PASSWORD_INVALID)    
        {
        }
    }
}
