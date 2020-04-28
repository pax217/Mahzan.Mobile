using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.ProductCategories
{
    public class PutProductCategoriesRequest
    {
        public Guid ProductCategoriesId { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }
    }
}
