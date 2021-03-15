using System;

namespace AluguelIdeal.Api.Database
{
    public sealed class CouldNotFindDatabaseException : Exception
    {
        public CouldNotFindDatabaseException(string message) : base(message)
        {

        }
    }
}
