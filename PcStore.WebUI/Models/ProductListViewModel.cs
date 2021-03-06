﻿using PcStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PcStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentSpecilization { get; set; }
    }
}