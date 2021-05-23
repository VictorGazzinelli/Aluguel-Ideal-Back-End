using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Model;
using FluentMigrator.Runner;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AluguelIdeal.Infrastructure.Extensions
{
    public static class FluentMigrationExtensions
    {

        private static void WriteSqlFile(IMigrationContext migrationContext, IMigrationGenerator migrationGenerator, string sqlScriptsDirectory, IMigrationInfo migration)
        {
            IEnumerable<string> sqlExpressions = migration.GetSqlExpressions(migrationContext, migrationGenerator);
            string fileName = $"{migration.GetName().Replace(": ", "_")}.sql";
            string path = Path.Combine(sqlScriptsDirectory, fileName);
            File.WriteAllLines(path, sqlExpressions);
        }

        public static void CreateMigrationsSqlEquivalent(this IMigrationInformationLoader migrationInformationLoader, IMigrationContext migrationContext, IMigrationGenerator migrationGenerator, string sqlScriptsDirectory)
        {
            foreach (IMigrationInfo migration in migrationInformationLoader.LoadMigrations().Select(kvp => kvp.Value))
                WriteSqlFile(migrationContext, migrationGenerator, sqlScriptsDirectory, migration);
        }

        public static IEnumerable<string> GetSqlExpressions(this IMigrationInfo migrationInfo, IMigrationContext migrationContext, IMigrationGenerator migrationGenerator)
        {
            List<string> sqlExpressions = new List<string>
            {
                "-- UP"
            };

            migrationInfo.Migration.GetUpExpressions(migrationContext);
            sqlExpressions.AddRange(migrationContext.Expressions.Select(exp => exp.GetSqlEquivalent(migrationGenerator)));
            migrationContext.Expressions.Clear();
            sqlExpressions.Add("-- DOWN");
            migrationInfo.Migration.GetDownExpressions(migrationContext);
            sqlExpressions.AddRange(migrationContext.Expressions.Select(exp => exp.GetSqlEquivalent(migrationGenerator)));
            migrationContext.Expressions.Clear();

            return sqlExpressions;
        }

        public static string GetSqlEquivalent(this IMigrationExpression migrationExpression, IMigrationGenerator migrationGenerator)
        {
            if (migrationExpression is ExecuteSqlScriptExpression executeSqlScriptExpression)
            {
                return executeSqlScriptExpression.SqlScript;
            }
            else if (migrationExpression is ExecuteSqlStatementExpression executeSqlStatementExpression)
            {
                return executeSqlStatementExpression.SqlStatement;
            }
            else
            {
                if (migrationExpression is CreateForeignKeyExpression cfke)
                {
                    cfke.ForeignKey.ForeignColumns = cfke.ForeignKey.ForeignColumns.Select(c => c.ToSnakeCase()).ToList();
                    cfke.ForeignKey.PrimaryColumns = cfke.ForeignKey.PrimaryColumns.Select(c => c.ToSnakeCase()).ToList();
                    cfke.ForeignKey.ForeignTable = cfke.ForeignKey.ForeignTable.ToSnakeCase();
                    cfke.ForeignKey.PrimaryTable = cfke.ForeignKey.PrimaryTable.ToSnakeCase();
                    cfke.ForeignKey.Name = cfke.ForeignKey.Name ??
                        $"FK_{cfke.ForeignKey.ForeignTable}_{cfke.ForeignKey.ForeignColumns.First()}_{cfke.ForeignKey.PrimaryTable}_{cfke.ForeignKey.PrimaryColumns.First()}";
                    
                }
                else if (migrationExpression is CreateTableExpression cte)
                {
                    List<ColumnDefinition> columnDefinitions = cte.Columns.ToList();
                    columnDefinitions.ForEach(cd => cd.Name = cd.Name.ToSnakeCase());
                    cte.Columns = columnDefinitions;
                }
                else if (migrationExpression is CreateConstraintExpression cce && cce.Constraint.IsPrimaryKeyConstraint)
                {
                    cce.Constraint.Columns = cce.Constraint.Columns.Select(c => c.ToSnakeCase()).ToList();
                    cce.Constraint.ConstraintName = $"PK_{cce.Constraint.TableName}";
                }
                MethodInfo processMethod = migrationGenerator.GetType().GetMethod("Generate", new[] { migrationExpression.GetType() });
                string sql = (string)processMethod.Invoke(migrationGenerator, new[] { migrationExpression });

                return sql;
            }
        }
    }
}
