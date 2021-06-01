using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutoMVVM.Domain.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool State { get; set; }
        public dynamic Object { get; set; }
    }
}
