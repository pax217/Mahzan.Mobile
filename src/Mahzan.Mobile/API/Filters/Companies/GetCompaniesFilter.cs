using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.Companies
{
    public class GetCompaniesFilter
    {
        public Guid? CompaniesId { get; set; }

        public string BusinessName { get; set; }
    }
}
