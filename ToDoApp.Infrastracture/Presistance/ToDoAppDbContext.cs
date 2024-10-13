using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastracture.Presistance;

public class ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : DbContext(options)
{
    public DbSet<Assignment> Assignments { get; set; }
    public  DbSet<User> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
