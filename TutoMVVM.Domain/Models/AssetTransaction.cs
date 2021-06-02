using System;

namespace TutoMVVM.Domain.Models
{
    public class AssetTransaction : DomainObjet
    {
        public Account Account { get; set; }
        public bool IsPurchase { get; set; }
        public Asset Asset { get; set; }
        public int Shares { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
