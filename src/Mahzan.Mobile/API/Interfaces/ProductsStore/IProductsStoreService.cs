using Mahzan.Mobile.API.Requests.ProductsStore;
using Mahzan.Mobile.API.Results.ProductsStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.ProductsStore
{
    public interface IProductsStoreService
    {
        Task<PostProductsStoreResult> Add(PostProductsStoreRequest postProductsStoreRequest);
    }
}
