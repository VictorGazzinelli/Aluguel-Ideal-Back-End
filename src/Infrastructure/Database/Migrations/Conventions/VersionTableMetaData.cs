using FluentMigrator.Runner.VersionTableInfo;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Conventions
{
    [VersionTableMetaData]
    public class VersionTableMetaData : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }

        public bool OwnsSchema { get; set; }

        public string SchemaName => "public";

        public string TableName => "version_info";

        public string ColumnName => "version";

        public string DescriptionColumnName => "description";

        public string UniqueIndexName => "UC_version";

        public string AppliedOnColumnName => "applied_on";
    }
}
