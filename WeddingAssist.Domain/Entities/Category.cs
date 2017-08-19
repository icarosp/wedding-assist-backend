﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingAssist.Domain.Entities
{
    public class Category
    {
        public IList<Category> SubCategory { get; private set; } //make it null
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Thumbnail { get; private set; }
    }
}
