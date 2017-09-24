using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BudgetService
    {
        public int BudgetId { get; set; }
        public EService ServiceType { get; set; }
        //public Decimal MaxBudgetService { get; set; }
        public IList<BudgetServiceCategory> Categories { get; set; }

        public BudgetService()
        {
            Categories = new List<BudgetServiceCategory>();
        }
    }
}
