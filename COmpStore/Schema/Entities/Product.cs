﻿using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Schema.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int PublisherId { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public string MadeIn { get; set; }

        public virtual Publisher Publisher { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
