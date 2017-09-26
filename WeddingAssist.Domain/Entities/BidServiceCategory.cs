using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BidServiceCategory
    {
        public IList<BidItem> BidItems { get; set; }
        public EBudgetServiceCategory Category { get; set; }
        public Decimal Amount { get; set; }//ONLY GETTER

        public BidServiceCategory()
        {
            BidItems = new List<BidItem>();
        }
    }
}
