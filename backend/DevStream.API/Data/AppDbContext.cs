using DevStream.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevStream.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Deployment> Deployments => Set<Deployment>();
}