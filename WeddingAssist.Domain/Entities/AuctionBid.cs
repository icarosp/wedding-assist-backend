using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class AuctionBid
    {
        public int BidId { get; set; }
        public string ProviderName { get; set; }
        public DateTime BidTime { get; set; }
        public decimal Amount { get; set; }
        public string FormatedAmount { get
            {
                return Amount.ToString("R$ #,##");
            }
            set {}
        }
        public string FormatedBidTime { get
            {
                return BidTime.ToString("dd/MM/yyyy HH:mm:ss");
            } set {}
        }
        public int Position { get; set; }
    }
}
