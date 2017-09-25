//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WeddingAssist.Domain.Entities;

//namespace WeddingAssist.Api.Models
//{
//    public class AuctionBudget : Budget
//    {
//        public int NumberOfBids { get; set; }
//        public int IsActive { get; set; }

//        public string FormatedCreationDate { get
//            {
//                return this.StartDate.ToString("dd/MM/yyyy mm:hh");
//            } set { }
//        }

//        public bool IsAuctionActive {
//            get {
//                if (IsActive < 1 || this.Duration > DateTime.Now)
//                    return false;
//                return true;
//            } set { }
//        }
//    }
//}
