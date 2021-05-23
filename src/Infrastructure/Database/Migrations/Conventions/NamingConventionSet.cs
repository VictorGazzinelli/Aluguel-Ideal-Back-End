using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using System.Collections.Generic;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Conventions
{
    public class NamingConventionSet : IConventionSet
    {
        public IList<IColumnsConvention> ColumnsConventions { get; }

        public IRootPathConvention RootPathConvention { get; }

        public DefaultSchemaConvention SchemaConvention { get; }

        public IList<IConstraintConvention> ConstraintConventions { get; }

        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }

        public IList<IIndexConvention> IndexConventions { get; }

        public IList<ISequenceConvention> SequenceConventions { get; }

        public IList<IAutoNameConvention> AutoNameConventions { get; }

        public NamingConventionSet(): this(new DefaultConventionSet(),
                                           new SnakeCaseColumnsConvention(),
                                           new SnakeCaseForeignKeyConvention(),
                                           new SnakeCaseConstraintConvention())
        {
        }

        private NamingConventionSet(IConventionSet innerConventionSet,
                                    IColumnsConvention snakeCaseColumnsConvention,
                                    IForeignKeyConvention snakeCaseForeignKeyConvention,
                                    IConstraintConvention snakeCaseConstraintConvention)
        {
            ColumnsConventions = new List<IColumnsConvention>()
            {
                snakeCaseColumnsConvention,
            };

            ForeignKeyConventions = new List<IForeignKeyConvention>()
            {
                snakeCaseForeignKeyConvention,
            };

            ConstraintConventions = new List<IConstraintConvention>()
            {
                snakeCaseConstraintConvention,
            };

            SequenceConventions = innerConventionSet.SequenceConventions;
            AutoNameConventions = innerConventionSet.AutoNameConventions;
            SchemaConvention = innerConventionSet.SchemaConvention;
            RootPathConvention = innerConventionSet.RootPathConvention;
            IndexConventions = innerConventionSet.IndexConventions;
        }
    }
}
