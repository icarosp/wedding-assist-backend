﻿using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Provider : User
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string Logo { get; set; }
        public List<EService> Services { get; set; }
        public bool HasNewBudget { get; set; }
        public int TotalOfBids { get; set; }
        public int TotalOfWinnerBids { get; set; }

        public Provider()
        {
            Services = new List<EService>();
        }
    }
}
