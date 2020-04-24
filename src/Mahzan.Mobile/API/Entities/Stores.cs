using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Stores
    {
        public Guid StoresId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        //Companies
        public Guid CompaniesId { get; set; }

        //Members
        public Guid MembersId { get; set; }
    }
}
