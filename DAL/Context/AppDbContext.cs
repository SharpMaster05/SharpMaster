using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Build> Builds { get; set; }
    public DbSet<Region> Region { get; set; }
}
