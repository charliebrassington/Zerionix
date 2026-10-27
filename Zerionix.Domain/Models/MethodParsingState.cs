using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public enum MethodParsingState
    {
        Continue = 0,
        WaitingForMethodCall = 1,
        Completed = 2
    }
}
