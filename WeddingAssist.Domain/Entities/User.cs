using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    public class User
    {
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string AwsUserId { get; private set; }
        public ERegistrationStatus RegistrationStatus { get; private set; }
        public bool Enable { get; private set; }
        public string Nickname { get; private set; }
    }
}

