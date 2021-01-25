using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrimeService.Data.Queries
{
    public static partial class SummaryExtensions
    {
        #region Generated Extensions
        public static PrimeService.Data.Entities.Summary GetByKey(this IQueryable<PrimeService.Data.Entities.Summary> queryable, long id)
        {
            if (queryable is DbSet<PrimeService.Data.Entities.Summary> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<PrimeService.Data.Entities.Summary> GetByKeyAsync(this IQueryable<PrimeService.Data.Entities.Summary> queryable, long id)
        {
            if (queryable is DbSet<PrimeService.Data.Entities.Summary> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<PrimeService.Data.Entities.Summary>(task);
        }

        #endregion

    }
}
