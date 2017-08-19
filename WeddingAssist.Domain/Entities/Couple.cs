using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class Couple
    {
        public Fiance FianceOne { get; private set; }
        public Fiance FianceTwo { get; private set; }
        public string Nick { get; private set; }
    }
}
