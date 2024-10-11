using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastracture.Presistance;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(a => a.Assignments)
            .WithOne(u => u.User)
            .HasForeignKey(i => i.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(p => p.Phone)
            .IsUnique();

    }
}
