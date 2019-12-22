using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Enum
{
    public enum StatusCode
    {
        PageAccesDenied = 1,
        Forbidden = 2,
        Unauthorized = 3,
        Success = 4,
        Info = 5,
        Warning = 6,
        Error = 7,
    }
}
