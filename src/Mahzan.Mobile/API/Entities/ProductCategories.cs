using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class ProductCategories
    {
        public Guid ProductCategoriesId { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public Guid MembersId { get; set; }
    }
}
