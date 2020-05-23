using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Taxes
{
    public class PostTaxesRequest
    {
        public string Name { get; set; }

        public bool TaxRateVariable { get; set; }

        public decimal TaxRatePercentage { get; set; }

        public bool Active { get; set; }

        public bool Printed { get; set; }
    }
}
