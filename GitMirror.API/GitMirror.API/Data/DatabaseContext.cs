using GitMirror.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Repository> Repositories { get; set; }
    public DbSet<Mirror> Mirrors { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<History> Histories { get; set; }
}
