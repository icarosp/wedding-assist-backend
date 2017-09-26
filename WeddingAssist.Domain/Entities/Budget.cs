using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public int CoupleId { get; set; }
        public IList<BudgetService> Services { get; set; }
        //public Decimal BudgetAmount { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime Duration { get; set; }
        //public List<EBidPriorityOrder> Priority { get; private set; }

        public Budget()
        {
            StartDate = DateTime.Now.AddHours(-3);
            Services = new List<BudgetService>();
        }
    }
}
