﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MainVIewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
