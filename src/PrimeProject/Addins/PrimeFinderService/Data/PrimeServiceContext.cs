using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrimeService.Data
{
    public partial class PrimeServiceContext : DbContext
    {
        public PrimeServiceContext(DbContextOptions<PrimeServiceContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<PrimeService.Data.Entities.PrimeNumber> PrimeNumbers { get; set; }

        public virtual DbSet<PrimeService.Data.Entities.Summary> Summaries { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new PrimeService.Data.Mapping.PrimeNumberMap());
            modelBuilder.ApplyConfiguration(new PrimeService.Data.Mapping.SummaryMap());
            #endregion
        }
    }
}
