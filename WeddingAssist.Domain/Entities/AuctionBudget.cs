using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Entities
{
    public class AuctionBudget : Budget
    {

        public int AuctionId { get; set; }
        public int NumberOfBids { get; set; }
        public int IsActive { get; set; }

        public string FormatedCreationDate
        {
            get
            {
                return this.StartDate.ToString("dd/MM/yyyy hh:mm");
            }
            set { }
        }

        public bool IsAuctionActive
        {
            get
            {
                if (IsActive < 1 || this.Duration > DateTime.Now)
                    return false;
                return true;
            }
            set { }
        }
    }
}
