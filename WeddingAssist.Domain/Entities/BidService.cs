using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class BidService
    {
        public int ServiceId { get; set; }
        public EService ServiceType { get; set; }
        public IList<BidServiceCategory> Categories { get; set; }
        public Decimal Amount { get; set; }//ONLY GETTER

        public BidService()
        {
            Categories = new List<BidServiceCategory>();
        }

        public Decimal TotalAmount()
        {
            foreach (var category in Categories)
                Amount += category.TotalAmount();
            return Amount;
        }
    }
}
