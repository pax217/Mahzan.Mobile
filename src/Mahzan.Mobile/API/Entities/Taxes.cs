using Mahzan.Mobile.API.Enums.Taxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Taxes
    {
        public Guid TaxesId { get; set; }

        public string Name { get; set; }

        public decimal TaxRateVariable { get; set; }

        public decimal TaxRatePercentage { get; set; }

        public bool Active { get; set; }

        public bool Printed { get; set; }

        public Guid MembersId { get; set; }
    }
}
