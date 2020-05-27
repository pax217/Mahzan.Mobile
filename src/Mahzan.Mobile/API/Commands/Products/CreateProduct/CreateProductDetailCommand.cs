using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Commands.Products.CreateProduct
{
    public class CreateProductDetailCommand
    {
        public Guid? ProductCategoriesId { get; set; }

        public Guid ProductUnitsId { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? Cost { get; set; }

        public bool FollowInventory { get; set; }
    }
}
