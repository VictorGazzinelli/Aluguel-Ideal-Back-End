using System;

namespace AluguelIdeal.Api.Database
{
    public class CouldNotFindDatabaseException : Exception
    {
        public CouldNotFindDatabaseException(string message) : base(message)
        {

        }
    }
}
