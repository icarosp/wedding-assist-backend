using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class Wedding
    {
        public Couple Couple { get; private set; }
        public IList<AuctionBudget> Budgets { get; private set; }
    }
}
