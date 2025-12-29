using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DesafioMSA.Infraestructure.Extensions
{
    public static class QueriableExtension
    {
        public static IQueryable<TEntity> ApplyListFilters<TEntity>(
            this IQueryable<TEntity> query,
            ICollection<Expression<Func<TEntity, bool>>> filters
        )
        {
            foreach (var filter in filters)
                query = query.Where(filter);
            return query;
        }
    }
}
