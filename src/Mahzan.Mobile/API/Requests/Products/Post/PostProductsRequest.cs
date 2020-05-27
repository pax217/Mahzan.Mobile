using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Products.Post
{
    public class PostProductsRequest
    {
        public PostProductPhotoRequest PostProductPhotoRequest { get; set; }

        public PostProductDetailRequest PostProductDetailRequest { get; set; }

        public List<PostProductTaxesRequest> PostProductTaxesRequest { get; set; }
    }
}
