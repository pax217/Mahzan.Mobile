using Mahzan.Mobile.API.Filters.PaymentTypes;
using Mahzan.Mobile.API.Results.PaymentTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.PaymentTypes
{
    public interface IPaymentTypesService
    {
        Task<GetPaymentTypesResult> Get(GetPaymentTypesFilter getPaymentTypesFilter);
    }
}
