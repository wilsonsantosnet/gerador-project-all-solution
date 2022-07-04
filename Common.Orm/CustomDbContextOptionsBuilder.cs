using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;

namespace Common.Orm
{
    public static class CustomDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseCustomSqlServerQuerySqlGenerator(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IQuerySqlGeneratorFactory, CustomSqlServerQuerySqlGeneratorFactory>();
            return optionsBuilder;
        }
    }

    class CustomSqlServerQuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {
        public CustomSqlServerQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies)
            => Dependencies = dependencies;
        public QuerySqlGeneratorDependencies Dependencies { get; }
        public QuerySqlGenerator Create() => new CustomSqlServerQuerySqlGenerator(Dependencies);
    }

    public class CustomSqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        public CustomSqlServerQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies)
            : base(dependencies) { }
        protected override Expression VisitTable(TableExpression tableExpression)
        {
            // base will append schema, table and alias
            var result = base.VisitTable(tableExpression);
            Sql.Append(" WITH (NOLOCK)");
            return result;
        }
    }

}
