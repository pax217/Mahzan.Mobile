using Mahzan.Mobile.API.Filters.Products;
using Mahzan.Mobile.API.Requests.Products.Post;
using Mahzan.Mobile.API.Results.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Products
{
    public interface IProductsService
    {
        Task<GetProductsResult> Get(GetProductsFilter getProductsFilter);

        Task<PostProductsResult> Post(PostProductsRequest postProductsRequest);
    }
}
