using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.PaymentTypes
{
    public class GetPaymentTypesResult:Result
    {
        public List<Entities.PaymentTypes> PaymentTypes { get; set; }

    }
}
