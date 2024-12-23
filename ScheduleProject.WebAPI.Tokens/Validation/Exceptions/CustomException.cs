using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{

    /// <summary>
    /// The basic custom exception
    /// </summary>
    /// <seealso cref="Exception" />
    public class CustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        public CustomException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CustomException(string message) : base(message)
        {

        }
    }
}
