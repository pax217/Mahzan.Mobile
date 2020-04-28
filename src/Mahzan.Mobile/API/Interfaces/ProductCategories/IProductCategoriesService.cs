using Mahzan.Mobile.API.Filters.ProductCategories;
using Mahzan.Mobile.API.Requests.ProductCategories;
using Mahzan.Mobile.API.Results.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.ProductCategories
{
    public interface IProductCategoriesService
    {
        Task<GetProductCategoriesResult> Get(GetProductCategoriesFilter getProductCategoriesFilter);

        Task<PostProductCategoriesResult> Add(PostProductCategoriesRequest request);

        Task<DeleteProductCategoriesResult> Delete(Guid productCategoriesId);

        Task<PutProductCategoriesResult> Put(PutProductCategoriesRequest request);
    }
}
