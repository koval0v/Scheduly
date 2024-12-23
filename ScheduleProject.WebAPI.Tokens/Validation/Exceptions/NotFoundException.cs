using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when the entity not found.
    /// </summary>
    /// <seealso cref="CustomException" />
    public class NotFoundException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="mes"></param>
        public NotFoundException(string mes) : base(mes) { }
    }
}
