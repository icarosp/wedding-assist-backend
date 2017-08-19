using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Fiance : User
    {
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public EGender Gender { get; private set; }
    }
}
