using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class Fiance : User
    {
        public int FianceId { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public bool HasNewBid { get; set; }
        public int Age
        {
            get { var today = DateTime.Today;
                var age = today.Year - Birth.Year;
                if (Birth > today.AddYears(-age))
                    age--;
                return age;
            }
            set { }
        }
    }
 }
