using System;

namespace TutoMVVM.Domain.Models
{
    public class User : DomainObjet
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
