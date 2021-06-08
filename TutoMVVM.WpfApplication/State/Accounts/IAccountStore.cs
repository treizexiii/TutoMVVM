using System;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.WpfApplication.State.Accounts
{
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
