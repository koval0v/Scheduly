using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when nickname is already taken
    /// </summary>
    /// <seealso cref="CustomException" />
    public class NicknameException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NicknameException"/> class.
        /// </summary>
        /// <param name="mes"></param>
        public NicknameException(string mes) : base(mes) { }
    }
}
