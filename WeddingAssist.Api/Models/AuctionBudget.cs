using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Api.Models
{
    public class AuctionBudget : Domain.Entities.AuctionBudget
    {
        public string CreationDateFormated { get; set; }
        public string EndDateFormated { get; set; }


        public int NumberOfBids { get; set; }
        public int IsActive { get; set; }
        public DateTime RemaningTime { get; set; }
    }
}
