using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class AssetViewModel : ViewModelBase
    {
        public string Symbol { get; }
        public int Shares { get; }

        public AssetViewModel(string symbol, int shares)
        {
            Symbol = symbol;
            Shares = shares;
        }
    }
}
