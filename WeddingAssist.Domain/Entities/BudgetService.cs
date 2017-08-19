using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BudgetService
    {
        public EService ServiceType { get; private set; }
        public Decimal MaxBudgetService { get; private set; }
        public IList<BudgetServiceItem> ServiceItems { get; private set; }

    }
}
