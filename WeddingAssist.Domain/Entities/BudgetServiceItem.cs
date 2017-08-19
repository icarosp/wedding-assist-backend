using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class BudgetServiceItem
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public Decimal MaxBudgetServiceItem { get; private set; }

    }
}
