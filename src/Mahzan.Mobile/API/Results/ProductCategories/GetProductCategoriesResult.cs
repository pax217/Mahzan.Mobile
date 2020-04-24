using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.ProductCategories
{
    public class GetProductCategoriesResult:Result
    {
        public List<Entities.ProductCategories> ProductCategories { get; set; }
    }
}
