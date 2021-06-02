using System.Collections.Generic;

namespace TutoMVVM.Domain.Models
{
    public class Account : DomainObjet
    {
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
