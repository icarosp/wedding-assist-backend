using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Budget
    {
        public IList<BudgetService> Services { get; private set; }
        public Decimal BudgetAmount { get; private set; }
        public DateTime StartDate { get; private set; }
        public TimeSpan Duration { get; private set; }
        public List<EBidPriorityOrder> Priority { get; private set; }
    }
}
