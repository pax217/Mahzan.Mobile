using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.ProductUnits
{
    public class PutProductUnitsRequest
    {
        public Guid ProductUnitsId { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }
    }
}
