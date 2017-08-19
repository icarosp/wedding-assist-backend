using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class CategoryItem
    {
        public BudgetServiceCategory Category { get; private set; }
        public string Name { get; private set; }
        public double Quantity { get; private set; }
        public Decimal MaxBudgetServiceItem { get; private set; }
        public bool IsRequired { get; private set; }

    }
}
