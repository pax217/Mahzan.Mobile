using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.ProductCategories
{
    public class PostProductCategoriesResult:Result
    {
        public Entities.ProductCategories ProductCategory { get; set; }
    }
}
