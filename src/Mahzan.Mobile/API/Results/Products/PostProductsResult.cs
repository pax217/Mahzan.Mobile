using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Products
{
    public class PostProductsResult:Result
    {

        public Entities.Products Product { get; set; }
    }
}
