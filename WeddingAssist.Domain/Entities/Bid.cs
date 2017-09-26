using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Bid
    {
        public int BidId { get; set; }
        public int ProviderId { get; set; }
        public int AuctionId { get; set; }
        public DateTime BidCreationDate { get; set; }

    }
}

