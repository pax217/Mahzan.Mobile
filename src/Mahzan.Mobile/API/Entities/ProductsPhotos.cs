using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class ProductsPhotos
    {
        public Guid ProductsPhotosId { get; set; }

        public string Title { get; set; }

        public DateTime DateTime { get; set; }

        public string MIMEType { get; set; }

        public string Base64 { get; set; }


        public Guid ProductsId { get; set; }
    }
}
