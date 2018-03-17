using System;
using System.Collections.Generic;

namespace INTEC.Models.Infraestructure
{
    public class ServiceResult
    {
        public Boolean Success { get; set; }
        public dynamic ResultObject { get; set; }
        public String ResultTitle { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
