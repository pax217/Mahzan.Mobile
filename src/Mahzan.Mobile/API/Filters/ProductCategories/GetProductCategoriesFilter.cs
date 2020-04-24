using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.ProductCategories
{
    public class GetProductCategoriesFilter
    {
        public Guid? ProductCategoriesId { get; set; }
        public string Description { get; set; }
    }
}
