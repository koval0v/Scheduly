using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when registration faulted.
    /// </summary>
    /// <seealso cref="CustomException" />
    public class InvalidRegistrationException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> class.
        /// </summary>
        /// <param name="mes"></param>
        public InvalidRegistrationException(string mes) : base(mes) { }
    }
}
