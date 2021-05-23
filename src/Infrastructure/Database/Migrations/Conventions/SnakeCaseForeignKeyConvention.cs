using AluguelIdeal.Infrastructure.Extensions;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;
using System.Linq;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Conventions
{
    public class SnakeCaseForeignKeyConvention : IForeignKeyConvention
    {
        public IForeignKeyExpression Apply(IForeignKeyExpression expression)
        {
            expression.ForeignKey.PrimaryColumns = expression.ForeignKey.PrimaryColumns.Select(c => c.ToSnakeCase()).ToList();
            expression.ForeignKey.ForeignColumns = expression.ForeignKey.ForeignColumns.Select(c => c.ToSnakeCase()).ToList();
            expression.ForeignKey.Name = expression.ForeignKey.Name ??
                $"FK_{expression.ForeignKey.ForeignTable}_{expression.ForeignKey.ForeignColumns.First()}_{expression.ForeignKey.PrimaryTable}_{expression.ForeignKey.PrimaryColumns.First()}";

            return expression;
        }
    }
}
