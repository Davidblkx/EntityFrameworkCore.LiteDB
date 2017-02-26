using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public interface IQueryMethodProvider
    {
        MethodInfo ShapedQueryMethod { get; }
    }
}