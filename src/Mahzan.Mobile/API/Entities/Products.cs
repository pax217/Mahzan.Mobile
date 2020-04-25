using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Products
    {
        public Guid ProductsId { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? Cost { get; set; }

        public bool FollowInventory { get; set; }

        public bool AvailableInAllStores { get; set; }
        public ProductsPhotos ProductsPhotos { get; set; }

        public Guid ProductCategoriesId { get; set; }
        public Guid ProductUnitsId { get; set; }
    }
}
