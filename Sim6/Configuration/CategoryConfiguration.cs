using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim6.Models;

namespace Sim6.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.Projects).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
