using Microsoft.EntityFrameworkCore;

public class LaLigaDbContext : DbContext
{
    public DbSet<Match> Matches {get; set;}

    public LaLigaDbContext(DbContextOptions<LaLigaDbContext> options) : base(options) { }
}
