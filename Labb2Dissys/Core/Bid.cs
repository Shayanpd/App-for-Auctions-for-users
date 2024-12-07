namespace Labb2Dissys.Core;


    public class Bid
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Bidder { get; set; } = string.Empty; // Email of the bidder
        public DateTime Timestamp { get; set; }
    }
