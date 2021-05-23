using AluguelIdeal.Infrastructure.Extensions;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;
using System.Linq;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Conventions
{
    public class SnakeCaseConstraintConvention : IConstraintConvention
    {
        public IConstraintExpression Apply(IConstraintExpression expression)
        {
            if (expression.Constraint.IsPrimaryKeyConstraint)
            {
                expression.Constraint.Columns = expression.Constraint.Columns.Select(c => c.ToSnakeCase()).ToList();
                expression.Constraint.ConstraintName = $"PK_{expression.Constraint.TableName}";
            }

            return expression;
        }
    }
}
