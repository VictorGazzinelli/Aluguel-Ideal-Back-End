using System;

namespace AluguelIdeal.Api.Gateways
{
    public sealed class UnexpectedDatabaseBehaviourException : Exception
    {
        public UnexpectedDatabaseBehaviourException(string message) : base(message)
        {

        }
    }
}
