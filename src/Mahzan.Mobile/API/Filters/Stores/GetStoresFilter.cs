using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.Stores
{
    public class GetStoresFilter
    {
        public Guid? StoresId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid? CompaniesId { get; set; }
    }
}
