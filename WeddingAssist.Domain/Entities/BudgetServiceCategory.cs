using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BudgetServiceCategory
    {
        public IList<BudgetCategoryItem> Items { get; set; } //make it null

        public EBudgetServiceCategory Category { get; set; }
        public string Description { get; set; }


        public BudgetServiceCategory()
        {
            Items = new List<BudgetCategoryItem>();
        }
    }
}
