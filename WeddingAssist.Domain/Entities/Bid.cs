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
        public IList<BidService> Services { get; set; }

        public Bid()
        {
            Services = new List<BidService>();
        }

        public Decimal TotalAmount()
        {
            Decimal amount = 0;
            foreach (var service in Services)
                amount += service.TotalAmount();
            return amount;
        }

    }
}

