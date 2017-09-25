﻿using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class AuctionBudget
    {
        public int CoupleId { get; set; }
        public IList<BudgetService> Services { get; set; }
        //public Decimal BudgetAmount { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime Duration { get; set; }
        //public List<EBidPriorityOrder> Priority { get; private set; }

        public AuctionBudget()
        {
            StartDate = DateTime.Now;
            Services = new List<BudgetService>();
        }
    }
}
