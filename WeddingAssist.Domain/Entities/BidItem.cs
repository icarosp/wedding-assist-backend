using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class BidItem : BudgetCategoryItem
    {
        public int BidItemId { get; set; }
        public Decimal BidItemAmount { get; set; }
        public string BidItemDescription { get; set; }
    }
}
