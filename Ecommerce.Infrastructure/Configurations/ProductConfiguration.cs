﻿using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Properties
            builder.ToTable("Product");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.ProductName)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal");
            builder.Property(p => p.IsDeleted)
                    .IsRequired(false)
                    .HasDefaultValue(false)
                    .HasColumnType("boolean");
            builder.Property(p => p.CreatedBy)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.CreatedDate)
                    .IsRequired(true)
                    .HasColumnType("timestamptz");
            builder.Property(p => p.UpdatedBy)
                    .IsRequired(false)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.UpdatedDate)
                    .IsRequired(false)
                    .HasColumnType("timestamptz");
            #endregion

            #region Relationships
            builder.HasMany(e => e.OrderDetails).WithOne(e => e.Product);
            #endregion

            #region SeedData
            //builder.HasData
            //    (
            //        new User
            //        {

            //        }
            //    );
            #endregion
        }
    }
}
