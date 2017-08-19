using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class Bid
    {
        public IList<BidItem> BidItems { get; private set; }
        public IList<BudgetCategoryItem> BudgetCategoryItem { get; private set; }
        public Decimal Amount { get; private set; }//ONLY GETTER
    }
}
