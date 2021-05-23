using FluentMigrator;
using System;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Attributes
{
    public class MigrationTimestampVersionAttribute : MigrationAttribute
    {
        public MigrationTimestampVersionAttribute(string description, TransactionBehavior transactionBehavior, int year, int month, int day, int hour, int minute, int second)
            : base(GetVersionNumber(new DateTime(year, month, day, hour, minute, second)), transactionBehavior, description)
        {

        }

        private static long GetVersionNumber(DateTime timestamp) =>
            Convert.ToInt64(timestamp.ToString("yyyyMMddHHmmss"));
    }
}
