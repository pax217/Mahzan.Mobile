using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.ProductsStore
{
    public class PostProductsStoreRequest
    {
        public List<ProductsStoreRequest> ProductsStoreRequest { get; set; }

    }
    public class ProductsStoreRequest
    {
        public decimal Price { get; set; }

        public decimal? Cost { get; set; }

        public decimal? InStock { get; set; }

        public decimal? LowStock { get; set; }

        public decimal? OptimumStock { get; set; }

        public Guid ProductsId { get; set; }

        public Guid StoresId { get; set; }
    }
}
