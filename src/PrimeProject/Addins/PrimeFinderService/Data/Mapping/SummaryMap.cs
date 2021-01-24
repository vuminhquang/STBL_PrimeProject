using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrimeService.Data.Mapping
{
    public partial class SummaryMap
        : IEntityTypeConfiguration<PrimeService.Data.Entities.Summary>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PrimeService.Data.Entities.Summary> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Summary");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.MaximumNumberReached)
                .HasColumnName("MaximumNumberReached")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "";
            public const string Name = "Summary";
        }

        public struct Columns
        {
            public const string MaximumNumberReached = "MaximumNumberReached";
            public const string Id = "Id";
        }
        #endregion
    }
}
