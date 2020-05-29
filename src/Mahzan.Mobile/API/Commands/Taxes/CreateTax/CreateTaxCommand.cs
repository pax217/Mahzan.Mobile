using Mahzan.Mobile.API.Enums.Taxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Taxes
{
    public class CreateTaxCommand
    {
        public string Name { get; set; }

        public TaxTypeEnum Type { get; set; }

        public bool TaxRateVariable { get; set; }

        public decimal TaxRatePercentage { get; set; }

        public bool Active { get; set; }

        public bool Printed { get; set; }
    }
}
