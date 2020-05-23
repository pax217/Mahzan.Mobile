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

        public decimal TaxRate { get; set; }

        public TaxTypeEnum TaxType { get; set; }

        public TaxOptionsEnum TaxOption { get; set; }

        public Guid MembersId { get; set; }
    }
}
