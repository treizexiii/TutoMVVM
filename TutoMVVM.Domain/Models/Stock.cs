﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutoMVVM.Domain.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public double PricePerShare { get; set; }
    }
}
