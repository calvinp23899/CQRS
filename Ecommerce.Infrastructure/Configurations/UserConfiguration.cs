using Ecommerce.Core.Constants;
using Ecommerce.Core.Extensions;
using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Properties
            builder.ToTable("User");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Username)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
            builder.Property(p => p.Password)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.Firstname)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.Lastname)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.Email)
                    .IsRequired(true)
                    .HasColumnType("varchar");
            builder.HasIndex(p => p.Email)
                    .IsUnique();
            builder.Property(p => p.PhoneNumber)
                    .IsRequired(false)
                    .HasColumnType("varchar")
                    .HasMaxLength(20);
            builder.Property(p => p.RefreshToken)
                    .IsRequired(false)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.ExpiredRefreshToken)
                    .IsRequired(false)
                    .HasColumnType("timestamptz");
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
            builder.HasMany(c => c.Orders).WithOne(e => e.User);
            #endregion

            #region SeedData
            builder.HasData
                (
                    new User
                    {
                        Id = 1,
                        Firstname = "admin",
                        Lastname = "1",
                        Username = "admin",
                        Password = "admin123".EncryptData(),
                        Email = "coloshopclient1@gmail.com",
                        PhoneNumber = "0812345555",
                        CreatedBy = StringConstant.SystemDefault,
                        CreatedDate = DateTimeOffset.Now,
                        IsDeleted = false
                    }
                );
            #endregion
        }
    }
}
