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
    public class AuthorTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
        builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.FirstName).IsRequired();
            builder.Property(s => s.LastName).IsRequired();
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.Email).IsRequired();
            builder.HasMany(s => s.Books).WithOne(b=> b.Author).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
