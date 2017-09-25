using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BudgetCategoryItem
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public EBudgetCategoryItem Type {get; set;}
        public string Description { get; set; }
        public int PeopleQuantity { get; set; }
        //public double Quantity { get; private set; }
        //public Decimal MaxBudgetServiceItem { get; private set; }
        public bool IsRequired { get; set; }

    }
}
