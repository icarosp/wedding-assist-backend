using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BidServiceCategory
    {
        public int CategoryId { get; set; }
        public IList<BidItem> Items { get; set; }
        public EBudgetServiceCategory Category { get; set; }
        public Decimal Amount { get; set; }//ONLY GETTER

        public BidServiceCategory()
        {
            Items = new List<BidItem>();
        }

        public Decimal TotalAmount()
        {
            foreach (var item in Items)
                Amount += item.BidItemAmount;
            return Amount;
        }
    }
}
