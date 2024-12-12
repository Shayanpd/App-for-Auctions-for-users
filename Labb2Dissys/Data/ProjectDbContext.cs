using Labb2Dissys.Core;
using Microsoft.EntityFrameworkCore;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

    // DbSets for your models
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
}