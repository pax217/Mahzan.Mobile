using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Products.Post
{
    public class PostProductTaxesRequest
    {
        public decimal TaxRate { get; set; }
        public Guid TaxesId { get; set; }
    }
}
