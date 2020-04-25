using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Products_Store
    {
        public Guid ProductsStoreId { get; set; }

        public decimal Price { get; set; }

        public decimal? Cost { get; set; }

        public decimal? InStock { get; set; }

        public decimal? LowStock { get; set; }

        public decimal? OptimumStock { get; set; }

        //Products
        public Guid ProductsId { get; set; }
        public Products Products { get; set; }

        //Stores
        public Guid StoresId { get; set; }
    }
}
