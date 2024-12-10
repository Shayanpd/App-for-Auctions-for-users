using Microsoft.EntityFrameworkCore;

namespace Labb2Dissys.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<AuctionDb> AuctionDbs { get; set; }
        public DbSet<BidDb> BidDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Auction
            AuctionDb auction1 = new AuctionDb
            {
                Id = -1,
                Title = "Test Car Auction",
                Description = "Classic vintage cars from the 1950s.",
                Seller = "user1@kth.se",
                StartingPrice = 10000m,
                EndDate = DateTime.Now.AddDays(7),
            };

            AuctionDb auction2 = new AuctionDb
            {
                Id = -2,
                Title = "Antique Vase Auction Test Data",
                Description = "Någonting.",
                Seller = "user1@kth.se",
                StartingPrice = 2000m,
                EndDate = DateTime.Now.AddDays(14),
            };

            modelBuilder.Entity<AuctionDb>().HasData(auction1, auction2);

            // Seed data for Bids with correct foreign key references
            BidDb bid1 = new BidDb
            {
                Id = -1,
                Amount = 10500m,
                Bidder = "bidder1@kth.se",
                Timestamp = DateTime.Now.AddDays(-1),
                AuctionId = -1,
            };

            BidDb bid2 = new BidDb
            {
                Id = -2,
                Amount = 12000m,
                Bidder = "bidder2@kth.se",
                Timestamp = DateTime.Now.AddDays(-2),
                AuctionId = -2,
            };

            modelBuilder.Entity<BidDb>().HasData(bid1, bid2);
        }
    }
}