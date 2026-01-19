using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim6.Models;

namespace Sim6.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Image).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.Projects).HasForeignKey(p => p.CategoryId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
