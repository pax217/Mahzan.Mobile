using Mahzan.Mobile.API.Filters.ProductUnits;
using Mahzan.Mobile.API.Requests.ProductUnits;
using Mahzan.Mobile.API.Results.ProductUnits;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.ProductUnits
{
    public interface IProductUnitsService
    {
        Task<GetProductUnitsResult> Get(GetProductUnitsFilter getProductUnitsFilter);

        Task<PostProductUnitsResult> Post(PostProductUnitsRequest request);

        Task<PutProductUnitsResult> Put(PutProductUnitsRequest request);

        Task<DeleteProductUnitsResult> Delete(Guid productUnitsId);
    }
}
