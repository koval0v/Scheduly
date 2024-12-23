using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    public class DifferenceEmailException : CustomException
    {
        public DifferenceEmailException(string mes) : base(mes) { }
    }
}
