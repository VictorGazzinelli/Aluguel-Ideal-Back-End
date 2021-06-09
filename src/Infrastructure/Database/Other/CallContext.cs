using System;
using System.Data;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class CallContext : IDisposable
    {
        public Action<IDbConnection, string, object> HistoryCallback { get; set; }
        public Attribute CallerMetadata { get; set; }
        public static CallContext Instance
        {
            get
            {
                if (_instanceCounter++ == 0)
                {
                    _instance = new CallContext();
                }
                return _instance;
            }
        }
        [ThreadStatic]
        private static CallContext _instance;
        [ThreadStatic]
        private static int _instanceCounter;

        public void Dispose()
        {
            if (--_instanceCounter == 0)
            {
                _instance = null;
            }
        }
    }
}
