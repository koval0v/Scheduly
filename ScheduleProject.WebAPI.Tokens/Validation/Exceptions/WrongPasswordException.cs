using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when password is incorrect.
    /// </summary>
    /// <seealso cref="CustomException" />
    public class WrongPasswordException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongPasswordException"/> class.
        /// </summary>
        /// <param name="mes"></param>
        public WrongPasswordException(string mes) : base(mes) { }
    }
}
