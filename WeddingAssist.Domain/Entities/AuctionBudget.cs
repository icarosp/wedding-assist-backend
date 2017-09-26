using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Entities
{
    public class AuctionBudget : Budget
    {

        public string UserName { get; set; }

        public int AuctionId { get; set; }
        public int NumberOfBids { get; set; }
        public bool IsActive { get; set; }
        public DateTime EndDate { get; set; }

        public string FormatedCreationDate
        {
            get
            {
                return this.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            set { }
        }

        public string FormatedEndDate
        {
            get
            {
                return this.EndDate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            set { }
        }

        public bool IsAuctionActive
        {
            get
            {
                if (!IsActive || this.EndDate < DateTime.Now.AddHours(-3))
                    return false;
                return true;
            }
            set { }
        }
    }
}
