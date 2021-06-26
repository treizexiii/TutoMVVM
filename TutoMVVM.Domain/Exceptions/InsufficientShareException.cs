using System;

namespace TutoMVVM.Domain.Exceptions
{
    public class InsufficientShareException : Exception
    {
        public string Symbol { get; }
        public int AccountShares { get; }
        public int RequiredShares { get; }

        public InsufficientShareException(string symbol, int accountShares, int requiredShares)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientShareException(string message, string symbol, int accountShares, int requiredShares) : base(message)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientShareException(string message, Exception innerException, string symbol, int accountShares, int requiredShares) : base(message, innerException)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }
    }
}
