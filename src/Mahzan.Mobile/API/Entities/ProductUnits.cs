using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class ProductUnits
    {
        public Guid ProductUnitsId { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public Guid MembersId { get; set; }
    }
}
