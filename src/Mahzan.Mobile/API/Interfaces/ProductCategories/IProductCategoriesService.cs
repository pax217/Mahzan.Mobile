using Mahzan.Mobile.API.Filters.ProductCategories;
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
    }
}
