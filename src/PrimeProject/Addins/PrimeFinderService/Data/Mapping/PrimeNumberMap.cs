using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrimeService.Data.Mapping
{
    public partial class PrimeNumberMap
        : IEntityTypeConfiguration<PrimeService.Data.Entities.PrimeNumber>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PrimeService.Data.Entities.PrimeNumber> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("PrimeNumber");

            // key
            builder.HasKey(t => t.Number);

            // properties
            builder.Property(t => t.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("INTEGER");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "";
            public const string Name = "PrimeNumber";
        }

        public struct Columns
        {
            public const string Number = "Number";
        }
        #endregion
    }
}
