using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AwsUserId { get; set; }
        public ERegistrationStatus RegistrationStatus { get; set; }
        public bool Enable { get; set; }
        public string Nickname { get; set; }
    }
}

