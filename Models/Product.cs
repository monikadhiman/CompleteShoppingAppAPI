//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myShoppingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public string Category { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageFile { get; set; }
        public string TC { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; }
    }
}
