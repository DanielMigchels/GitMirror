using GitMirror.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }
}
