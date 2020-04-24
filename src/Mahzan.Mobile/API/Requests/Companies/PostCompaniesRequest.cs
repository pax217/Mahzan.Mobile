using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Companies
{
    public class PostCompaniesRequest
    {
        public string RFC { get; set; }

        public string CommercialName { get; set; }

        public string BusinessName { get; set; }

        public Guid GroupsId { get; set; }
    }
}
