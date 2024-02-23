using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Utils;

using Microsoft.EntityFrameworkCore.ChangeTracking;
public static class DbSetExtensions
{
    // https://dev.to/alexgreatdev/entity-framework-core-add-if-not-exist-507a
    public static EntityEntry<T>? AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>>? predicate = null) where T : class, new()
    {
        var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
        return !exists ? dbSet.Add(entity) : null;
    }
}