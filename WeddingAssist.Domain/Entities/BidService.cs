using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BidService
    {
        public EService ServiceType { get; set; }
        public IList<BidServiceCategory> BudgetCategoryItem { get; set; }
        public Decimal Amount { get; set; }//ONLY GETTER

        public BidService()
        {
            BudgetCategoryItem = new List<BidServiceCategory>();
        }
    }
}
