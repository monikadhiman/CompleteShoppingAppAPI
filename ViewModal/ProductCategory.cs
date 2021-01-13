using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myShoppingApp.Models;
namespace myShoppingApp.ViewModal
{
    public class ProductCategory
    {
        public string Category { get; set; }
        public List<Product> Products { get; set; }
    }
}