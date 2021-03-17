using System;

namespace AluguelIdeal.Api.Repositories
{
    public sealed class UnexpectedDatabaseBehaviourException : Exception
    {
        public UnexpectedDatabaseBehaviourException(string message) : base(message)
        {

        }
    }
}
