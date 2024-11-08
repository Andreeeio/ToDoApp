using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastracture.Presistance;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(a => a.Assignments)
            .WithOne(u => u.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(p => p.Phone)
            .IsUnique();

        builder.HasMany(r => r.Roles)
            .WithMany(r => r.Users);
    }
}