using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoInspira.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : EcoInspiraException
    {
        public IList<string> ErrorMensages { get; set; }

        public ErrorOnValidationException(IList<string> errorMensages)
        {
            ErrorMensages = errorMensages;
        }
    }
}
