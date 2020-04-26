using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class PaymentTypes
    {
        public Guid PaymentTypesId { get; set; }

        public string Name { get; set; }

        public Guid MembersId { get; set; }
    }
}
