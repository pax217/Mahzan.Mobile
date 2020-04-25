using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.Products
{
    public class GetProductsFilter
    {
        public Guid ProductsId { get; set; }
        public string Barcode { get; set; }
    }
}
