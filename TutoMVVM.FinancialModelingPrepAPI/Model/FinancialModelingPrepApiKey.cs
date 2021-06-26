using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutoMVVM.FinancialModelingPrepAPI.Model
{
    public class FinancialModelingPrepApiKey
    {
        public string Key { get; set; }

        public FinancialModelingPrepApiKey(string key)
        {
            Key = key;
        }
    }
}
