using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ModelConfigs
{
    public class BookTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
            builder.Property(b => b.DateOfPublication).IsRequired();
            builder.Property(b => b.CreatedBy).IsRequired();
            builder.Property(b => b.Count).IsRequired();
            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
