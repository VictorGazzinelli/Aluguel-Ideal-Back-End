using System;

namespace AluguelIdeal.Api.Gateways
{
    public abstract class BaseGateway : IDisposable
    {
        protected readonly string connectionName;

        protected BaseGateway(string connectionName)
        {
            this.connectionName = connectionName;
        }

        public void Dispose()
        {
            // 
        }
    }
}
