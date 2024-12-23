using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    public class ProhibitedOperationException : Exception
    {
        public ProhibitedOperationException(string mes): base(mes) { }
        public ProhibitedOperationException(){ }
    }
}
