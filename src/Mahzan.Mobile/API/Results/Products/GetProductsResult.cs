using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Products
{
    public class GetProductsResult:Result
    {
        public List<Entities.Products> Products { get; set; }

    }
}
