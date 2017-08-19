using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class Auction
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public IList<Bid> Bids { get; private set; }
    }
}
