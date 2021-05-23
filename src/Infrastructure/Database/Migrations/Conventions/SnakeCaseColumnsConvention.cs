using AluguelIdeal.Infrastructure.Extensions;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Conventions;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Conventions
{
    public class SnakeCaseColumnsConvention : IColumnsConvention
    {
        public IColumnsExpression Apply(IColumnsExpression expression)
        {
            foreach (ColumnDefinition columDefinition in expression.Columns)
                columDefinition.Name = columDefinition.Name.ToSnakeCase();

            return expression;
        }
    }
}
