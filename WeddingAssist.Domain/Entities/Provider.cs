using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Provider : User
    {
        public string ProviderName { get; private set; }
        public string Logo { get; private set; }
        public List<EService> Services { get; private set; }
    }
}
