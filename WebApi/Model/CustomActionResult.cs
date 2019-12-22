using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WebApi.Enum;

namespace WebApi.Model
{
    public class CustomActionResult
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public object Data { get; set; } = null;
    }
}

