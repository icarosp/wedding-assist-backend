using System;
using System.Collections.Generic;
using System.Text;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Entities
{
    class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string AwsUserId { get; private set; }
        public ERegistrationStatus RegistrationStatus { get; private set; }
        public bool Enable { get; private set; }
    }
}

