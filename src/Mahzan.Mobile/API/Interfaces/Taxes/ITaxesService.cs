using Mahzan.Mobile.API.Filters.Taxes;
using Mahzan.Mobile.API.Requests.Taxes;
using Mahzan.Mobile.API.Results.Taxes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Taxes
{
    public interface ITaxesService
    {
        Task<GetTaxesResult> GetWhere(GetTaxesFilter filter);

        Task<PostTaxesResult> Post(PostTaxesRequest request);
    }
}
