using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrimeService.Data.Queries
{
    public static partial class PrimeNumberExtensions
    {
        #region Generated Extensions
        public static PrimeService.Data.Entities.PrimeNumber GetByKey(this IQueryable<PrimeService.Data.Entities.PrimeNumber> queryable, long number)
        {
            if (queryable is DbSet<PrimeService.Data.Entities.PrimeNumber> dbSet)
                return dbSet.Find(number);

            return queryable.FirstOrDefault(q => q.Number == number);
        }

        public static ValueTask<PrimeService.Data.Entities.PrimeNumber> GetByKeyAsync(this IQueryable<PrimeService.Data.Entities.PrimeNumber> queryable, long number)
        {
            if (queryable is DbSet<PrimeService.Data.Entities.PrimeNumber> dbSet)
                return dbSet.FindAsync(number);

            var task = queryable.FirstOrDefaultAsync(q => q.Number == number);
            return new ValueTask<PrimeService.Data.Entities.PrimeNumber>(task);
        }

        #endregion

    }
}
