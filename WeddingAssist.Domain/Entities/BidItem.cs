using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class BidItem
    {
        public BudgetCategoryItem Item { get; private set; }
        public Decimal Amount { get; private set; }
        public double BonusQuantity { get; private set; }
    }
}
