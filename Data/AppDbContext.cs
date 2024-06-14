using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Command> Commands { get; set; }
}