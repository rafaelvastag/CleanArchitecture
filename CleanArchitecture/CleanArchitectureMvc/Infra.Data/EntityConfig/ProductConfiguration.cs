using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.EntityConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired().HasColumnName("NAME");
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired().HasColumnName("DESCRIPTION");
            builder.Property(p => p.Price).HasPrecision(10, 2).HasColumnName("PRICE");

            builder.Property(p => p.CategoryId).HasColumnName("CATEGORY_ID");

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
