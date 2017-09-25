using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Entities
{
    public class AuctionBudget : Budget
    {
        public int NumberOfBids { get; set; }
        public int IsActive { get; set; }
        public int AuctionId { get; set; }
    }
}
