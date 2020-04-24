using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Companies
    {
        public Guid CompaniesId { get; set; }
        public string RFC { get; set; }
        public string CommercialName { get; set; }
        public string BusinessName { get; set; }
        public bool Active { get; set; }
        public Guid GroupsId { get; set; }
        public Guid MembersId { get; set; }
    }
}
