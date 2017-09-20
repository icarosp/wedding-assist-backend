using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Fiance : User
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
    }
}
